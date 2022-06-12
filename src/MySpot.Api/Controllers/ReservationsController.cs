using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{    
    private readonly ReservationsService _service = new ReservationsService();

    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> Get()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> Get(int id)
    {
        var reservation = _service.Get(id);

        if(reservation is null)
        {
            return NotFound();
        }

        return reservation;
    }

    [HttpPost]
    public IActionResult Post(Reservation reservation)
    {
        var id = _service.Create(reservation);

        if(id is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { Id = id }, null);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Reservation reservation)
    {
        reservation.Id = id;
        var succedded = _service.Update(reservation);

        if(!succedded)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var succedded = _service.Delete(id);

        if(!succedded)
        {
            return BadRequest();
        }

        return NoContent();
    }
}