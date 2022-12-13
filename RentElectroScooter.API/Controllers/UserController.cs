using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentElectroScooter.CoreModels.DTO;
using RentElectroScooter.API.Services;
using RentElectroScooter.CoreModels.Models;
using RentElectroScooter.DAL.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace RentElectroScooter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly RentElectroscooterDBContext _dBContext;
        private readonly IConfiguration _configuration;

        public UserController(ILogger<UserController> logger, RentElectroscooterDBContext dBContext, IConfiguration configuration)
        {
            _logger = logger;
            _dBContext = dBContext;
            _configuration = configuration;
        }

        [HttpPost("auth", Name = "PostAuthenticate")]
        public async Task<ActionResult> Authenticate([FromBody]AuthData authData)
        {
            var error = authData?.Error ?? "Authenticate data did not provide.";

            if (error != string.Empty)
                return BadRequest(error);

            var user = await _dBContext.Users.FirstOrDefaultAsync(u => u.Login == authData!.Login);

            if (user == null)
                return NotFound($"User with login {authData.Login} not found.");

            var pwd = CryptoHelper.EncryptData(Encoding.UTF8.GetBytes(authData.Password), Encoding.Unicode.GetBytes(user.Salt));

            if (Encoding.Unicode.GetString(pwd) != user.Password)
                return Unauthorized("Password is not correct.");

            return Ok(CryptoHelper.CreateJWT(user, _configuration));
        }

        [HttpPost("register", Name = "PostRegister")]
        public async Task<ActionResult> Register([FromBody]RegisterData registerData)
        {
            var error = registerData?.Error ?? "Authenticate data did not provide.";

            if (error != string.Empty)
                return BadRequest(error);

            var user = await _dBContext.Users.FirstOrDefaultAsync(u => u.Login == registerData!.Login);

            if (user != null)
                return StatusCode(StatusCodes.Status409Conflict, "User already exists.");

            var saltSizeS = _configuration[Constants.SaltSize];

            if (!int.TryParse(saltSizeS, out var saltSize))
            {
                _logger.LogError("User create error: Salt size is not defined.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var salt = RandomNumberGenerator.GetBytes(saltSize);
            var pwd = CryptoHelper.EncryptData(Encoding.UTF8.GetBytes(registerData.Password), salt);

            user = new User
            {
                Id = MassTransit.NewId.NextGuid(),
                Login = registerData.Login,
                Password = Encoding.Unicode.GetString(pwd),
                Salt = Encoding.Unicode.GetString(salt),
            };

            _dBContext.Users.Add(user);
            _dBContext.UserProfiles.Add(new UserProfile { UserId = user.Id, Name = registerData.Name });

            await _dBContext.SaveChangesAsync();

            return Ok(CryptoHelper.CreateJWT(user, _configuration));
        }

        [Authorize]
        [HttpGet("profile", Name = "GetUserProfile")]
        public ActionResult<UserProfile> GetUserProfile()
        {
            var userIdS = User.FindFirst("Id")?.Value;

            if (userIdS == null)
                return Unauthorized();

            var userId = new Guid(userIdS);

            var userProfile = _dBContext.UserProfiles.FirstOrDefault(up => up.UserId == userId);

            if (userProfile == null)
                return NotFound("User profile not found.");

            return userProfile;
        }
    }
}
