using FluentValidation;
using FluentValidation.AspNetCore;

using CityRide.BillingService.Infrastructure.Repositories;
using CityRide.BillingService.API.Requests;
using CityRide.BillingService.API.Validators;
using CityRide.BillingService.Application.Profiles;
using CityRide.BillingService.Application.Services;
using CityRide.BillingService.Application.Services.Interfaces;
using CityRide.BillingService.Domain.Dtos;
using CityRide.BillingService.Domain.Repositories;

namespace CityRide.BillingService.API;

public static class ServicesConfiguration
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRidePriceService, RidePriceService>();
        serviceCollection.AddScoped<ICostService, CostService>();
    }
    
    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRidePriceRepository, RidePriceRepository>();
    }

    public static void AddMappingProfiles(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(RidePriceToDto));
    }

    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddFluentValidationAutoValidation();
        serviceCollection.AddScoped<IValidator<RidePriceDto>, RidePriceDtoValidator>();
        serviceCollection.AddScoped<IValidator<CalculateRidePriceRequest>, CalculateRidePriceRequestValidator>();
    }
}