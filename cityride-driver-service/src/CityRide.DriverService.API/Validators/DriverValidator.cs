using CityRide.DriverService.Domain.Entities;
using FluentValidation;

namespace CityRide.DriverService.API.Validators;

public class DriverValidator : AbstractValidator<Driver>
{
    public DriverValidator()
    {
        RuleFor(driver => driver.Email).NotEmpty().EmailAddress();
        RuleFor(driver => driver.Password).NotEmpty().MinimumLength(6);
        RuleFor(driver => driver.FirstName).NotEmpty();
        RuleFor(driver => driver.LastName).NotEmpty();
        RuleFor(driver => driver.PhoneNumber).NotEmpty().Matches(@"^\d{10}$")
            .WithMessage("Phone number must be 10 digits.");
        RuleFor(driver => driver.CarClass).IsInEnum();
    }
}