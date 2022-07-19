using MySpot.Application;
using MySpot.Core;
using MySpot.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services
       .AddCore()
       .AddApplication()
       .AddInfrastructure(builder.Configuration)
       .AddControllers();

var app = builder.Build();
app.UseInfrastructure();
app.MapControllers();
app.Run();
