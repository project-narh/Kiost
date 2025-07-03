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
    public class WaitingService : IWaitingService
    {
        private readonly ServerdbContext _context;

        public WaitingService(ServerdbContext context)
        {
            _context = context;
        }

        public async Task<int> GetWaitTimeAsync(int people)
        {
            var occupiedTables = await _context.Tables
                .CountAsync(t => t.Status == "occupied");

            var activeReservations = await _context.Reservations
                .CountAsync(r => r.Status == "active");

            var availableTables = await _context.Tables
                .CountAsync(t => t.Status == "available" && t.Seats >= people);

            if (availableTables > 0)
            {
                return 0;
            }

            var avgDiningTime = await GetAverageDiningTimeAsync();
            int estimatedWaitTime = activeReservations * 15;

            return Math.Max(15, estimatedWaitTime);
        }

        public async Task<List<WaitingEntry>> GetWaitingListAsync()
        {
            var reservations = await _context.Reservations
                .Where(r => r.Status == "active")
                .OrderBy(r => r.Time)
                .ToListAsync();

            var result = new List<WaitingEntry>();
            int estimatedWaitTime = 0;

            foreach (var reservation in reservations)
            {
                result.Add(new WaitingEntry
                {
                    ReservationId = reservation.ReservationId,
                    People = reservation.People,
                    ReservationTime = reservation.Time,
                    Status = reservation.Status,
                    EstimatedWaitTime = estimatedWaitTime
                });

                estimatedWaitTime += 15;
            }

            return result;
        }

        private async Task<int> GetAverageDiningTimeAsync()
        {
            var completedVisits = await _context.VisitLogs
                .Where(v => v.ExitTime != null && v.TotalTime > 0)
                .Take(100)
                .ToListAsync();

            if (completedVisits.Any())
            {
                return (int)completedVisits.Average(v => v.TotalTime);
            }

            return 45;
        }
    }
}