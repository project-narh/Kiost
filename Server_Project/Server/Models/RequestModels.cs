using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class KioskStatusRequest
    {
        [Required]
        public string Status { get; set; } = "on";
    }

    // 새로 추가된 메뉴 관리 요청 모델들
    public class MenuCreateRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 300)]
        public int AvgDuration { get; set; }
    }

    public class MenuUpdateRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 300)]
        public int AvgDuration { get; set; }
    }

    // 실제 DB에 맞춘 예약 요청 (name, phone 제거)
    public class ReservationRequest
    {
        [Required]
        [Range(1, 20)]
        public int People { get; set; }

        [Required]
        public DateTime ReservationTime { get; set; }

        public string? SpecialRequests { get; set; }
    }

    // 실제 DB에 맞춘 테이블 입장 요청 (customerName 제거)
    public class TableEnterRequest
    {
        [Required]
        public int TableId { get; set; }

        [Required]
        [Range(1, 20)]
        public int People { get; set; }

        public int? ReservationId { get; set; }
    }

    public class TableOrderRequest
    {
        [Required]
        public int TableId { get; set; }

        [Required]
        public List<OrderMenuItem> MenuItems { get; set; } = new();

        public string? SpecialRequests { get; set; }
    }

    public class TableExitRequest
    {
        [Required]
        public int TableId { get; set; }

        public decimal? TotalAmount { get; set; }
        public string? PaymentMethod { get; set; }
    }

    public class TableForceExitRequest
    {
        [Required]
        public int TableId { get; set; }

        [Required]
        public string Reason { get; set; } = string.Empty;
    }

    public class TableConfigRequest
    {
        [Required]
        [Range(1, 20)]
        public int Seats { get; set; }

        public string Status { get; set; } = "available";
    }
}