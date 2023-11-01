using ClientService.Domain.Entities;
using ClientService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Infrastructure
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(ClientServiceContext clientServiceContext) : base(clientServiceContext) { }
        public async Task<Client> GetClientViaEmailAndPasswordHash(string email, string passwordHash)
        {
            return await _context.Clients.Where(c => c.Email == email && c.Password == passwordHash).FirstOrDefaultAsync();
        }
    }
}
