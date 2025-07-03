using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet("available")]
        public IActionResult GetAvailableTables()
        {
            var tables = _tableService.GetAvailableTables();
            return Ok(new ApiResponse<List<Table>> { Success = true, Data = tables });
        }

        [HttpGet("status")]
        public IActionResult GetTableStatus()
        {
            var status = _tableService.GetTableStatus();
            return Ok(new ApiResponse<List<TableStatus>> { Success = true, Data = status });
        }

        [HttpPost("enter")]
        public IActionResult EnterTable([FromBody] TableEnterRequest request)
        {
            _tableService.EnterTable(request);
            return Ok(new ApiResponse<object> { Success = true, Message = "입장 처리되었습니다." });
        }

        [HttpPost("order")]
        public IActionResult Order([FromBody] TableOrderRequest request)
        {
            _tableService.Order(request);
            return Ok(new ApiResponse<object> { Success = true, Message = "주문이 기록되었습니다." });
        }

        [HttpPost("exit")]
        public IActionResult ExitTable([FromBody] TableExitRequest request)
        {
            _tableService.ExitTable(request);
            return Ok(new ApiResponse<object> { Success = true, Message = "퇴장 처리되었습니다." });
        }

        [HttpPost("force-exit")]
        public IActionResult ForceExitTable([FromBody] TableForceExitRequest request)
        {
            _tableService.ForceExitTable(request);
            return Ok(new ApiResponse<object> { Success = true, Message = "강제 종료되었습니다." });
        }
    }

}
