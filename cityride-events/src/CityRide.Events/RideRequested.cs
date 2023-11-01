using CityRide.Domain.Dtos;
using CityRide.Domain.Enums;

namespace CityRide.Events
{
    public class RideRequested
    {
        public LocationDto Source { get; set; } = new();
        public LocationDto Destination { get; set; } = new();
        public CarClass CarClass { get; set; }
        public decimal Price { get; set; }
    }
}