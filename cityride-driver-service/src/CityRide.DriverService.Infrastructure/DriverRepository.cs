using CityRide.Domain.Dtos;
using CityRide.DriverService.Domain.Entities;
using CityRide.DriverService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using CityRide.Events.Models.Enums;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using CarClass = CityRide.Domain.Enums.CarClass;

namespace CityRide.DriverService.Infrastructure;

public class DriverRepository : BaseRepository<Driver>, IDriverRepository
{
    public DriverRepository(DriverServiceContext appContext) : base(appContext)
    {
    }

    public async Task<Driver> GetDriverByEmailAndPasswordHash(string email, string passwordHash)
    {
        return await _context.Drivers.Where(c => c.Email == email && c.Password == passwordHash).FirstOrDefaultAsync();
    }

    public List<DriverLocation> GetDriversByLocationAndCarClassAsync(LocationDto locationFrom,
        CarClass carClass, double distanceInMeters, int maxDriversToRetrieve)
    {
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);
        var point = geometryFactory.CreatePoint(new Coordinate(locationFrom.Longitude, locationFrom.Latitude));

        var topDrivers = _context.DriverLocations
            .Where(l => l.Status == DriverStatus.Available && l.Location.Distance(point) <= distanceInMeters)
            .OrderBy(l => l.Location.Distance(point))
            .Join(_context.Drivers,
                closestDriver => closestDriver.DriverId,
                driver => driver.Id,
                (closestDriver, driver) => new { ClosestDriver = closestDriver, Driver = driver })
            .Where(x => x.Driver.CarClass == (CarClass)carClass)
            .Select(x => x.ClosestDriver)
            .Take(maxDriversToRetrieve)
            .ToList();

        return topDrivers;
    }

    public async Task<DriverLocation?> GetDriverLocation(int driverId)
    {
        return await _context.DriverLocations
            .Where(dl => dl.DriverId == driverId)
            .FirstOrDefaultAsync();
    }

    public async void UpdateDriverLocationAsync(DriverLocation entity)
    {
        _context.DriverLocations.Attach(entity);
        _context.DriverLocations.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }
}