using System.Timers;

using CityRide.DriverConsoleApp.Client.Requests;
using CityRide.DriverConsoleApp.Client.Services.Interfaces;

namespace CityRide.DriverConsoleApp.Client.Services;

public class RideRequestService : IRideRequestService
{
    private const int TimeToRespond = 10000;
    
    private readonly IDriverHubConnectionService _driverHubConnectionService;
    private bool _isTimeElapsed;
    private bool _isDecisionMade;

    public RideRequestService(IDriverHubConnectionService driverHubConnectionService)
    {
        _driverHubConnectionService = driverHubConnectionService;
    }

    public async Task StartRideRequestListener()
    {
        await _driverHubConnectionService.StartConnection();
    }

    public async Task StopRideRequestListener()
    {
        await _driverHubConnectionService.StopConnection();
    }

    public void ProcessRideRequest()
    {
        async void Handler(RideRequest r)
        {
            _isTimeElapsed = false;
            _isDecisionMade = false;
            
            // Set up a timer
            var timer = new System.Timers.Timer(TimeToRespond);
            timer.Elapsed += OnTimeElapsed;
            timer.AutoReset = false;
            timer.Start();

            // Display ride request
            Console.WriteLine($"New ride request: {r.Source.Latitude}, {r.Source.Longitude} -> {r.Destination.Latitude}, {r.Destination.Longitude}");

            // Get driver's decision
            bool? isRideAccepted = null;
            do
            {
                Console.Write("\nPress 1 to accept ride or 2 to decline it: ");
                isRideAccepted = Console.ReadKey().Key switch
                {
                    ConsoleKey.D1 => true,
                    ConsoleKey.D2 => false,
                    _ => isRideAccepted
                };
            } while (isRideAccepted == null && !_isTimeElapsed);

            _isDecisionMade = true;

            timer.Close();
            if (_isTimeElapsed)
            {
                return;
            }

            // If ride was declined
            if (!isRideAccepted!.Value)
            {
                await _driverHubConnectionService.SendDeclineRideRequest();
                Console.WriteLine("\nRide declined");
                return;
            }

            // If ride was accepted
            await _driverHubConnectionService.SendAcceptRideRequest();
            Console.WriteLine("\nRide accepted");
            
            // Start ride
            ConsoleKey? consoleKey;
            do
            {
                Console.Write("\nPress 1 to start the ride: ");
                consoleKey = Console.ReadKey().Key;
            } while (consoleKey != ConsoleKey.D1);
            await _driverHubConnectionService.SendStartRideRequest();
            Console.WriteLine("\nThe ride started");

            // End ride
            do
            {
                Console.Write("\nPress 1 to end the ride: ");
                consoleKey = Console.ReadKey().Key;
            } while (consoleKey != ConsoleKey.D1);
            await _driverHubConnectionService.SendStopRideRequest();
            Console.WriteLine("\nThe ride ended");
        }
        
        _driverHubConnectionService.AddOnReceiveRideRequestHandler(Handler);
    }

    private async void OnTimeElapsed(object? source, ElapsedEventArgs e)
    {
        if (!_isDecisionMade)
        {
            Console.Write("\nYour time to decide has ran out. Input 1 to continue: ");
            
            // Decline the ride
            await _driverHubConnectionService.SendDeclineRideRequest();
        }

        _isTimeElapsed = true;
    }
}