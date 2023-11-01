using CityRide.RideService.Domain.Dtos;

namespace CityRide.RideService.Application.Services.Interfaces;

public interface IRideService
{
    Task<RideDto> CreateRideAsync(RideDto rideDto);
    Task UpdateRideAsync(RideDto rideDto);
    Task DeleteRideAsync(int rideId);
    Task<RideDto> GetRide(int rideId);

    Task<IEnumerable<RideDto>> GetRidesByClientIdAsync(int clientId);

    Task<IEnumerable<RideDto>> GetRidesByDriverIdAsync(int driverId);
}