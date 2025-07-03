using Server.Services.IService;
using Server.Models;
using Server.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public class MenuService : IMenuService
    {
        private readonly ServerdbContext _context;

        public MenuService(ServerdbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetMenuAsync()
        {
            var menus = await _context.Menus.ToListAsync();

            return menus.Select(m => new MenuItem
            {
                MenuId = m.MenuId,
                Name = m.Name,
                AvgDuration = m.AvgDuration
            }).ToList();
        }

        public async Task<MenuItem> GetMenuByIdAsync(int menuId)
        {
            var menu = await _context.Menus.FindAsync(menuId);
            if (menu == null) return null;

            return new MenuItem
            {
                MenuId = menu.MenuId,
                Name = menu.Name,
                AvgDuration = menu.AvgDuration
            };
        }

        public async Task<MenuItem> CreateMenuAsync(MenuCreateRequest request)
        {
            var menu = new Menu
            {
                Name = request.Name,
                AvgDuration = request.AvgDuration
            };

            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();

            return new MenuItem
            {
                MenuId = menu.MenuId,
                Name = menu.Name,
                AvgDuration = menu.AvgDuration
            };
        }

        public async Task<bool> UpdateMenuAsync(int menuId, MenuUpdateRequest request)
        {
            var menu = await _context.Menus.FindAsync(menuId);
            if (menu == null) return false;

            menu.Name = request.Name;
            menu.AvgDuration = request.AvgDuration;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMenuAsync(int menuId)
        {
            var menu = await _context.Menus.FindAsync(menuId);
            if (menu == null) return false;

            // 주문 항목에서 사용 중인지 확인
            var hasOrders = await _context.OrderItems
                .AnyAsync(oi => oi.MenuId == menuId);

            if (hasOrders)
            {
                // 주문 기록이 있으면 삭제하지 않음
                return false;
            }

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
