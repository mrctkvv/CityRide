using CityRide.Domain.Dtos;
using CityRide.Domain.Enums;
using CityRide.DriverService.Domain.Dtos;
using DriverStatus = CityRide.Events.Models.Enums.DriverStatus;

namespace CityRide.DriverService.Application.Services.Interfaces;

public interface IDriverLocationService
{
    Task<List<DriverLocationDto>> GetClosestDriversAsync(LocationDto locationFrom, CarClass carClass,
        double distanceInMeters, int numberOfUsersToRetrieve);

    Task<DriverStatus> GetDriverStatus(int driverId);
    
    Task UpdateDriverStatus(int driverId, DriverStatus status);
    void SetDriverStatus(int driverId, DriverStatus status);
    Task UpdateDriverLocation(int driverId, LocationDto location);
    Task<DriverLocationDto> GetDriverLocationById(int driverId);
    Task UpdateDriverLocationAsync(DriverLocationDto newDriverLocation);
}