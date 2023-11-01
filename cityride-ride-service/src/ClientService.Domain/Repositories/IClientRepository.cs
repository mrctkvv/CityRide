using ClientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Domain.Repositories
{
    public interface IClientRepository: IBaseRepository<Client>
    {
        Task<Client> GetClientViaEmailAndPasswordHash(string email, string passwordHash);
    }
}
