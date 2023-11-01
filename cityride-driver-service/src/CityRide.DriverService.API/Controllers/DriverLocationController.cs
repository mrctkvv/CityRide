using System.Security.Claims;
using AutoMapper;
using CityRide.Domain.Dtos;
using CityRide.DriverService.API.Authentication.Requests;
using CityRide.DriverService.Application.Profiles;
using CityRide.DriverService.Application.Services.Interfaces;
using CityRide.DriverService.Domain.Dtos;
using CityRide.DriverService.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DriverStatus = CityRide.Events.Models.Enums.DriverStatus;

namespace CityRide.DriverService.API.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class DriverLocationController : ControllerBase
{
    private readonly IDriverLocationService _driverLocationService;
    private readonly IMapper _mapper;

    public DriverLocationController(IDriverLocationService driverService, IMapper mapper)
    {
        _driverLocationService = driverService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<DriverLocationDto>> GetClosestDrivers(
        GetClosestDriversRequest getClosestDriversRequest)
    {
        var closestDrivers =
            await _driverLocationService.GetClosestDriversAsync(getClosestDriversRequest.Location,
                getClosestDriversRequest.CarClass, getClosestDriversRequest.distanceInMeters,
                getClosestDriversRequest.numberOfDriversToRetrieve);

        return Ok(closestDrivers);
    }

    [AllowAnonymous]
    [HttpPut]
    public async Task UpdateDriverStatus(int driverId, DriverStatus status)
    {
        await _driverLocationService.UpdateDriverStatus(driverId, status);
    }

    [HttpGet]
    public async Task<ActionResult<string>> GetDriverStatus()
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        return Ok(_driverLocationService.GetDriverStatus(userId).Result);
    }

    [HttpPut]
    public async Task SetDriverStatus(DriverStatus status)
    {
        var driverId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "DriverId")?.Value;
        await _driverLocationService.UpdateDriverStatus(Convert.ToInt32(driverId), status);
    }

    [HttpPut]
    public async Task UpdateDriverLocation(LocationDto location)
    {
        var driverId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "DriverId")?.Value;
        await _driverLocationService.UpdateDriverLocation(Convert.ToInt32(driverId), location);
    }
}