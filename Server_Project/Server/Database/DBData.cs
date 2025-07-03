using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public enum ItemState
{
    USE, TRADE, DESTROY, LOCK
}

public enum ItemType
{
    EQUIPMENT, CONSUMABLE, MATERIAL
}

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
}
[Table("orderData")]
public class orderdata
{
    [Key]
    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("table_id")]
    public int TableId { get; set; }

    [Column("order_time")]
    public int OrderTime { get; set; }
}

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
}

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

[Table("Visitlog")]
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

[Table("public_item")]
public class PublicItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("tokenId")]
    public int TokenId { get; set; }

    [Column("id")]
    public int Id { get; set; } // itemId

    [Column("uniqueId")]
    public int UniqueId { get; set; } // 시리얼

    [Column("uid")]
    public int Uid { get; set; } // 생성자

    [Column("count")]
    public int Count { get; set; }

    [Column("type", TypeName = "ENUM('CONSUMABLE','PERMANENT')")]
    public string Type { get; set; }

    [Column("privateId")]
    public int PrivateTokenId { get; set; }

    [Column("hash")]
    public string Hash { get; set; }

    [Column("timestamp")]
    public long Timestamp { get; set; }
    public int MintType { get; set; }

    [Column("set_size")]
    public int? SetSize { get; set; }

    [Column("item_ids")]
    public string? ItemIds { get; set; }

    [Column("unique_ids")]
    public string? UniqueIds { get; set; }
    [Column("state", TypeName = "ENUM('USE', 'TRADE', 'DESTROY', 'LOCK')")]
    public string State { get; set; }
}


public class UserItemDTO
{
    public int Id { get; set; }
    public int UniqueId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string State { get; set; }
    public string Type { get; set; }
    public int Count { get; set; }
    public int Uid { get; set; }
    public string Etc { get; set; }
    public string Image { get; set; }
    public int TokenId { get; set; }

}
public class ItemSet
{
    [JsonPropertyName("itemId")]
    public int itemId { get; set; }

    [JsonPropertyName("uniqueId")]
    public int uniqueId { get; set; }

    [JsonPropertyName("count")]
    public int count { get; set; }
}


