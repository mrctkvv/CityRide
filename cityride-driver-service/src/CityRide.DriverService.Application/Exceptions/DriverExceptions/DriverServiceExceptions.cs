namespace CityRide.DriverService.Application.Exceptions.DriverExceptions;

public class DriverServiceExceptions : Exception
{
    public int StatusCode { get; set; }

    public DriverServiceExceptions(string message, int code)
        : base(message)
    {
        StatusCode = code;
    }

    public DriverServiceExceptions(string message, Exception innerException, int code)
        : base(message, innerException)
    {
        StatusCode = code;
    }

    public DriverServiceExceptions(DriverServiceExceptions ex)
    {
    }
}