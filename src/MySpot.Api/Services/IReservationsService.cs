using MySpot.Api.Commands;
using MySpot.Api.DTO;

namespace MySpot.Api.Services;

public interface IReservationsService
{
    IEnumerable<ReservationDTO> GetWeekly();
    ReservationDTO Get(Guid id);
    Guid? Create(CreateReservation command);
    bool Update(ChangeReservationLicencePlate command);
    bool Delete(DeleteReservation command);
}