using MISA_Core.Entities;
using MISA_Core.Interface.Repositories;
using MISA_Core.Interface.Services;
using MISA_Core.Interface.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Services
{
    public class SalariesCompositionSystemService : IBaseService<SalariesCompositionSystem>, ISalariesCompositionSystemService
    {
        private readonly ISalariesCompositionSystemRepo _salariesCompositionSystemRepo;

        /// <summary>
        /// Khởi tạo service với repository quản lý dữ liệu thành phần lương hệ thống
        /// </summary>
        /// <param name="salariesCompositionSystemRepo">Repository của danh mục thành phần lương hệ thống</param>

        public SalariesCompositionSystemService(ISalariesCompositionSystemRepo salariesCompositionSystemRepo)
        {
            _salariesCompositionSystemRepo = salariesCompositionSystemRepo;
        }
        /// <summary>
        /// Xóa bản ghi thành phần lương hệ thống theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần xóa</param>
        /// <returns>ID của bản ghi đã bị xóa</returns>
        public Task<Guid> DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIds(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lấy danh sách tất cả các thành phần lương hệ thống
        /// </summary>
        /// <returns>Danh sách thành phần lương hệ thống</returns>
        public Task<IEnumerable<SalariesCompositionSystem>?> GetAll()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Lấy danh sách thành phần lương hệ thống có điều kiện tìm kiếm và phân trang
        /// </summary>
        /// <param name="search">Từ khóa tìm kiếm (có thể null)</param>
        /// <param name="limit">Số lượng bản ghi tối đa mỗi trang</param>
        /// <param name="page">Số trang hiện tại</param>
        /// <returns>Danh sách thành phần lương hệ thống phù hợp điều kiện</returns>
        public async Task<IEnumerable<SalariesCompositionSystem>?> GetAllWithFilter(string? search, int limit, int page)
        {
            return await _salariesCompositionSystemRepo.GetAllWithFilter(search, limit, page);
        }
        /// <summary>
        /// Lấy thông tin chi tiết của thành phần lương hệ thống theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần lấy</param>
        /// <returns>Bản ghi tương ứng hoặc null nếu không tồn tại</returns>
        public Task<SalariesCompositionSystem?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Thêm mới một bản ghi thành phần lương hệ thống
        /// </summary>
        /// <param name="entity">Dữ liệu thành phần lương cần thêm</param>
        /// <returns>Bản ghi sau khi thêm</returns>
        public Task<SalariesCompositionSystem> Insert(SalariesCompositionSystem entity)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Cập nhật thông tin thành phần lương hệ thống theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần cập nhật</param>
        /// <param name="entity">Dữ liệu mới của bản ghi</param>
        /// <returns>Bản ghi sau khi cập nhật</returns>
        public Task<SalariesCompositionSystem> Update(Guid id, SalariesCompositionSystem entity)
        {
            throw new NotImplementedException();
        }
    }
}
