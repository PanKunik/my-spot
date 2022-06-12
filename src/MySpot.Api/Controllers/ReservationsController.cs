using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private static string[] _parkingSpotNames = { "P1", "P2", "P3", "P4", "P5" };
    private static readonly List<Reservation> _reservation = new();

    private static int Id = 1;
    
    [HttpGet]
    public IEnumerable<Reservation> Get()
    {
        return _reservation;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> Get(int id)
    {
        var reservation = _reservation.SingleOrDefault(r => r.Id == id);
        
        if(reservation is null)
        {
            return NotFound();
        }

        return reservation;
    }

    [HttpPost]
    public IActionResult Post(Reservation reservation)
    {
        reservation.Id = Id;
        reservation.Date = DateTime.Now.AddDays(1).Date;

        if(_parkingSpotNames.All(ps => reservation.ParkingSpotName != reservation.ParkingSpotName))
        {
            return BadRequest();
        }

        if(_reservation.Any(r => r.Date.Date == reservation.Date.Date && r.ParkingSpotName == reservation.ParkingSpotName))
        {
            return BadRequest();
        }

        _reservation.Add(reservation);
        Id++;

        return CreatedAtAction(nameof(Get), new { Id = reservation.Id }, null);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Reservation reservation)
    {
        var existingReservation = _reservation.SingleOrDefault(r => r.Id == id);

        if(existingReservation is null)
        {
            return NotFound();
        }

        existingReservation.LicencePlate = reservation.LicencePlate;
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var existingReservation = _reservation.SingleOrDefault(r => r.Id == id);

        if(existingReservation is null)
        {
            return NotFound();
        }

        _reservation.Remove(existingReservation);
        return NoContent();
    }
}