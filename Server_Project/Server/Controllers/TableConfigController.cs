using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableConfigController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableConfigController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public IActionResult GetConfig()
        {
            var config = _tableService.GetTableConfig();
            return Ok(new ApiResponse<List<Table>> { Success = true, Data = config });
        }

        [HttpPost]
        public IActionResult AddTable([FromBody] TableConfigRequest request)
        {
            _tableService.AddTable(request);
            return Ok(new ApiResponse<object> { Success = true, Message = "테이블이 추가되었습니다." });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTable(int id, [FromBody] TableConfigRequest request)
        {
            _tableService.UpdateTable(id, request);
            return Ok(new ApiResponse<object> { Success = true, Message = "테이블이 수정되었습니다." });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTable(int id)
        {
            _tableService.DeleteTable(id);
            return Ok(new ApiResponse<object> { Success = true, Message = "테이블이 삭제되었습니다." });
        }
    }

}
