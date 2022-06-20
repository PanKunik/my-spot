using MySpot.Api.Repositories;
using MySpot.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services
       .AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpot>()
       .AddSingleton<IReservationsService, ReservationsService>()
       .AddSingleton<IClock, Clock>();

var app = builder.Build();
app.MapControllers();
app.Run();
