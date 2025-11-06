using Microsoft.AspNetCore.Mvc;
using MISA_Core.Dtos.Request;
using MISA_Core.Entities;
using MISA_Core.Interface.Services;

namespace MISA_salaries_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesCompositionSystemController : ControllerBase
    {
        private readonly ISalariesCompositionSystemService _salariesCompositionSystemService;

        public SalariesCompositionSystemController(ISalariesCompositionSystemService salariesCompositionSystemService)
        {
            _salariesCompositionSystemService = salariesCompositionSystemService;
        }

        /// <summary>
        /// Lấy danh sách thành phần lương hệ thống có phân trang và tìm kiếm
        /// </summary>
        /// <param name="search">Từ khóa tìm kiếm (có thể rỗng)</param>
        /// <param name="limit">Số lượng bản ghi mỗi trang</param>
        /// <param name="page">Số trang hiện tại</param>
        /// <param name="statusIndex">Trạng thái lọc (mặc định -1 = tất cả)</param>
        /// <returns>Danh sách thành phần lương hệ thống thỏa mãn điều kiện</returns>
        // GET: api/salaries-composition-system
        [HttpGet]
        public async Task<object> GetSalaryCompositionSystemByFilter([FromQuery] string search = "",[FromQuery] int limit = 10,[FromQuery] int page = 1,[FromQuery] int statusIndex = -1)
        {
            var getSalarySystem = await _salariesCompositionSystemService.GetAllWithFilter(search, limit, page, statusIndex);
            return getSalarySystem;
        }

        /// <summary>
        /// Xóa nhiều bản ghi thành phần lương hệ thống theo danh sách ID
        /// </summary>
        /// <param name="request">Danh sách ID các bản ghi cần xóa</param>
        /// <returns>Kết quả xóa thành công hoặc thất bại</returns>
        // DELETE: api/salaries-composition-system
        [HttpDelete]
        public async Task<object> DeleteSalaryCompositionSystemByIds([FromBody] SalariesCompositionSystemIdsRequest request)
        {
            var deleteSalarySystem = await _salariesCompositionSystemService.DeleteByIds(request.SalaryCompositionSystemIds);
            return deleteSalarySystem;
        }

        /// <summary>
        /// Chuyển dữ liệu từ bảng thành phần lương hệ thống sang bảng thành phần lương chính
        /// </summary>
        /// <param name="request">Danh sách ID cần chuyển</param>
        /// <returns>Kết quả chuyển dữ liệu thành công hoặc thất bại</returns>
        // POST: api/salaries-composition-system/transfer-salary-system-to-salary
        [HttpPost("transfer-salary-system-to-salary")]
        public async Task<object> TransferDataSalarySystemToSalary([FromBody] SalariesCompositionSystemIdsRequest request)
        {
            var transferResult = await _salariesCompositionSystemService.TransferDataSalarySystemToSalary(request.SalaryCompositionSystemIds);
            return transferResult;
        }   

        /// <summary>
        /// Thêm mới một bản ghi thành phần lương hệ thống
        /// </summary>
        /// <param name="request">Dữ liệu thành phần lương hệ thống cần thêm</param>
        /// <returns>Kết quả thêm bản ghi thành công hoặc thất bại</returns>
        // POST: api/salaries-composition-system
        [HttpPost]
        public async Task<object> InsertSalaryCompositionSystem([FromBody] SalariesCompositionSystem request)
        {
            var insertSalarySystem = await _salariesCompositionSystemService.Insert(request);
            return insertSalarySystem;
        }
    }
}
