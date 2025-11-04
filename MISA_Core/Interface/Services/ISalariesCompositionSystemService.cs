using MISA_Core.Entities;
using MISA_Core.Interface.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Interface.Services
{
    public interface ISalariesCompositionSystemService: IBaseService<SalariesCompositionSystem>
    {
        /// <summary>
        /// Lấy danh sách thành phần lương hệ thống có điều kiện tìm kiếm và phân trang
        /// </summary>
        /// <param name="search">Từ khóa tìm kiếm (có thể null)</param>
        /// <param name="limit">Số lượng bản ghi mỗi trang</param>
        /// <param name="page">Số trang hiện tại</param>
        /// <returns>Danh sách thành phần lương hệ thống phù hợp điều kiện</returns>

        Task<IEnumerable<SalariesCompositionSystem>?> GetAllWithFilter(string? search, int limit, int page);

    }
}
