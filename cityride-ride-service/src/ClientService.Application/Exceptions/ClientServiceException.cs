using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Exceptions
{
    public class ClientServiceException : Exception
    {
        public int ErrorCode { get; set; }

        public ClientServiceException(string message, int code) : base(message)
        {
            ErrorCode = code;
        }

        public ClientServiceException(string message, Exception innerException, int code) : base(message, innerException)
        {
            ErrorCode = code;
        }
    }
}
