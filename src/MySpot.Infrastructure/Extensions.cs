using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Abstractions;
using MySpot.Core.Abstractions;
using MySpot.Infrastructure.DAL.Repositories;
using MySpot.Infrastructure.Exceptions;
using MySpot.Infrastructure.Logging;
using MySpot.Infrastructure.Security;
using MySpot.Infrastructure.Services;

[assembly: InternalsVisibleTo("MySpot.Tests.Unit")]
namespace MySpot.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        services.Configure<AppOptions>(configuration.GetRequiredSection("app"));

        services.AddSingleton<ExceptionMiddleware>();
        services.AddPostgres(configuration);
        // services.AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpot>();
        services.AddSingleton<IClock, Clock>();

        services.AddCustomLogging();
        services.AddSecurity();

        var infrastructureAssembly = typeof(AppOptions).Assembly;

        services.Scan(s => s.FromAssemblies(infrastructureAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        return app;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        configuration.GetRequiredSection(sectionName)
                     .Bind(options);
        return options;
    }
}