using CityRide.Domain.Enums;

namespace CityRide.Events
{
    public class RideStatusUpdated
    {
        public RideStatus Status { get; set; }
    }
}