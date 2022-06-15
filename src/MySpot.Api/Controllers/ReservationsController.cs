using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{    
    private readonly ReservationsService _service = new ReservationsService();

    [HttpGet]
    public ActionResult<IEnumerable<ReservationDTO>> Get()
    {
        return Ok(_service.GetWeekly());
    }

    [HttpGet("{id:guid}")]
    public ActionResult<ReservationDTO> Get(Guid id)
    {
        var reservation = _service.Get(id);

        if(reservation is null)
        {
            return NotFound();
        }

        return reservation;
    }

    [HttpPost]
    public IActionResult Post(CreateReservation command)
    {
        var id = _service.Create(command with { ReservationId = Guid.NewGuid() });

        if(id is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { Id = id }, null);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Put(Guid id, ChangeReservationLicencePlate command)
    {
        var succedded = _service.Update(command with { ReservationId = id });

        if(!succedded)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var succedded = _service.Delete(new DeleteReservation(id));

        if(!succedded)
        {
            return BadRequest();
        }

        return NoContent();
    }
}