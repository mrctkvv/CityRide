using AutoMapper;
using CityRide.ClientService.API.Client.Requests;
using ClientService.Domain.Dtos;

namespace ClientService.API.Profiles
{
    public class CreateClientRequestToDto : Profile
    {
        public CreateClientRequestToDto() {
            CreateMap<CreateClientRequest, ClientDto>();
        }
    }
}
