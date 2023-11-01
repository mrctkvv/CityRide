using AutoMapper;
using CityRide.ClientService.API.Client.Responses;
using ClientService.Domain.Dtos;

namespace ClientService.API.Profiles
{
    public class DtoToClientResponse : Profile
    {
        public DtoToClientResponse() {
            CreateMap<ClientDto, ClientResponse>();
        }
    }
}
