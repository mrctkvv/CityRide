using ClientService.Domain.Dtos;

namespace ClientService.Application.Services.Interfaces;

public interface IAuthenticationService
{
    string GetTokenString(ClientDto clientDto, TimeSpan expirationPeriod);
}