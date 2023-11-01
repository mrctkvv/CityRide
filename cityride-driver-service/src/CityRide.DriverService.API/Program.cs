using CityRide.DriverService.API;
using CityRide.DriverService.Domain.Entities;
using CityRide.DriverService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using DriverStatus = CityRide.Events.Models.Enums.DriverStatus;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAppServices();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwagger();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/V1/swagger.json", "DriverApp"); });

using (var scope = app.Services.CreateScope())
{
    var appContext = scope.ServiceProvider.GetRequiredService<DriverServiceContext>();
    if (!appContext.Database.GetService<IRelationalDatabaseCreator>().Exists())
        try
        {
            appContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Migration has failed: {ex.Message}");
        }

    var testDriver = appContext.Drivers.FirstOrDefault(d => d.Id == 1);
    if (testDriver == null)
        appContext.Drivers.Add(new Driver
        {
            Email = "superdriver@gmail.com",
            Password = "/yvqEAQczrBU3WQXvHDo55btuU3663BGtTZQl0P6thk=",
            FirstName = "James",
            LastName = "Bond",
            PhoneNumber = "1234567890",
            CarClass = CityRide.Domain.Enums.CarClass.Comfort
        });
    var driverLocation = appContext.DriverLocations.FirstOrDefault(d => d.Id == 1);
    if (driverLocation == null)
    {
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);
        appContext.DriverLocations.Add(new DriverLocation
        {
            DriverId = 1,
            Id = 1,
            Location = geometryFactory.CreatePoint(new Coordinate(49.89008, 14.17057)),
            Status = DriverStatus.Available
        });
        appContext.DriverLocations.Add(new DriverLocation
        {
            DriverId = 8,
            Id = 4,
            Location = geometryFactory.CreatePoint(new Coordinate(49.89008, 14.17057)),
            Status = DriverStatus.Available
        });
        appContext.DriverLocations.Add(new DriverLocation
        {
            DriverId = 9,
            Id = 5,
            Location = geometryFactory.CreatePoint(new Coordinate(49.89008, 14.17057)),
            Status = DriverStatus.Available
        });
        appContext.DriverLocations.Add(new DriverLocation
        {
            DriverId = 10,
            Id = 6,
            Location = geometryFactory.CreatePoint(new Coordinate(49.89008, 14.17057)),
            Status = DriverStatus.Available
        });
    }

    appContext.SaveChanges();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();