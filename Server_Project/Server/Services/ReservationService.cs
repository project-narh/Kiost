using Server.Services.IService;
using System;

namespace Server.Services
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;

        public ReservationService(AppDbContext context)
        {
            _context = context;
        }

        public Reservation CreateReservation(ReservationRequest request)
        {
            var reservation = new Reservation
            {
                Name = request.Name,
                Phone = request.Phone,
                People = request.People,
                ReservationTime = request.ReservationTime,
                Status = "active"
            };
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return reservation;
        }

        public List<Reservation> GetReservations()
        {
            return _context.Reservations.Where(r => r.Status == "active").ToList();
        }

        public void CancelReservation(int id)
        {
            var res = _context.Reservations.Find(id);
            if (res != null)
            {
                res.Status = "canceled";
                _context.SaveChanges();
            }
        }
    }

