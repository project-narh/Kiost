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

        [HttpGet]
        public IActionResult GetMenu()
        {
            var menu = _menuService.GetMenu();
            return Ok(new ApiResponse<List<MenuItem>> { Success = true, Data = menu });
        }
    }

}
