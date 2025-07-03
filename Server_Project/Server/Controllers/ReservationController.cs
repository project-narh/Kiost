using Microsoft.AspNetCore.Mvc;
using Server.Services.IService;

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
        public IActionResult Create([FromBody] ReservationRequest request)
        {
            var result = _reservationService.CreateReservation(request);
            return Ok(new { success = true, data = result });
        }

        [HttpGet("list")]
        public IActionResult List()
        {
            var list = _reservationService.GetReservations();
            return Ok(new { success = true, data = list });
        }

        [HttpDelete("{id}")]
        public IActionResult Cancel(int id)
        {
            _reservationService.CancelReservation(id);
            return Ok(new { success = true, message = "예약 취소됨" });
        }
    }

    public class ReservationRequest
    {
        public string Name { get; set; } = "";
        public string Phone { get; set; } = "";
        public int People { get; set; }
        public DateTime ReservationTime { get; set; }
    }

}
