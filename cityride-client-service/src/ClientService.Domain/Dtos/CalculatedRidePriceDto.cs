using CityRide.Domain.Enums;

namespace ClientService.Domain.Dtos
{
    public class CalculatedRidePriceDto
    {
        public CarClass CarClass { get; set; }
        public decimal TotalCost { get; set; }
    }
}
