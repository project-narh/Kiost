using Microsoft.AspNetCore.Mvc;
using Server.Services.IService;
using Server.Models;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/kiosk")]
    public class KioskController : ControllerBase
    {
        private readonly IKioskService _kioskService;

        public KioskController(IKioskService kioskService)
        {
            _kioskService = kioskService;
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var status = await _kioskService.GetStatusAsync();
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = new { status }
            });
        }

        [HttpPost("status")]
        public async Task<IActionResult> SetStatus([FromBody] KioskStatusRequest request)
        {
            await _kioskService.SetStatusAsync(request.Status);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "상태가 변경되었습니다."
            });
        }
    }
}
