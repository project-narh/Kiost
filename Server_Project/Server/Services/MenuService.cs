using Server.Services.IService;
using System;

namespace Server.Services
{
    public class MenuService : IMenuService
    {
        private readonly AppDbContext _context;

        public MenuService(AppDbContext context)
        {
            _context = context;
        }

        public List<MenuItem> GetMenu()
        {
            return _context.Menus.ToList();
        }
    }

}
