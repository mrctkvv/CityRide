using ClientService.Domain.Dtos;
using Microsoft.AspNetCore.SignalR;
using ClientService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ClientService.Application.Services
{
    [Authorize]
    public class ClientHub : Hub<IClientAppClient>
    {
        private readonly IBillingApiService _billingApiService;

        public ClientHub(IBillingApiService billingApiService)
        {
            _billingApiService = billingApiService;
        }

        public async Task SendMessageToClient(string clientId, string message)
        {
            await Clients.Client(clientId).ReceiveRideStatus(message);
        }

        public async Task<decimal> CalculateRidePrice(CalculateRidePriceQueryDto calculateRidePriceQueryDto)
        {
            return await _billingApiService.CalculateRidePriceRequestAsync(calculateRidePriceQueryDto);
        }
    }
}
