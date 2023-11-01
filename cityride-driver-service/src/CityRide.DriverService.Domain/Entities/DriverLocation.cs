using NetTopologySuite.Geometries;
using CityRide.Events.Models.Enums;

namespace CityRide.DriverService.Domain.Entities;

public class DriverLocation
{
    public int Id { get; set; }
    public int DriverId { get; set; }
    public Geometry Location { get; set; }
    public DriverStatus Status { get; set; } = DriverStatus.Unavailable;
}