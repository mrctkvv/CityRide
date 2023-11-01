using System.Security.Claims;
using AutoMapper;
using CityRide.Domain.Dtos;
using CityRide.DriverService.API.Authentication.Requests;
using CityRide.DriverService.Application.Exceptions;
using CityRide.DriverService.Application.Exceptions.DriverExceptions;
using CityRide.DriverService.Application.Services.Interfaces;
using CityRide.DriverService.Domain.Dtos;
using CityRide.DriverService.Domain.Entities;
using CityRide.Events.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;


namespace CityRide.DriverService.API.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class DriverController : ControllerBase
{
    private readonly IDriverService _driverService;
    private readonly IMapper _mapper;

    public DriverController(IDriverService driverService, IMapper mapper)
    {
        _driverService = driverService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<Driver>> CreateDriver(Driver request)
    {
        var createdDriver = await _driverService.CreateDriverAsync(_mapper.Map<DriverDto>(request));

        return Ok(_mapper.Map<Driver>(createdDriver));
    }

    [HttpGet]
    public async Task<ActionResult<Driver>> GetDriverProfile(int driverId)
    {
        if (driverId != _driverService.CurrentDriverId) throw new NotAllowedException();

        var driver = await _driverService.GetDriverByIdAsync(driverId);

        return Ok(_mapper.Map<Driver>(driver));
    }

    [HttpPut]
    public async Task<ActionResult> UpdateDriver(Driver request)
    {
        if (request.Id != _driverService.CurrentDriverId) throw new NotAllowedException();

        var driverDto = _mapper.Map<DriverDto>(request);

        await _driverService.UpdateDriverAsync(driverDto);

        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteTodoList(int driverId)
    {
        if (driverId != _driverService.CurrentDriverId) throw new NotAllowedException();

        await _driverService.DeleteDriverAsync(driverId);

        return NoContent();
    }
}