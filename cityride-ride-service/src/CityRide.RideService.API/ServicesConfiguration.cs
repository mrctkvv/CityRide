using FluentValidation.AspNetCore;
using CityRide.RideService.Application.Services.Interfaces;
using CityRide.RideService.Domain.Repositories;
using CityRide.RideService.Infrastructure;

namespace CityRide.RideService.API;

public static class ServicesConfiguration
{
    public static void AddAppService(this IServiceCollection services)
    {
        services.AddDbContext<RideServiceContext>();

        services.AddScoped<IRideRepository, RideRepository>();


        services.AddScoped<IRideService, global::CityRide.RideService.Application.Services.RideService>();


        services.AddFluentValidationAutoValidation();
    }
}