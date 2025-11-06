using MISA_Core.Dtos.Response;
using MISA_Core.Entities;
using MISA_Core.Interface.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Interface.Repositories
{
    public interface ISalariesCompositionSystemRepo : IBaseRepository<SalariesCompositionSystem>
    {
        /// <summary>
        /// Lấy danh sách thành phần lương hệ thống có phân trang và điều kiện tìm kiếm
        /// </summary>
        /// <param name="search">Từ khóa tìm kiếm (có thể null)</param>
        /// <param name="limit">Số lượng bản ghi tối đa trong một trang</param>
        /// <param name="page">Số trang hiện tại</param>
        /// <returns>Danh sách thành phần lương hệ thống thỏa mãn điều kiện</returns>
        Task<PageDataResponse<SalariesCompositionSystem>?> GetAllWithFilter(string? search, int limit, int page, int statusIndex);
        Task<bool> DeleteByIds(List<Guid> ids);

        Task<PageDataResponse<SalariesCompositionSystem>?> GetByIds(List<Guid> ids);
    }
}
