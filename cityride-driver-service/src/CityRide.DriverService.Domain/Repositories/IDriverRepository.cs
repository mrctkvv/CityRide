using CityRide.Domain.Dtos;
using CityRide.DriverService.Domain.Entities;
using CityRide.Domain.Enums;

namespace CityRide.DriverService.Domain.Repositories;

public interface IDriverRepository : IBaseRepository<Driver>
{
    Task<Driver> GetDriverByEmailAndPasswordHash(string email, string passwordHash);

    List<DriverLocation> GetDriversByLocationAndCarClassAsync(LocationDto locationFrom,
        CarClass carClass,
        double distanceInMeters,
        int numberOfDriversToRetrieve);

    Task<DriverLocation?> GetDriverLocation(int driverId);
    void UpdateDriverLocationAsync(DriverLocation entity);
}