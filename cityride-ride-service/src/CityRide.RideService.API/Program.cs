using CityRide.RideService.API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using CityRide.RideService.Domain.Entities;
using CityRide.RideService.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAppService();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var todoListAppContext = scope.ServiceProvider.GetRequiredService<RideServiceContext>();
    // Check if the database exists
    var databaseExists = todoListAppContext.Database.GetService<IRelationalDatabaseCreator>().Exists();

    // If the database does not exist, create it and perform migrations
    if (!databaseExists)
        try
        {
            todoListAppContext.Database.EnsureCreated(); // Create the database
            todoListAppContext.Database.Migrate(); // Apply any pending migrations
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Migration has failed: {ex.Message}");
        }

    var testRide = todoListAppContext.Rides.FirstOrDefault(b => b.RideId == 1);
    if (testRide == null)
        todoListAppContext.Rides.Add(new Ride
        {
            From = new Location { Latitude = 4, Longitude = 6 },
            To = new Location { Latitude = 5, Longitude = 1 },
            ClientId = 4,
            DriverId = 5,
            Status = RideStatus.Successful,
            Price = 5.3m
        });

    todoListAppContext.SaveChanges();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();