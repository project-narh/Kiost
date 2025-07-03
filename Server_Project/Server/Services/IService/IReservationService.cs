using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Services.IService
{
    public interface IReservationService
    {
        Task<ReservationInfo> CreateReservationAsync(ReservationRequest request);
        Task<List<ReservationInfo>> GetReservationsAsync();
        Task<bool> CancelReservationAsync(int id);
    }
}
