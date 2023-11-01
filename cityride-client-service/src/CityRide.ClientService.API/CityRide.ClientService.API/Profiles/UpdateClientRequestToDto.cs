using AutoMapper;
using ClientService.Domain.Dtos;
using CityRide.ClientService.API.Client.Requests;

namespace ClientService.API.Profiles
{
    public class UpdateClientRequestToDto : Profile
    {
        public UpdateClientRequestToDto() {
            CreateMap<UpdateClientRequest, ClientDto>();
        }
    }
}
