using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Abstractions;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Decorators;

namespace MySpot.Infrastructure.DAL.Repositories;

internal static class Extensions
{
    private const string OptionsSectionName = "Postgres";
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetRequiredSection(OptionsSectionName));
        var postgresOptions = configuration.GetOptions<PostgresOptions>(OptionsSectionName);
        services.AddDbContext<MySpotDbContext>(x => x.UseNpgsql(postgresOptions.ConnectionString));
        services.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
        services.AddScoped<IUserRepository, PostgresUserRepository>();
        services.AddHostedService<DatabaseInitializer>();
        services.AddScoped<IUnitOfWork, PostgresUnitOfWork>();

        var b = services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }
}