using Mysqlx.Crud;
using Server.Services.IService;
using System;

namespace Server.Services
{
    public class TableService : ITableService
    {
        private readonly AppDbContext _context;

        public TableService(AppDbContext context)
        {
            _context = context;
        }

        public List<Table> GetAvailableTables()
        {
            return _context.Tables.Where(t => t.Status == "available").ToList();
        }

        public List<TableStatus> GetTableStatus()
        {
            return _context.Tables.Select(t => new TableStatus
            {
                TableId = t.TableId,
                Status = t.Status
            }).ToList();
        }

        public void EnterTable(TableEnterRequest request)
        {
            var table = _context.Tables.Find(request.TableId);
            if (table != null)
            {
                table.Status = "occupied";
                _context.SaveChanges();

                _context.VisitLogs.Add(new VisitLog
                {
                    TableId = request.TableId,
                    EnterTime = DateTime.Now
                });
                _context.SaveChanges();
            }
        }

        public void Order(TableOrderRequest request)
        {
            var order = new Order
            {
                TableId = request.TableId,
                OrderTime = DateTime.Now
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var item in request.MenuItems)
            {
                _context.OrderItems.Add(new OrderItem
                {
                    OrderId = order.OrderId,
                    MenuId = item.MenuId
                });
            }
            _context.SaveChanges();
        }

        public void ExitTable(TableExitRequest request)
        {
            var table = _context.Tables.Find(request.TableId);
            if (table != null)
            {
                table.Status = "available";
                _context.SaveChanges();

                var visit = _context.VisitLogs
                    .Where(v => v.TableId == request.TableId && v.ExitTime == null)
                    .OrderByDescending(v => v.EnterTime)
                    .FirstOrDefault();
                if (visit != null)
                {
                    visit.ExitTime = DateTime.Now;
                    visit.TotalTime = (int)(visit.ExitTime.Value - visit.EnterTime).TotalMinutes;
                    _context.SaveChanges();
                }
            }
        }

        public void ForceExitTable(TableForceExitRequest request)
        {
            var table = _context.Tables.Find(request.TableId);
            if (table != null)
            {
                table.Status = "available";
                _context.SaveChanges();
            }
        }

        // 설정
        public List<Table> GetTableConfig()
        {
            return _context.Tables.ToList();
        }

        public void AddTable(TableConfigRequest request)
        {
            var table = new Table
            {
                Seats = request.Seats,
                Status = "available"
            };
            _context.Tables.Add(table);
            _context.SaveChanges();
        }

        public void UpdateTable(int id, TableConfigRequest request)
        {
            var table = _context.Tables.Find(id);
            if (table != null)
            {
                table.Seats = request.Seats;
                _context.SaveChanges();
            }
        }

        public void DeleteTable(int id)
        {
            var table = _context.Tables.Find(id);
            if (table != null)
            {
                _context.Tables.Remove(table);
                _context.SaveChanges();
            }
        }
    }

}
