using CityRide.Domain.Dtos;
using CityRide.DriverService.Domain.Dtos;
using CityRide.DriverService.Domain.Entities;
using CityRide.Events.Models.Enums;
using CarClass = CityRide.Domain.Enums.CarClass;

namespace CityRide.DriverService.Application.Services.Interfaces;

public interface IDriverService
{
    Task<DriverDto> CreateDriverAsync(DriverDto driverDto);
    Task UpdateDriverAsync(DriverDto driverDto);
    Task DeleteDriverAsync(int driverId);
    Task<DriverDto> GetDriverByIdAsync(int? driverId);
    Task<DriverDto?> GetDriverByEmailAndPassword(string email, string password);
    int CurrentDriverId { get; }
}