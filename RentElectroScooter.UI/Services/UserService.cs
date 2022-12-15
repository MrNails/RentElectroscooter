using Microsoft.Extensions.Configuration;
using RentElectroScooter.CoreModels.DTO;
using RentElectroScooter.CoreModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RentElectroScooter.UI.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(IConfiguration configuration)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri($"{configuration["Host"]}/api/User/") };
        }

        public async Task<(string, System.Net.HttpStatusCode)> Authenticate(AuthData authData)
        {
            if (authData == null) throw new ArgumentNullException(nameof(authData));

            using var requestMsg = new HttpRequestMessage(HttpMethod.Post, "auth");

            requestMsg.Content = JsonContent.Create(authData);

            var result = await _httpClient.SendAsync(requestMsg);

            return (await result.Content.ReadAsStringAsync(), result.StatusCode);
        }

        public async Task<(string, System.Net.HttpStatusCode)> Register(RegisterData registerData)
        {
            if (registerData == null) throw new ArgumentNullException(nameof(registerData));

            using var requestMsg = new HttpRequestMessage(HttpMethod.Post, "register");

            requestMsg.Content = JsonContent.Create(registerData);

            var result = await _httpClient.SendAsync(requestMsg);

            return (await result.Content.ReadAsStringAsync(), result.StatusCode);
        }

        public async Task<(UserProfile, System.Net.HttpStatusCode)> GetProfile(string jwt)
        {
            if (string.IsNullOrEmpty(jwt)) throw new ArgumentException("Jwt cannot be empty.");

            using var requestMsg = new HttpRequestMessage(HttpMethod.Get, "profile");

            requestMsg.Headers.Add("Authorization", $"Bearer {jwt}");

            var result = await _httpClient.SendAsync(requestMsg);

            return (await result.Content.ReadFromJsonAsync<UserProfile>(), result.StatusCode);
        }
    }
}
