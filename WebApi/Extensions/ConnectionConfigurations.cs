using System;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Extensions;

public static class ConnectionConfigurations
{
    public static void AddConnectionConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<Infrastructure.Persistences.ApplicationDbContext>(options => options.UseNpgsql(connectionString));
    }
}
