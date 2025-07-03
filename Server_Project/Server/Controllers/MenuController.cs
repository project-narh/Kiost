using Microsoft.AspNetCore.Mvc;
using Server.Services.IService;
using Server.Models;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// 전체 메뉴 조회
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetMenu()
        {
            var menu = await _menuService.GetMenuAsync();
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = new { menu, count = menu.Count }
            });
        }

        /// <summary>
        /// 메뉴 상세 조회
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuById(int id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            if (menu == null)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "메뉴를 찾을 수 없습니다."
                });

            return Ok(new ApiResponse<MenuItem>
            {
                Success = true,
                Data = menu
            });
        }

        /// <summary>
        /// 메뉴 추가
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateMenu([FromBody] MenuCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "메뉴 정보가 올바르지 않습니다."
                });

            var menu = await _menuService.CreateMenuAsync(request);
            return Ok(new ApiResponse<MenuItem>
            {
                Success = true,
                Data = menu,
                Message = "메뉴가 추가되었습니다."
            });
        }

        /// <summary>
        /// 메뉴 수정
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(int id, [FromBody] MenuUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "메뉴 정보가 올바르지 않습니다."
                });

            var result = await _menuService.UpdateMenuAsync(id, request);
            if (!result)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "메뉴를 찾을 수 없습니다."
                });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "메뉴가 수정되었습니다."
            });
        }

        /// <summary>
        /// 메뉴 삭제
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var result = await _menuService.DeleteMenuAsync(id);
            if (!result)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "메뉴를 삭제할 수 없습니다. (주문 기록이 있거나 존재하지 않음)"
                });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "메뉴가 삭제되었습니다."
            });
        }
    }
}