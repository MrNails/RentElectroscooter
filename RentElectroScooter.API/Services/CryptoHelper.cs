using Microsoft.IdentityModel.Tokens;
using RentElectroScooter.CoreModels.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RentElectroScooter.API.Services
{
    public static class CryptoHelper
    {
        private static readonly SHA256 s_sha256Encryptor = SHA256.Create();

        public static byte[] EncryptData(byte[] data, byte[] salt)
        {
            if (data.Length == 0 || salt.Length == 0)
                return Array.Empty<byte>();

            var encryptedStr = new byte[data.Length + salt.Length];

            for (int i = 0; i < data.Length; i++)
                encryptedStr[i] = data[i];

            for (int i = 0; i < salt.Length; i++)
                encryptedStr[data.Length + i] = salt[i];

            return s_sha256Encryptor.ComputeHash(encryptedStr);
        }

        public static string CreateJWT(User user, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(configuration[Constants.JWT_Key]);
            var jwt = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(int.TryParse(configuration[Constants.JWT_LifeTime], out var h) ? h : 1),
                Issuer = configuration[Constants.JWT_Issuer],
                Audience = configuration[Constants.JWT_Audience],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(jwt);
            return tokenHandler.WriteToken(token);
        }
    }
}
