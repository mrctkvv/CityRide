using ClientService.API.Authentication.Requests;
using FluentValidation;

namespace ClientService.API.Validators;

public class LogInRequestValidator : AbstractValidator<LogInRequest>
{
    public LogInRequestValidator()
    {
        RuleFor(l => l.Email).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
        RuleFor(l => l.Password).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
    }
}