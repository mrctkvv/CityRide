using CityRide.Domain.Enums;
using CityRide.Domain.Dtos;

namespace CityRide.DriverService.API.Authentication.Requests;

public class GetClosestDriversRequest
{
    public LocationDto Location { get; set; }
    public CarClass CarClass { get; set; }
    public double distanceInMeters { get; set; }
    public int numberOfDriversToRetrieve { get; set; } = 5;
}