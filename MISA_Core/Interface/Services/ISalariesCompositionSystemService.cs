using MISA_Core.Dtos.Response;
using MISA_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Interface.Services
{
    public interface ISalariesCompositionSystemService
    {
        Task<IEnumerable<SalariesCompositionSystem>?> GetAll();
        Task<SalariesCompositionSystem?> GetById(Guid id);
        Task<Guid> DeleteById(Guid id);
        Task<SalariesCompositionSystem> Insert(SalariesCompositionSystem salarySystem);
        Task<SalariesCompositionSystem> Update(Guid id, SalariesCompositionSystem salarySystem);

        /// <summary>
        /// Lấy danh sách thành phần lương hệ thống có điều kiện tìm kiếm và phân trang
        /// </summary>
        /// <param name="search">Từ khóa tìm kiếm (có thể null)</param>
        /// <param name="limit">Số lượng bản ghi mỗi trang</param>
        /// <param name="page">Số trang hiện tại</param>
        /// <returns>Danh sách thành phần lương hệ thống phù hợp điều kiện</returns>

        Task<PageDataResponse<SalariesCompositionSystem>?> GetAllWithFilter(string? search, int limit, int page, int statusIndex);
        Task<bool> TransferDataSalarySystemToSalary(List<Guid> ids);
        Task<bool> DeleteByIds(List<Guid> ids);
    }
}
