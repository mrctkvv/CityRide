using ClientService.Domain.Dtos;

namespace ClientService.Application.Services.Interfaces
{
    public interface IBillingApiService
    {
        Task<decimal> CalculateRidePriceRequestAsync(CalculateRidePriceQueryDto calculateRidePriceQueryDto);
    }
}
