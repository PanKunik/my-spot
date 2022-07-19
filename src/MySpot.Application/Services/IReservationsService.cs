using MySpot.Application.Commands;
using MySpot.Application.DTO;

namespace MySpot.Application.Services;

public interface IReservationsService
{
    Task<IEnumerable<ReservationDTO>> GetWeeklyAsync();
    Task<ReservationDTO> GetAsync(Guid id);
    Task CreateAsync(CreateReservation command);
    Task UpdateAsync(ChangeReservationLicencePlate command);
    Task DeleteAsync(DeleteReservation command);
}