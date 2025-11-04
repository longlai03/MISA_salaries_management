using MISA_Core.Entities;
using MISA_Core.Interface.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Interface.Repositories
{
    public interface ISalariesCompositionRepo : IBaseRepository<SalariesComposition>
    {
        /// <summary>
        /// Lấy danh sách thành phần lương có phân trang và điều kiện tìm kiếm
        /// </summary>
        /// <param name="search">Từ khóa tìm kiếm (có thể null)</param>
        /// <param name="limit">Số lượng bản ghi tối đa trong một trang</param>
        /// <param name="page">Số trang hiện tại</param>
        /// <returns>Danh sách thành phần lương thỏa mãn điều kiện</returns>
        Task<IEnumerable<SalariesComposition>?> GetAllWithFilter(string? search, int limit, int page);
        /// <summary>
        /// Kiểm tra mã thành phần lương đã tồn tại trong hệ thống hay chưa
        /// </summary>
        /// <param name="code">Mã thành phần lương cần kiểm tra</param>
        /// <returns>True nếu mã đã tồn tại, ngược lại False</returns>
        Task<bool> CheckComponentCodeExist(string code);
        Task<bool> ChangeSalaryStatus(List<Guid> ids, bool status);
        Task<bool> DeleteByIds(List<Guid> ids);

    }
}
