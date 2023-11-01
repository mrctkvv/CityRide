using CityRide.BillingService.Application.Services.Interfaces;
using CityRide.BillingService.Domain.Dtos;
using CityRide.Domain.Dtos;

namespace CityRide.BillingService.Application.Services;

public class CostService : ICostService
{
    private const int Precision = 2;

    public double CalculateRideCost(RidePriceDto? ridePriceDto, LocationDto source, LocationDto destination)
    {
        if (ridePriceDto == null)
        {
            // TODO: custom exceptions and exception handling
            throw new Exception("Can't calculate total cost of a null ride price");
        }
        
        double distance = CalculateDistance(source, destination);

        double totalCost = distance * ridePriceDto.CostPerKm * ridePriceDto.Coefficient + ridePriceDto.ExtraFees;
        return Math.Round(totalCost, Precision);
    }

    private static double CalculateDistance(LocationDto source, LocationDto destination)
    {
        return Math.Sqrt((source.Latitude - destination.Latitude) * (source.Latitude - destination.Latitude) +
                         (source.Longitude - destination.Longitude) * (source.Longitude - destination.Longitude));
    }
}