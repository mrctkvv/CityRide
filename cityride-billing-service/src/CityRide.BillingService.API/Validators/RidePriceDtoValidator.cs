using System.Data;
using FluentValidation;

using CityRide.BillingService.Domain.Dtos;

namespace CityRide.BillingService.API.Validators;

public class RidePriceDtoValidator : AbstractValidator<RidePriceDto>
{
    public RidePriceDtoValidator()
    {
        RuleFor(c => c.Id).Cascade(CascadeMode.Stop).GreaterThan(0);
        RuleFor(c => c.Name).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
        RuleFor(c => c.Coefficient).Cascade(CascadeMode.Stop).GreaterThan(0);
        RuleFor(r => r.CostPerKm).Cascade(CascadeMode.Stop).GreaterThanOrEqualTo(0);
        RuleFor(r => r.ExtraFees).Cascade(CascadeMode.Stop).GreaterThanOrEqualTo(0);
    }
}