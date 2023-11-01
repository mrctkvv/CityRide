using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Exceptions
{
    public class ClientNotFoundException : ClientServiceException
    {
        public ClientNotFoundException()
            : base("CLient not found", (int)HttpStatusCode.NotFound)
        { 
        }

        public ClientNotFoundException(string message)
            :base(message, (int)HttpStatusCode.NotFound) { }

        public ClientNotFoundException(int clientId)
           : base($"Client with id {clientId} not found", (int)HttpStatusCode.NotFound)
        {

        }

        public ClientNotFoundException(string message, Exception innerException)
            : base (message, innerException, (int)HttpStatusCode.NotFound) { }
    }
}
