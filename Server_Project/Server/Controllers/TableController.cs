using Microsoft.AspNetCore.Mvc;
using Server.Services.IService;
using Server.Models;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/table")]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableTables()
        {
            var tables = await _tableService.GetAvailableTablesAsync();
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = new { tables, count = tables.Count }
            });
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetTableStatus()
        {
            var status = await _tableService.GetTableStatusAsync();
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = new { tables = status, count = status.Count }
            });
        }

        [HttpPost("enter")]
        public async Task<IActionResult> EnterTable([FromBody] TableEnterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "입력 정보가 올바르지 않습니다."
                });

            var result = await _tableService.EnterTableAsync(request);
            if (!result)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "테이블 입장 처리에 실패했습니다."
                });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "입장 처리가 완료되었습니다."
            });
        }

        [HttpPost("order")]
        public async Task<IActionResult> Order([FromBody] TableOrderRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "주문 정보가 올바르지 않습니다."
                });

            var orderId = await _tableService.OrderAsync(request);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = new { orderId },
                Message = "주문이 접수되었습니다."
            });
        }

        [HttpPost("exit")]
        public async Task<IActionResult> ExitTable([FromBody] TableExitRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "퇴장 정보가 올바르지 않습니다."
                });

            var result = await _tableService.ExitTableAsync(request);
            if (!result)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "퇴장 처리에 실패했습니다."
                });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "퇴장 처리가 완료되었습니다."
            });
        }

        [HttpPost("force-exit")]
        public async Task<IActionResult> ForceExitTable([FromBody] TableForceExitRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "강제 종료 정보가 올바르지 않습니다."
                });

            await _tableService.ForceExitTableAsync(request);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "테이블이 강제 종료되었습니다."
            });
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTable([FromBody] TableConfigRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "테이블 정보가 올바르지 않습니다."
                });

            var tableId = await _tableService.AddTableAsync(request);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = new { tableId },
                Message = "테이블이 추가되었습니다."
            });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _tableService.DeleteTableAsync(id);
            if (!result)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "테이블을 찾을 수 없습니다."
                });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "테이블이 삭제되었습니다."
            });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTable(int id, [FromBody] TableConfigRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "테이블 정보가 올바르지 않습니다."
                });

            var result = await _tableService.UpdateTableAsync(id, request);
            if (!result)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "테이블을 찾을 수 없습니다."
                });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "테이블이 수정되었습니다."
            });
        }

        [HttpGet("config")]
        public async Task<IActionResult> GetTableConfig()
        {
            var config = await _tableService.GetTableConfigAsync();
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = new { tables = config, count = config.Count }
            });
        }
    }
}