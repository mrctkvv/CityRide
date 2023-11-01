using ClientService.Application.Services.Interfaces;
using ClientService.Domain.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace ClientService.Application.Services
{
    public class BillingApiService : IBillingApiService
    {
        private readonly IConfiguration _configuration;

        public BillingApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<decimal> CalculateRidePriceRequestAsync(CalculateRidePriceQueryDto calculateRidePriceQueryDto)
        {
            decimal rideCost = 0;
            using (var httpClient = new HttpClient())
            {
                var jsonContent = JsonContent.Create(calculateRidePriceQueryDto);
                var result = await httpClient.PostAsync(_configuration["BillingService:CalculateUrl"], jsonContent);
                if (result.IsSuccessStatusCode)
                {
                    var deserializedObject = JsonConvert.DeserializeObject<CalculatedRidePriceDto>(await result.Content.ReadAsStringAsync());
                    rideCost = deserializedObject!.TotalCost;
                }
            }
            return rideCost;
        }
    }
}
