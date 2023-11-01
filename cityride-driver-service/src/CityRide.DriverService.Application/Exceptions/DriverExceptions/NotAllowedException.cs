using System.Net;

namespace CityRide.DriverService.Application.Exceptions.DriverExceptions;

public class NotAllowedException : DriverServiceExceptions
{
    private const string message = "You can only view and edit your own data";

    public NotAllowedException() :
        base(message, (int)HttpStatusCode.Unauthorized)
    {
    }

    public NotAllowedException(Exception innerException) :
        base(message, innerException, (int)HttpStatusCode.Unauthorized)
    {
    }
}