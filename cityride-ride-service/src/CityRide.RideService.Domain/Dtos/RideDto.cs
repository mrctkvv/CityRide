using CityRide.RideService.Domain.Entities;

namespace CityRide.RideService.Domain.Dtos;

public class RideDto
{
    public int Id { get; set; }
    public Location From { get; set; } = new();
    public Location To { get; set; } = new();
    public int ClientId { get; set; }
    public int? DriverId { get; set; } = null;

    protected RideStatus Status { get; set; } = RideStatus.NotDefined;
    public decimal Price { get; set; }
}