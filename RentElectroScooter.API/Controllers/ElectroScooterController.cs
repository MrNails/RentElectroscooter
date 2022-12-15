using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentElectroScooter.CoreModels.DTO;
using RentElectroScooter.API.Services;
using RentElectroScooter.CoreModels.Models;
using RentElectroScooter.DAL.Repositories;
using System.Reflection.Metadata;
using System.Text;
using RentElectroScooter.CoreModels;

namespace RentElectroScooter.API.Controllers
{
    [ApiController, Authorize]
    [Route("api/[controller]")]
    public class ElectroScooterController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly RentElectroscooterDBContext _dBContext;

        public ElectroScooterController(ILogger<UserController> logger, RentElectroscooterDBContext dBContext)
        {
            _logger = logger;
            _dBContext = dBContext;
        }

        [AllowAnonymous]
        [HttpPost("electroscooters", Name = nameof(GetElectroScooters))]
        public async Task<ActionResult<IEnumerable<ElectroScooter>>> GetElectroScooters([FromBody] FieldCondition[] fieldConditions)
        {
            if (fieldConditions == null || fieldConditions.Length == 0)
                return Ok(await _dBContext.ElectroScooters.ToListAsync());

            var dbConnection = _dBContext.Database.GetDbConnection();

            var query = new StringBuilder(@"
SELECT es.Id, es.UserId, es.Name, es.BatteryCharge, 
es.Status, es.Description, es.AdditionalDataId, es.Modified, es.Created,
es.Id, es.Position_Latitude as Latitude, es.Position_Longitude as Longitude,
vd.Id, vd.[ManufacturerName], vd.[MaxBatteryCharge], vd.[MaxLoadWeight], 
vd.[MaxSpeed], vd.[PricePerTime], vd.[Time], vd.[TimeUnits]
FROM RentElectroScooterDB.dbo.ElectroScooters es
    INNER JOIN
[RentElectroScooterDB].[dbo].[VehicleDatas] vd ON vd.Id = es.AdditionalDataId
WHERE ");

            var dbArgs = new DynamicParameters();
            var parName = "@param";
            for (int i = 0; i < fieldConditions.Length; i++)
            {
                var fieldCondition = fieldConditions[i];
                var tmpParName = parName + i.ToString();

                var err = fieldCondition.Error;
                if (err != string.Empty)
                    return BadRequest($"Condition at position {i} has an error: {err}");

                if (fieldCondition.Value != null)
                {
                    query.AppendFormat("{0} = {1} {2} ",
                        SQLHelpers.AddColumnBracket(fieldCondition.FieldName, _dBContext.UsingDatabase),
                        tmpParName,
                        i < fieldConditions.Length - 1
                            ? SQLHelpers.GetComprasionOperation(fieldCondition.ComprasionType)
                            : string.Empty);

                    dbArgs.Add(tmpParName, fieldCondition.Value);
                }
                else
                {
                    query.AppendFormat("{0} IS NULL {1} ",
                        SQLHelpers.AddColumnBracket(fieldCondition.FieldName, _dBContext.UsingDatabase),
                        i < fieldConditions.Length - 1
                            ? SQLHelpers.GetComprasionOperation(fieldCondition.ComprasionType)
                            : string.Empty);
                }
            }

            query.Remove(query.Length - 2, 2);

            return Ok(await dbConnection.QueryAsync<ElectroScooter, Coordinate, VehicleData, ElectroScooter>(query.ToString(),
                (es, c, vd) =>
                { 
                    es.Position = c;
                    es.AdditionalData = vd;
                    return es; },
                dbArgs));
        }

        [AllowAnonymous]
        [HttpGet("electroscooter", Name = nameof(GetElectroScooter))]
        public async Task<ActionResult<ElectroScooter>> GetElectroScooter(Guid electroScooterId)
        {
            var electroScooter = await _dBContext.ElectroScooters.FirstOrDefaultAsync(es => es.Id == electroScooterId);

            if (electroScooter == null)
                return NotFound($"ElectroScooter with id {electroScooterId} does not exists.");

            return Ok(electroScooter);
        }

        [HttpPost("electroscooter", Name = nameof(RentElectroScooter))]
        public async Task<ActionResult> RentElectroScooter(Guid electroScooterId)
        {
            var userId = new Guid(GetAuthtorizedUserId());

            if (await _dBContext.ElectroScooters.FirstOrDefaultAsync(es => es.UserId == userId) != null)
                return StatusCode(StatusCodes.Status409Conflict, "There is renting electroscooter.");

            var electroScooter = await _dBContext.ElectroScooters.FirstOrDefaultAsync(es => es.Id == electroScooterId);

            if (electroScooter == null)
                return NotFound($"ElectroScooter with id {electroScooterId} does not exists.");

            if (electroScooter.UserId != null)
                return StatusCode(StatusCodes.Status409Conflict, "Specified electroscooter already using.");

            if (electroScooter.Status != VehicleStatus.Available)
                return BadRequest($"Vehicle has {electroScooter.Status} status.");

            electroScooter.UserId = userId;
            electroScooter.Status = VehicleStatus.Occupied;

            await _dBContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("electroscooter", Name = nameof(ReturnElectroScooter))]
        public async Task<ActionResult> ReturnElectroScooter(Guid electroScooterId)
        {
            var userIdS = GetAuthtorizedUserId();
            var userId = new Guid(userIdS);
            var electroScooter = await _dBContext.ElectroScooters.FirstOrDefaultAsync(es => es.Id == electroScooterId);

            if (electroScooter == null)
                return NotFound($"ElectroScooter with id {electroScooterId} does not exists.");

            if (electroScooter.UserId != userId)
                return StatusCode(StatusCodes.Status409Conflict, $"Specified electroscooter is not using by user {userIdS}.");

            electroScooter.UserId = null;

            electroScooter.Status = electroScooter.BatteryCharge < electroScooter.AdditionalData.MaxBatteryCharge * 0.1
                ? VehicleStatus.Discharged
                : VehicleStatus.Available;

            await _dBContext.SaveChangesAsync();

            return Ok();
        }

        private string? GetAuthtorizedUserId() => User.FindFirst("Id")?.Value;
    }
}
