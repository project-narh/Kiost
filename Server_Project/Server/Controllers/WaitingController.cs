using Microsoft.AspNetCore.Mvc;
using Server.Services.IService;
using Server.Models;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/waiting")]
    public class WaitingController : ControllerBase
    {
        private readonly IWaitingService _waitingService;

        public WaitingController(IWaitingService waitingService)
        {
            _waitingService = waitingService;
        }

        [HttpGet("waittime")]
        public async Task<IActionResult> GetWaitTime([FromQuery] int people)
        {
            var time = await _waitingService.GetWaitTimeAsync(people);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = new { waitTime = time, people }
            });
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetWaitingList()
        {
            var list = await _waitingService.GetWaitingListAsync();
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = new { waitingList = list, count = list.Count }
            });
        }
    }
}