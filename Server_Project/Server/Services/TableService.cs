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
    public class TableService : ITableService
    {
        private readonly ServerdbContext _context;

        public TableService(ServerdbContext context)
        {
            _context = context;
        }

        public async Task<List<Table>> GetAvailableTablesAsync()
        {
            return await _context.Tables
                .Where(t => t.Status == "available")
                .ToListAsync();
        }

        public async Task<List<TableStatus>> GetTableStatusAsync()
        {
            var tables = await _context.Tables.ToListAsync();
            var result = new List<TableStatus>();

            foreach (var table in tables)
            {
                var status = new TableStatus
                {
                    TableId = table.TableId,
                    Status = table.Status,
                    Seats = table.Seats
                };

                if (table.Status == "occupied")
                {
                    var latestVisit = await _context.VisitLogs
                        .Where(v => v.TableId == table.TableId && v.ExitTime == null)
                        .OrderByDescending(v => v.EnterTime)
                        .FirstOrDefaultAsync();

                    if (latestVisit != null)
                    {
                        status.OccupiedSince = latestVisit.EnterTime;
                        status.CurrentGuests = table.Seats;
                    }
                }

                result.Add(status);
            }

            return result;
        }

        public async Task<bool> EnterTableAsync(TableEnterRequest request)
        {
            var table = await _context.Tables.FindAsync(request.TableId);
            if (table == null || table.Status != "available") return false;

            table.Status = "occupied";
            await _context.SaveChangesAsync();

            var visitLog = new VisitLog
            {
                TableId = request.TableId,
                EnterTime = DateTime.UtcNow,
                TotalTime = 0
            };
            _context.VisitLogs.Add(visitLog);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> OrderAsync(TableOrderRequest request)
        {
            var order = new OrderData
            {
                TableId = request.TableId,
                OrderTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // 주문 상세 항목 추가: Count 만큼 레코드 생성
            foreach (var item in request.MenuItems)
            {
                for (int i = 0; i < item.Count; i++)  // quantity → count로 변경
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.OrderId,
                        MenuId = item.MenuId
                        // quantity 필드 제거
                    };
                    _context.OrderItems.Add(orderItem);
                }
            }
            await _context.SaveChangesAsync();

            return order.OrderId;
        }

        public async Task<bool> ExitTableAsync(TableExitRequest request)
        {
            var table = await _context.Tables.FindAsync(request.TableId);
            if (table == null || table.Status != "occupied") return false;

            table.Status = "available";
            await _context.SaveChangesAsync();

            var visitLog = await _context.VisitLogs
                .Where(v => v.TableId == request.TableId && v.ExitTime == null)
                .OrderByDescending(v => v.EnterTime)
                .FirstOrDefaultAsync();

            if (visitLog != null)
            {
                visitLog.ExitTime = DateTime.UtcNow;
                visitLog.TotalTime = (int)(visitLog.ExitTime.Value - visitLog.EnterTime).TotalMinutes;
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task ForceExitTableAsync(TableForceExitRequest request)
        {
            var table = await _context.Tables.FindAsync(request.TableId);
            if (table != null)
            {
                table.Status = "available";
                await _context.SaveChangesAsync();

                var visitLog = await _context.VisitLogs
                    .Where(v => v.TableId == request.TableId && v.ExitTime == null)
                    .OrderByDescending(v => v.EnterTime)
                    .FirstOrDefaultAsync();

                if (visitLog != null)
                {
                    visitLog.ExitTime = DateTime.UtcNow;
                    visitLog.TotalTime = (int)(visitLog.ExitTime.Value - visitLog.EnterTime).TotalMinutes;
                    await _context.SaveChangesAsync();
                }

                Console.WriteLine($"테이블 {request.TableId} 강제 종료: {request.Reason}");
            }
        }

        public async Task<List<Table>> GetTableConfigAsync()
        {
            return await _context.Tables.ToListAsync();
        }

        public async Task<int> AddTableAsync(TableConfigRequest request)
        {
            var table = new Table
            {
                Seats = request.Seats,
                Status = request.Status
            };

            _context.Tables.Add(table);
            await _context.SaveChangesAsync();
            return table.TableId;
        }

        public async Task<bool> UpdateTableAsync(int id, TableConfigRequest request)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null) return false;

            table.Seats = request.Seats;
            if (table.Status != "occupied")
            {
                table.Status = request.Status;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTableAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null || table.Status == "occupied") return false;

            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}