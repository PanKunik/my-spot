using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{    
    private readonly IReservationsService _reservationsService;

    public ReservationsController(IReservationsService reservationsService)
    {
        _reservationsService = reservationsService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReservationDTO>> Get()
    {
        return Ok(_reservationsService.GetWeekly());
    }

    [HttpGet("{id:guid}")]
    public ActionResult<ReservationDTO> Get(Guid id)
    {
        var reservation = _reservationsService.Get(id);

        if(reservation is null)
        {
            return NotFound();
        }

        return reservation;
    }

    [HttpPost]
    public IActionResult Post(CreateReservation command)
    {
        var id = _reservationsService.Create(command with { ReservationId = Guid.NewGuid() });

        if(id is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { Id = id }, null);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Put(Guid id, ChangeReservationLicencePlate command)
    {
        var succedded = _reservationsService.Update(command with { ReservationId = id });

        if(!succedded)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var succedded = _reservationsService.Delete(new DeleteReservation(id));

        if(!succedded)
        {
            return BadRequest();
        }

        return NoContent();
    }
}