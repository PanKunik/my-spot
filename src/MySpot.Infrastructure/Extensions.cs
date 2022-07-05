using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Services;
using MySpot.Infrastructure.DAL.Repositories;
using MySpot.Infrastructure.Services;

[assembly:InternalsVisibleTo("MySpot.Tests.Unit")]
namespace MySpot.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres(configuration);
        // services.AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpot>();
        services.AddSingleton<IClock, Clock>();

        return services;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        configuration.GetRequiredSection(sectionName)
                     .Bind(options);
        return options;
    }
}