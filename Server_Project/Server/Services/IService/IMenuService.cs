using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Services.IService
{
    public interface IMenuService
    {
        Task<List<MenuItem>> GetMenuAsync();
        Task<MenuItem> GetMenuByIdAsync(int menuId);
        Task<MenuItem> CreateMenuAsync(MenuCreateRequest request);
        Task<bool> UpdateMenuAsync(int menuId, MenuUpdateRequest request);
        Task<bool> DeleteMenuAsync(int menuId);
    }
}