using DeskHub.Application.Interfaces.Persistence;
using DeskHub.Infrastructure.Data;
using DeskHub.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeskHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("Database"));
        });

        services.AddScoped<IDeskRepository, DeskRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
