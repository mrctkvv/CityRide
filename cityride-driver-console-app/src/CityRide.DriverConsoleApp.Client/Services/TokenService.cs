using CityRide.DriverConsoleApp.Client.Requests;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace CityRide.DriverConsoleApp.Client.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetTokenAsync(LoginRequest loginRequest)
        {
            var content = JsonContent.Create(loginRequest);
            string token = string.Empty;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(_configuration["LoginUrl"], content);
                    if (response.IsSuccessStatusCode)
                    {
                        token = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Authorization connection failed.");
            }
            return token;
        }
    }
}
