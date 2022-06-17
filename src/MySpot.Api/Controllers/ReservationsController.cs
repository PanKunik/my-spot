using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{    
    private static readonly ReservationsService _service = new ReservationsService(new Clock(), new WeeklyParkingSpot[]
    {
        new (Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(new Clock().Current()), "P1"),
        new (Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(new Clock().Current()), "P2"),
        new (Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(new Clock().Current()), "P3"),
        new (Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(new Clock().Current()), "P4"),
        new (Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(new Clock().Current()), "P5"),
    });

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