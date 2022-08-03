using MySpot.Application.Commands;
using MySpot.Application.DTO;

namespace MySpot.Application.Services;

public interface IReservationsService
{
    Task<IEnumerable<ReservationDTO>> GetWeeklyAsync();
    Task<ReservationDTO> GetAsync(Guid id);
    Task ReserveForVehicleAsync(ReserveParkingSpotForVehicle command);
    Task ReserveForCleaningAsync(ReserveParkingSpotForCleaning command);
    Task ChangeReservationLicencePlateAsync(ChangeReservationLicencePlate command);
    Task DeleteAsync(DeleteReservation command);
}