namespace CityRide.RideService.Domain.Entities;

// The main thought of making Location a record there it have not got any functional responsibilities 
public class Location
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}