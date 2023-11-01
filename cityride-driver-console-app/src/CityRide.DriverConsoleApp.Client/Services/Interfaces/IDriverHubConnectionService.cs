using CityRide.DriverConsoleApp.Client.Requests;

namespace CityRide.DriverConsoleApp.Client.Services.Interfaces;

public interface IDriverHubConnectionService
{
    Task StartConnection();
    Task StopConnection();
    void AddOnReceiveRideRequestHandler(Action<RideRequest> handler);
    
    // Should these requests have bodies?
    Task SendAcceptRideRequest();
    Task SendDeclineRideRequest();
    Task SendStartRideRequest();
    Task SendStopRideRequest();
}