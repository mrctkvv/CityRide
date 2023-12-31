using Microsoft.OpenApi.Models;
using ClientService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ClientService.Domain.Entities;
using ClientService.Application.Services;

namespace ClientService.API;

public class Program {
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAppServices();
        builder.Services.AddJWTAuthentication(builder.Configuration);
        builder.Services.AddControllers();
        builder.Services.AddSwagger();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSignalR();
        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/V1/swagger.json", "ClientApp");
            });

        using (var scope = app.Services.CreateScope())
        {
            var appContext = scope.ServiceProvider.GetRequiredService<ClientServiceContext>();
            if (!appContext.Database.GetService<IRelationalDatabaseCreator>().Exists())
            {
                try {
                    appContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Magration has failed: {ex.Message}");
                }
            }

            var testClient = appContext.Clients.FirstOrDefault(b => b.ID ==1);
            if (testClient == null)
            {
                appContext.Clients.Add(new Client
                {
                    Email = "janedoe@gmail.com",
                    Password = "/yvqEAQczrBU3WQXvHDo55btuU3663BGtTZQl0P6thk=",
                    FirstName = "Jane",
                    LastName = "Doe",
                    PhoneNumber = "0232415789"
                });
            }
            appContext.SaveChanges();
        }

        //app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapHub<ClientHub>("/ClientHub");
      
        app.MapControllers();

        app.Run();
    }
}