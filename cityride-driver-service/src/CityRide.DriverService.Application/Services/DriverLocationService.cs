using AutoMapper;
using CityRide.Domain.Dtos;
using CityRide.Domain.Enums;
using CityRide.DriverService.Application.Exceptions.DriverLocationExceptions;
using CityRide.DriverService.Application.Services.Interfaces;
using CityRide.DriverService.Domain.Dtos;
using CityRide.DriverService.Domain.Entities;
using CityRide.DriverService.Domain.Repositories;
using NetTopologySuite.Geometries;
using DriverStatus = CityRide.Events.Models.Enums.DriverStatus;

namespace CityRide.DriverService.Application.Services;

public class DriverLocationService : IDriverLocationService
{
    private readonly IDriverRepository _driverRepository;
    private readonly IMapper _mapper;

    public DriverLocationService(IDriverRepository driverRepository, IMapper mapper)
    {
        _driverRepository = driverRepository;
        _mapper = mapper;
    }

    public async Task<DriverLocationDto> GetDriverLocationById(int driverId)
    {
        var driverLocation = await _driverRepository.GetDriverLocation(driverId);
        if (driverLocation == null) throw new DriverLocationNotFoundException(driverId);
        return _mapper.Map<DriverLocationDto>(driverLocation);
    }

    public async Task<List<DriverLocationDto>> GetClosestDriversAsync(LocationDto locationFrom,
        CarClass carClass, double distanceInMeters,
        int numberOfDriversToRetrieve)
    {
        return _mapper.Map<List<DriverLocationDto>>( _driverRepository.GetDriversByLocationAndCarClassAsync(locationFrom, carClass, distanceInMeters,
            numberOfDriversToRetrieve));
    }

    public Task<DriverStatus> GetDriverStatus(int driverId)
    {
        var driverLocation = _driverRepository.GetDriverLocation(driverId).Result;
        if (driverLocation == null) throw new DriverLocationNotFoundException(driverId);
        return Task.FromResult(driverLocation.Status);
    }
    

    
    public async void SetDriverStatus(int driverId, DriverStatus status)
    {
        var driverLocation = _driverRepository.GetDriverLocation(driverId).Result;
        if (driverLocation == null) throw new DriverLocationNotFoundException(driverId);
        driverLocation.Status = status;
        await UpdateDriverLocationAsync( _mapper.Map<DriverLocationDto>(driverLocation));
    }
    
    public async Task UpdateDriverStatus(int driverId, DriverStatus status)
    {
        SetDriverStatus(driverId, status);
    }
    
    public async Task UpdateDriverLocation(int driverId, LocationDto location)
    {
        var driverLocation = _driverRepository.GetDriverLocation(driverId).Result;
        if (driverLocation == null) throw new DriverLocationNotFoundException(driverId);
        driverLocation.Location = new Point(location.Latitude, location.Longitude);
        await UpdateDriverLocationAsync( _mapper.Map<DriverLocationDto>(driverLocation));
    }
    
    public async Task UpdateDriverLocationAsync(DriverLocationDto newDriverLocation)
    {
        var driverLocation = await _driverRepository.GetDriverLocation(newDriverLocation.DriverId);
        if (driverLocation == null)
            throw new DriverLocationNotFoundException("Driver Location bny this id" + driverLocation.Id +
                                                      " is not found ");
        _driverRepository.UpdateDriverLocationAsync(driverLocation);
    }
}