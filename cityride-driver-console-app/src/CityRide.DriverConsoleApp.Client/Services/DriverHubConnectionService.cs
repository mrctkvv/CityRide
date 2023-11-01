using Microsoft.AspNetCore.SignalR.Client;

using CityRide.DriverConsoleApp.Client.Requests;
using CityRide.DriverConsoleApp.Client.Services.Interfaces;

namespace CityRide.DriverConsoleApp.Client.Services;

public class DriverHubConnectionService : IDriverHubConnectionService
{
    private readonly HubConnection _hubConnection;

    public DriverHubConnectionService(HubConnection hubConnection)
    {
        _hubConnection = hubConnection;
    }

    public async Task StartConnection()
    {
        await _hubConnection.StartAsync();
    }

    public async Task StopConnection()
    {
        await _hubConnection.StopAsync();
    }

    public void AddOnReceiveRideRequestHandler(Action<RideRequest> handler)
    {
        _hubConnection.On("ReceivePersonalRide", handler);
    }

    public async Task SendAcceptRideRequest()
    {
        await _hubConnection.SendAsync("AcceptRide");
    }

    public async Task SendDeclineRideRequest()
    {
        await _hubConnection.SendAsync("DeclineRide");
    }

    public async Task SendStartRideRequest()
    {
        await _hubConnection.SendAsync("StartRide");
    }

    public async Task SendStopRideRequest()
    {
        await _hubConnection.SendAsync("StopRide");
    }
}