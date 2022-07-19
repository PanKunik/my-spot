using MySpot.Application.Commands;
using MySpot.Application.DTO;

namespace MySpot.Application.Services;

public interface IReservationsService
{
    Task<IEnumerable<ReservationDTO>> GetWeeklyAsync();
    Task<ReservationDTO> GetAsync(Guid id);
    Task<Guid?> CreateAsync(CreateReservation command);
    Task<bool> UpdateAsync(ChangeReservationLicencePlate command);
    Task<bool> DeleteAsync(DeleteReservation command);
}