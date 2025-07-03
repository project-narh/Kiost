namespace Server.Services.IService
{
    public interface IReservationService
    {
        Reservation CreateReservation(ReservationRequest request);
        List<Reservation> GetReservations();
        void CancelReservation(int id);
    }
}
