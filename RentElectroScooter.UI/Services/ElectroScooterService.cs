using Microsoft.Extensions.Configuration;
using RentElectroScooter.CoreModels.DTO;
using RentElectroScooter.CoreModels.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RentElectroScooter.UI.Services
{
    public class ElectroScooterService
    {
        private readonly HttpClient _httpClient;

        public ElectroScooterService(IConfiguration configuration)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri($"{configuration["Host"]}/api/ElectroScooter") };
        }

        public async Task<List<ElectroScooter>> GetElectroScootersAsync(string jwt, IEnumerable<FieldCondition> fieldConditions)
        {
            if (string.IsNullOrEmpty(jwt))
                throw new ArgumentException("Jwt cannot be empty.");

            using var requestMsg = new HttpRequestMessage(HttpMethod.Post, "electroscooters");

            requestMsg.Headers.Add("Bearer", jwt);
            requestMsg.Content = JsonContent.Create(fieldConditions);

            var result = await _httpClient.SendAsync(requestMsg);

            return await result.Content.ReadFromJsonAsync<List<ElectroScooter>>();
        }

        public async Task<string> RentElectroScooterAsync(string jwt, ElectroScooter electroScooter)
        {
            if (electroScooter == null) throw new ArgumentNullException(nameof(electroScooter));

            var queryUri = new Uri($"electroscooter?electroScooterId={electroScooter.Id}");

            using var requestMsg = new HttpRequestMessage(HttpMethod.Post, queryUri);

            requestMsg.Headers.Add("Bearer", jwt);

            var result = await _httpClient.SendAsync(requestMsg);

            return await result.Content.ReadAsStringAsync();
        }
    }
}
