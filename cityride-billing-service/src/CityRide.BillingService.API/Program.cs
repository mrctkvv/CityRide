using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.OpenApi.Models;

using CityRide.BillingService.API;
using CityRide.BillingService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddDbContext<BillingServiceContext>();
builder.Services.AddRepositories();
builder.Services.AddMappingProfiles();
builder.Services.AddServices();
builder.Services.AddValidators();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options => 
    options.SwaggerDoc("V1", new OpenApiInfo
    {
        Version = "V1",
        Title = "Billing Service",
        Description = "Billing Service Web API"
    }));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/swagger/V1/swagger.json", "Billing Service");
});

using (var scope = app.Services.CreateScope())
{
    var billingServiceAppContext = scope.ServiceProvider.GetRequiredService<BillingServiceContext>();
    if (!billingServiceAppContext.Database.GetService<IRelationalDatabaseCreator>().Exists())
    {
        try
        {
            billingServiceAppContext.Database.Migrate();
            Console.WriteLine($"Migrated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Migration has failed: {ex.Message}");
        }
    }

    billingServiceAppContext.SaveChanges();
}

app.MapControllers();
app.Run();