using MySpot.Application;
using MySpot.Core;
using MySpot.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services
       .AddCore()
       .AddApplication()
       .AddInfrastructure(builder.Configuration);


builder.Host.UseSerilog((context, loggerConfiguration) =>
{
       loggerConfiguration.WriteTo
              .Console()
              .WriteTo
              .File("logs.txt")
              .WriteTo
              .Seq("http://localhost:5341");
});

var app = builder.Build();
app.UseInfrastructure();
app.MapControllers();
app.Run();
