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
        await _reservationsService.CreateAsync(command with { ReservationId = Guid.NewGuid() });
        return CreatedAtAction(nameof(Get), new { Id = command.ReservationId }, default);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, ChangeReservationLicencePlate command)
    {
        await _reservationsService.UpdateAsync(command with { ReservationId = id });
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _reservationsService.DeleteAsync(new DeleteReservation(id));
        return NoContent();
    }
}