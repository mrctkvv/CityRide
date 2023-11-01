using CityRide.Domain.Dtos;
using CityRide.Domain.Enums;

namespace ClientService.Domain.Dtos
{
    public class CalculateRidePriceQueryDto
    {
        public LocationDto Source { get; set; } = null!;
        public LocationDto Destination { get; set; } = null!;
        public CarClass CarClass { get; set; }
    }
}
