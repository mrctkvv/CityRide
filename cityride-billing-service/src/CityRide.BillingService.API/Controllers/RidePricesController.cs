using CityRide.BillingService.API.Requests;
using Microsoft.AspNetCore.Mvc;

using CityRide.BillingService.API.Responses;
using CityRide.BillingService.Application.Services.Interfaces;
using CityRide.BillingService.Domain.Dtos;

namespace CityRide.BillingService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RidePricesController : ControllerBase
    {
        private readonly IRidePriceService _ridePriceService;
        private readonly ICostService _costService;

        public RidePricesController(IRidePriceService ridePriceService, ICostService costService)
        {
            _ridePriceService = ridePriceService;
            _costService = costService;
        }

        [HttpPost]
        public async Task<ActionResult<CalculateRidePriceResponse>> CalculateRidePrice(
            CalculateRidePriceRequest calculateRidePriceRequest)
        {
            var ridePriceDto = await _ridePriceService.GetRidePriceByCarClass(calculateRidePriceRequest.CarClass);
            var source = calculateRidePriceRequest.Source;
            var destination = calculateRidePriceRequest.Destination;
            
            var totalCost = _costService.CalculateRideCost(ridePriceDto, source, destination);

            var calculateRidePriceResponse = new CalculateRidePriceResponse
            {
                // If ridePriceDto is null _costService would throw an exception by now
                CarClass = ridePriceDto!.Name,
                TotalCost = totalCost
            };

            return Ok(calculateRidePriceResponse);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RidePriceDto?>>> GetAllRidePrices()
        {
            var ridePrices = await _ridePriceService.GetAllRidePrices();

            return Ok(ridePrices);
        }

        // CRUD
        [HttpGet]
        public async Task<ActionResult<RidePriceDto?>> GetRidePrice(int ridePriceId)
        {
            var ridePriceDto = await _ridePriceService.GetRidePriceById(ridePriceId);
            
            return Ok(ridePriceDto);
        }

        [HttpPost]
        public async Task<ActionResult<RidePriceDto?>> CreateRidePrice(RidePriceDto ridePriceDto)
        {
            var createdRidePriceDto = await _ridePriceService.CreateRidePrice(ridePriceDto);

            return Ok(createdRidePriceDto);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateRidePrice(RidePriceDto ridePriceDto)
        {
            await _ridePriceService.UpdateRidePrice(ridePriceDto);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRidePriceDto(int ridePriceId)
        {
            await _ridePriceService.DeleteRidePrice(ridePriceId);

            return NoContent();
        }
    }
}
