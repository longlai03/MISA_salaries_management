using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA_Core.Interface.Services;
using MISA_Core.Helpers;
using MISA_Core.Entities;
using MISA_Core.Dtos.Request;

namespace MISA_salaries_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesCompositionController : ControllerBase
    {
        private readonly ISalariesCompositionService _salariesCompositionService;
        public SalariesCompositionController(ISalariesCompositionService salariesCompositionService)
        {
            _salariesCompositionService = salariesCompositionService;
        }

        /// <summary>
        /// Lấy danh sách thành phần lương có phân trang và tìm kiếm
        /// </summary>
        /// <param name="search">Từ khóa tìm kiếm (có thể rỗng)</param>
        /// <param name="limit">Số lượng bản ghi mỗi trang</param>
        /// <param name="page">Số trang hiện tại</param>
        /// <returns>Danh sách thành phần lương thỏa mãn điều kiện</returns>
        // GET: api/salaries-composition
        [HttpGet]
        public async Task<object?> GetSalaryCompositionByFilter([FromQuery] string search = "", [FromQuery] int limit = 10, [FromQuery] int page = 1)
        {

            var getSalary = await _salariesCompositionService.GetAllWithFilter(search, limit, page);
            return getSalary;
        }

        /// <summary>
        /// Lấy thông tin chi tiết của thành phần lương theo ID
        /// </summary>
        /// <param name="id">ID của thành phần lương cần lấy</param>
        /// <returns>Thông tin chi tiết thành phần lương</returns>
        // GET: api/salaries-composition/{id}
        [HttpGet("{id}")]
        public async Task<object?> GetSalaryCompositionById([FromRoute] Guid id)
        {
            var getSalaryById = await _salariesCompositionService.GetById(id);
            return getSalaryById;
        }

        /// <summary>
        /// Thêm mới một bản ghi thành phần lương
        /// </summary>
        /// <param name="salary">Dữ liệu thành phần lương cần thêm</param>
        /// <returns>Kết quả thêm bản ghi thành công hoặc thất bại</returns>
        // POST: api/salaries-composition
        [HttpPost]
        public async Task<object> InsertSalaryComposition([FromBody] SalariesComposition salary)
        {
            var insertedSalary = await _salariesCompositionService.Insert(salary);
            return insertedSalary;
        }

        /// <summary>
        /// Cập nhật thông tin thành phần lương theo ID
        /// </summary>
        /// <param name="salary">Dữ liệu thành phần lương cần cập nhật</param>
        /// <param name="id">ID của bản ghi cần cập nhật</param>
        /// <returns>Kết quả cập nhật bản ghi thành công hoặc thất bại</returns>
        //PUT: api/salaries-composition/{id}
        [HttpPut("{id}")]
        public async Task<object> UpdateSalaryComposition([FromBody] SalariesComposition salary, [FromRoute] Guid id)
        {
            var updatedSalary = await _salariesCompositionService.Update(id, salary);
            return updatedSalary;
        }

        /// <summary>
        /// Xóa thành phần lương theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần xóa</param>
        /// <returns>Kết quả xóa bản ghi thành công</returns>
        // DELETE: api/salaries-composition/{id}
        [HttpDelete("{id}")]
        public async Task<object> DeleteSalaryCompositionById([FromRoute] Guid id)
        {
            var deletedSalary = await _salariesCompositionService.DeleteById(id);
            return deletedSalary;
        }

        /// <summary>
        /// Xóa thành phần lương theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần xóa</param>
        /// <returns>Kết quả xóa bản ghi thành công</returns>
        // DELETE: api/salaries-composition/{id}
        [HttpDelete("delete-bulk")]
        public async Task<object> DeleteSalaryCompositionByIds([FromBody] SalariesCompositionDeleteRequest request)
        {
            var deletedSalary = await _salariesCompositionService.DeleteByIds(request.SalaryCompositionIds);
            return deletedSalary;
        }

        /// <summary>
        /// Kiểm tra mã thành phần lương đã tồn tại trong hệ thống hay chưa
        /// </summary>
        /// <param name="code">Mã thành phần lương cần kiểm tra</param>
        /// <returns>True nếu mã đã tồn tại, ngược lại False</returns>
        // GET: api/salaries-composition/check-code
        [HttpGet("check-code")]
        public async Task<object> CheckComponentCodeExist([FromQuery] string code)
        {
            var isExist = await _salariesCompositionService.CheckComponentCodeExist(code);
            return isExist;
        }

        /// <summary>
        /// Cập nhật trạng thái hoạt động của thành phần lương
        /// </summary>
        /// <param name="code">Mã thành phần lương cần kiểm tra</param>
        /// <returns>True nếu mã đã tồn tại, ngược lại False</returns>
        // GET: api/salaries-composition/change-status
        [HttpPut("change-status")]
        public async Task<object> ChangeStatus([FromBody] SalariesCompositionStatusRequest request)
        {
            var changeStatus = await _salariesCompositionService.ChangeSalaryStatus(request.SalaryCompositionIds, request.Status);
            return changeStatus;
        }
    }
}
