using ClientService.Domain.Dtos;

namespace ClientService.Application.Services.Interfaces
{
    public interface IClientService
    {
        Task<ClientDto> CreateClientAsync(ClientDto clientDto);
        Task UpdateClientAsync(ClientDto clientDto);
        Task DeleteClientAsync(int clientId);
        Task<ClientDto> GetClientProfile(int clientId);
        Task<ClientDto?> GetClientByEmailAndPassword(string email, string password);
        int? CurrentClientId { get; }
    }
}
