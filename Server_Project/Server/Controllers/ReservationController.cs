using Microsoft.AspNetCore.Mvc;
using Server.Services.IService;
using Server.Models;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/reservation")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReservationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "입력 정보가 올바르지 않습니다."
                });

            var result = await _reservationService.CreateReservationAsync(request);
            return Ok(new ApiResponse<ReservationInfo>
            {
                Success = true,
                Data = result,
                Message = "예약이 등록되었습니다."
            });
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var list = await _reservationService.GetReservationsAsync();
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = new { reservations = list, count = list.Count }
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var result = await _reservationService.CancelReservationAsync(id);
            if (!result)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "예약을 찾을 수 없습니다."
                });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "예약이 취소되었습니다."
            });
        }
    }
}