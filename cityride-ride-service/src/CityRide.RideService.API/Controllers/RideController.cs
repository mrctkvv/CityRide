using AutoMapper;
using CityRide.RideService.API.Ride.Responses;
using Microsoft.AspNetCore.Mvc;
using CityRide.RideService.Application.Services.Interfaces;

namespace CityRide.RideService.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RideController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRideService _rideService;

    public RideController(IRideService rideService, IMapper mapper)
    {
        _rideService = rideService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RideResponse>>> GetRidesByDriver(int driverId)
    {
        var rides = await _rideService.GetRidesByDriverIdAsync(driverId);
        var rideResponses = rides.Select(ride => _mapper.Map<RideResponse>(ride)).ToList();

        return Ok(rideResponses);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RideResponse>>> GetRidesByClient(int clientId)
    {
        var rides = await _rideService.GetRidesByClientIdAsync(clientId);
        var rideResponses = rides.Select(ride => _mapper.Map<RideResponse>(ride)).ToList();

        return Ok(rideResponses);
    }
}