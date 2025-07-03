using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WaitingController : ControllerBase
    {
        private readonly IWaitingService _waitingService;

        public WaitingController(IWaitingService waitingService)
        {
            _waitingService = waitingService;
        }

        [HttpGet("waittime")]
        public IActionResult GetWaitTime([FromQuery] int people)
        {
            var time = _waitingService.GetWaitTime(people);
            return Ok(new ApiResponse<int> { Success = true, Data = time });
        }

        [HttpGet("list")]
        public IActionResult GetWaitingList()
        {
            var list = _waitingService.GetWaitingList();
            return Ok(new ApiResponse<List<WaitingEntry>> { Success = true, Data = list });
        }
    }

}
