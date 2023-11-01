using AutoMapper;
using CityRide.DriverService.Application.Services.Interfaces;
using CityRide.DriverService.Domain.Dtos;
using CityRide.DriverService.Domain.Entities;
using CityRide.DriverService.Domain.Repositories;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CityRide.Domain.Dtos;
using CityRide.Events.Models.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using CarClass = CityRide.Domain.Enums.CarClass;


namespace CityRide.DriverService.Application.Services;

public class DriverService : IDriverService
{
    private readonly IDriverRepository _driverRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private int? _currentDriverId;

    public DriverService(IDriverRepository driverRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _driverRepository = driverRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<DriverDto> CreateDriverAsync(DriverDto driverDto)
    {
        var driver = _mapper.Map<Driver>(driverDto);
        driver.Password = ComputeObjectHash(driver.Password);
        var createdDriver = await _driverRepository.CreateAsync(driver);

        return _mapper.Map<DriverDto>(createdDriver);
    }

    public async Task<DriverDto> GetDriverByIdAsync(int? driverId)
    {
        var driver = await _driverRepository.GetByIdAsync(driverId);
        if (driver == null) throw new DriveNotFoundException();
        return _mapper.Map<DriverDto>(driver);
    }


    public async Task DeleteDriverAsync(int driverId)
    {
        await _driverRepository.DeleteAsync(driverId);
    }

    public async Task UpdateDriverAsync(DriverDto driverDto)
    {
        var driver = await _driverRepository.GetByIdAsync(driverDto.Id);

        if (driver == null) throw new DriveNotFoundException();
        driverDto.Password = ComputeObjectHash(driverDto.Password);

        _mapper.Map(driverDto, driver);
        _driverRepository.UpdateAsync(driver);
    }

    public async Task<DriverDto?> GetDriverByEmailAndPassword(string email, string password)
    {
        var passwordHashed = ComputeObjectHash(password);

        var driver = await _driverRepository.GetDriverByEmailAndPasswordHash(email, passwordHashed);

        return _mapper.Map<DriverDto>(driver);
    }

    public int CurrentDriverId
    {
        get
        {
            if (_currentDriverId == null)
            {
                var userIdClaim = GetUserIdFromClaims();

                if (int.TryParse(userIdClaim, out var userId)) _currentDriverId = userId;
            }

            return (int)_currentDriverId;
        }
    }


    private string? GetUserIdFromClaims()
    {
        return _httpContextAccessor.HttpContext?.User.Claims
            .First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    }

    private string ComputeObjectHash<T>(T obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        var bytes = Encoding.UTF8.GetBytes(json);
        var hashBytes = SHA256.HashData(bytes);
        return Convert.ToBase64String(hashBytes);
    }
}