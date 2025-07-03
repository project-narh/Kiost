using System;
using System.Collections.Generic;

namespace Server.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    // 실제 DB에 맞춘 MenuItem
    public class MenuItem
    {
        public int MenuId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int AvgDuration { get; set; }
        // 제거된 필드들: Price, Category, Description, IsAvailable
    }

    // 주문 방식 변경: quantity → count
    public class OrderMenuItem
    {
        public int MenuId { get; set; }
        public int Count { get; set; } = 1; // quantity에서 count로 변경
        public string? SpecialRequests { get; set; }
    }

    // 실제 DB에 맞춘 ReservationInfo
    public class ReservationInfo
    {
        public int ReservationId { get; set; }
        public int People { get; set; }
        public DateTime ReservationTime { get; set; }
        public string Status { get; set; } = string.Empty;
        // 제거된 필드들: Name, Phone, CreatedAt
    }

    public class TableStatus
    {
        public int TableId { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Seats { get; set; }
        public DateTime? OccupiedSince { get; set; }
        public int? CurrentGuests { get; set; }
    }

    public class WaitingEntry
    {
        public int ReservationId { get; set; }
        public int People { get; set; }
        public DateTime ReservationTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public int EstimatedWaitTime { get; set; }
    }

    public class OrderInfo
    {
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public DateTime OrderTime { get; set; }
        public List<OrderItemInfo> Items { get; set; } = new();
    }

    public class OrderItemInfo
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; } = string.Empty;
        // quantity 필드 제거
    }
}
