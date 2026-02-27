using DeskHub.Application.Interfaces.Services;
using DeskHub.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DeskHub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDeskService, DeskService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}
