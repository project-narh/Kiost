using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Database
{
    // 실제 DB 스키마에 맞춘 Menu 엔티티
    [Table("menu")]
    public class Menu
    {
        [Key]
        [Column("menu_id")]
        public int MenuId { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("avg_duration")]
        public int AvgDuration { get; set; }

        // 제거된 필드들:
        // - price, category, description, is_available
    }

    // 실제 DB 스키마에 맞춘 OrderItem 엔티티
    [Table("orderitem")]
    public class OrderItem
    {
        [Key]
        [Column("order_item_id")]
        public int OrderItemId { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("menu_id")]
        public int MenuId { get; set; }

        // 제거된 필드:
        // - quantity (개수만큼 레코드를 생성하는 방식으로 변경)
    }

    // 실제 DB 스키마에 맞춘 Reservation 엔티티
    [Table("reservation")]
    public class Reservation
    {
        [Key]
        [Column("reservation_id")]
        public int ReservationId { get; set; }

        [Column("people")]
        public int People { get; set; }

        [Column("time")]
        public DateTime Time { get; set; }

        [Column("status")]
        public string Status { get; set; } = string.Empty;

        // 제거된 필드들:
        // - name, phone, created_at (번호 기반 관리)
    }

    // 변경되지 않은 엔티티들
    [Table("orderdata")]
    public class OrderData
    {
        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("table_id")]
        public int TableId { get; set; }

        [Column("order_time")]
        public int OrderTime { get; set; }
    }

    [Table("table")]
    public class Table
    {
        [Key]
        [Column("table_id")]
        public int TableId { get; set; }

        [Column("status")]
        public string Status { get; set; } = string.Empty;

        [Column("seats")]
        public int Seats { get; set; }
    }

    [Table("visitlog")]
    public class VisitLog
    {
        [Key]
        [Column("visit_id")]
        public int VisitId { get; set; }

        [Column("table_id")]
        public int TableId { get; set; }

        [Column("enter_time")]
        public DateTime EnterTime { get; set; }

        [Column("exit_time")]
        public DateTime? ExitTime { get; set; }

        [Column("total_time")]
        public int TotalTime { get; set; }
    }

    public class ConnectAccount
    {
        public string DB { get; set; } = string.Empty;
    }
}