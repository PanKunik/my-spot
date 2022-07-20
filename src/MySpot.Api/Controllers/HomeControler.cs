using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MySpot.Infrastructure;

namespace MySpot.Api.Controllers;

[Route("")]
public class HomeController : ControllerBase
{
    private readonly AppOptions _appOptions;

    public HomeController(IOptionsMonitor<AppOptions> appOptions)
    {
        _appOptions = appOptions.CurrentValue;
    }

    [HttpGet]
    public ActionResult Get() => Ok(_appOptions.Name);
}