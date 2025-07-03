using Server.Services.IService;
using Server.Models;
using Server.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ServerdbContext _context;

        public ReservationService(ServerdbContext context)
        {
            _context = context;
        }

        public async Task<ReservationInfo> CreateReservationAsync(ReservationRequest request)
        {
            var reservation = new Reservation
            {
                Name = request.Name,
                Phone = request.Phone,
                People = request.People,
                Time = request.ReservationTime,
                Status = "active",
                CreatedAt = DateTime.UtcNow
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return new ReservationInfo
            {
                ReservationId = reservation.ReservationId,
                Name = reservation.Name,
                Phone = reservation.Phone,
                People = reservation.People,
                ReservationTime = reservation.Time,
                Status = reservation.Status,
                CreatedAt = reservation.CreatedAt
            };
        }

        public async Task<List<ReservationInfo>> GetReservationsAsync()
        {
            var reservations = await _context.Reservations
                .Where(r => r.Status == "active")
                .OrderBy(r => r.Time)
                .ToListAsync();

            return reservations.Select(r => new ReservationInfo
            {
                ReservationId = r.ReservationId,
                Name = r.Name,
                Phone = r.Phone,
                People = r.People,
                ReservationTime = r.Time,
                Status = r.Status,
                CreatedAt = r.CreatedAt
            }).ToList();
        }

        public async Task<bool> CancelReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return false;

            reservation.Status = "canceled";
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
