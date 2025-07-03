using Server.Services.IService;
using System;

namespace Server.Services
{
    public class WaitingService : IWaitingService
    {
        private readonly AppDbContext _context;

        public WaitingService(AppDbContext context)
        {
            _context = context;
        }

        public int GetWaitTime(int people)
        {
            // 간단 예시: 현재 occupied 테이블 평균 잔여시간 + 대기열 * 평균회전
            int avgDiningTime = 30; // 기본값
            return avgDiningTime * _context.Reservations.Count(r => r.Status == "active");
        }

        public List<WaitingEntry> GetWaitingList()
        {
            return _context.Reservations
                .Where(r => r.Status == "active")
                .Select(r => new WaitingEntry
                {
                    Name = r.Name,
                    People = r.People,
                    ReservationTime = r.ReservationTime
                }).ToList();
        }
    }

}
