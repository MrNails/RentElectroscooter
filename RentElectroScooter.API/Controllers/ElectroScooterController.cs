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
SELECT Id, UserId, Name, Position_Lattitude, Position_Longitude, BatteryCharge, 
Status, Description, AdditionlDataId, Modified, Created
FROM RentElectroScooterDB.dbo.ElectroScooters
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
                    query.AppendFormat("{0} = {1} {2}, ",
                        SQLHelpers.AddColumnBracket(fieldCondition.FieldName, _dBContext.UsingDatabase),
                        tmpParName, SQLHelpers.GetComprasionOperation(fieldCondition.ComprasionType));

                    dbArgs.Add(tmpParName, fieldCondition.Value);
                }
                else
                {
                    query.AppendFormat("{0} IS NULL {1}, ",
                        SQLHelpers.AddColumnBracket(fieldCondition.FieldName, _dBContext.UsingDatabase),
                        SQLHelpers.GetComprasionOperation(fieldCondition.ComprasionType));
                }
            }

            query.Remove(query.Length - 2, 2);

            return Ok(await dbConnection.QueryAsync<ElectroScooter>(query.ToString(), dbArgs));
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
            var electroScooter = await _dBContext.ElectroScooters.FirstOrDefaultAsync(es => es.Id == electroScooterId);

            if (electroScooter == null)
                return NotFound($"ElectroScooter with id {electroScooterId} does not exists.");

            if (electroScooter.UserId != null)
                return StatusCode(StatusCodes.Status409Conflict, "Specified electroscooter already using.");

            if (electroScooter.Status != VehicleStatus.Available)
                return BadRequest($"Vehicle has {electroScooter.Status} status.");

            electroScooter.UserId = userId;

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

            await _dBContext.SaveChangesAsync();

            return Ok();
        }

        private string? GetAuthtorizedUserId() => User.FindFirst("Id")?.Value;
    }
}
