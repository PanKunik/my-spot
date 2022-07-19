using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Application.Services;

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
    public async Task<ActionResult<IEnumerable<ReservationDTO>>> Get()
    {
        return Ok(await _reservationsService.GetWeeklyAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReservationDTO>> Get(Guid id)
    {
        var reservation = await _reservationsService.GetAsync(id);

        if(reservation is null)
        {
            return NotFound();
        }

        return reservation;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateReservation command)
    {
        var id = await _reservationsService.CreateAsync(command with { ReservationId = Guid.NewGuid() });

        if(id is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { Id = id }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, ChangeReservationLicencePlate command)
    {
        var succedded = await _reservationsService.UpdateAsync(command with { ReservationId = id });

        if(!succedded)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var succedded = await _reservationsService.DeleteAsync(new DeleteReservation(id));

        if(!succedded)
        {
            return BadRequest();
        }

        return NoContent();
    }
}