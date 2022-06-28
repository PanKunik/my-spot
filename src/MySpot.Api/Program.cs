using MySpot.Application;
using MySpot.Core;
using MySpot.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services
       .AddCore()
       .AddApplication()
       .AddInfrastructure()
       .AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
