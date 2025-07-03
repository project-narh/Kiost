using Microsoft.AspNetCore.Mvc;
using Server.Services.IService;

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
        public IActionResult GetStatus()
        {
            var status = _kioskService.GetStatus();
            return Ok(new { success = true, status });
        }

        [HttpPost("status")]
        public IActionResult SetStatus([FromBody] KioskStatusRequest request)
        {
            _kioskService.SetStatus(request.Status);
            return Ok(new { success = true, message = "상태 변경됨" });
        }
    }

    public class KioskStatusRequest
    {
        public string Status { get; set; } = "on";
    }
}
