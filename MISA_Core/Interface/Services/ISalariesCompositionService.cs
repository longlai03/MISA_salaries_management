using MISA_Core.Dtos.Response;
using MISA_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Interface.Services
{
    public interface ISalariesCompositionService
    {
        Task<IEnumerable<SalariesComposition>?> GetAll();
        Task<SalariesComposition?> GetById(Guid id);
        Task<SalariesComposition> Insert(SalariesComposition salary);
        Task<SalariesComposition> Update(Guid id, SalariesComposition salary);
        Task<Guid> DeleteById(Guid id);
        /// <summary>
        /// Lấy danh sách thành phần lương có điều kiện lọc và phân trang
        /// </summary>
        /// <param name="search">Từ khóa tìm kiếm (có thể null)</param>
        /// <param name="limit">Số lượng bản ghi tối đa trong một trang</param>
        /// <param name="page">Số trang hiện tại</param>
        /// <returns>Danh sách thành phần lương phù hợp với điều kiện</returns>
        Task<PageDataResponse<SalariesComposition>?> GetAllWithFilter(string? search, int limit, int page, int statusIndex);
        /// <summary>
        /// Kiểm tra xem mã thành phần lương đã tồn tại trong hệ thống hay chưa
        /// </summary>
        /// <param name="code">Mã thành phần cần kiểm tra</param>
        /// <returns>True nếu mã đã tồn tại, ngược lại False</returns>
        Task<bool> ChangeSalaryStatus(List<Guid> ids, int status);
        Task<bool> DeleteByIds(List<Guid> ids);
    }
}
