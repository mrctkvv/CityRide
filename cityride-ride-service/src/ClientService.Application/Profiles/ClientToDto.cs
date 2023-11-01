using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClientService.Domain.Dtos;
using ClientService.Domain.Entities;

namespace ClientService.Application.Profiles
{
    public class ClientToDto : Profile
    {
        public ClientToDto() {
            CreateMap<Client, ClientDto>().ReverseMap();
        }
    }
}
