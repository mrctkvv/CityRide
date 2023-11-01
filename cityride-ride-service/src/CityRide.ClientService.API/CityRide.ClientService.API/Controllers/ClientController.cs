using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClientService.Domain;
using ClientService.Infrastructure;
using ClientService.Application.Services.Interfaces;
using CityRide.ClientService.API.Client.Responses;
using System.Reflection.Metadata.Ecma335;
using ClientService.Domain.Dtos;
using CityRide.ClientService.API.Client.Requests;

using System.Runtime.CompilerServices;
using ClientService.Application.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace ClientService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ClientResponse>> CreateClient(CreateClientRequest req)
        {
            var createdClient = await _clientService.CreateClientAsync(_mapper.Map<ClientDto>(req));
            return Ok(_mapper.Map<ClientResponse>(createdClient));
        }
        
        [HttpGet]
        public async Task<ActionResult<ClientResponse>> GetClientProfile(int clientId)
        {
            if (clientId != _clientService.CurrentClientId)
            {
                throw new NotAllowedException();
            }
            
            var client = await _clientService.GetClientProfile(clientId);
            return Ok(_mapper.Map<ClientResponse>(client));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateClient(UpdateClientRequest req)
        {
            if (req.ID != _clientService.CurrentClientId)
            {
                throw new NotAllowedException();
            }
            
            var clientDto = _mapper.Map<ClientDto>(req);
            await _clientService.UpdateClientAsync(clientDto);
            return NoContent();
        }
        
        [HttpDelete]
        public async Task<ActionResult> DeleteClient(int clientId)
        { 
            if (clientId != _clientService.CurrentClientId)
            {
                throw new NotAllowedException();
            }
            
            await _clientService.DeleteClientAsync(clientId);
            return NoContent(); 
        }
    }
}
