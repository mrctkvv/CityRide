using System.Net;

namespace ClientService.Application.Exceptions;

public class NotAllowedException : ClientServiceException
{
    private const string message = "You can only view and edit your own data";

    public NotAllowedException() : 
        base(message, (int)HttpStatusCode.Unauthorized) { }

    public NotAllowedException(Exception innerException) : 
        base(message, innerException, (int)HttpStatusCode.Unauthorized) { }
}