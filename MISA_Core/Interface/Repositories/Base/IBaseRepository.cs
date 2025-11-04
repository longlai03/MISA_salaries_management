using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Interface.Repositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Lấy toàn bộ danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách các bản ghi</returns>
        Task<IEnumerable<T>?> GetAll();
        /// <summary>
        /// Lấy bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần lấy</param>
        /// <returns>Bản ghi tương ứng hoặc null nếu không tìm thấy</returns>
        Task<T?> GetById(Guid id);
        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entity">Thực thể cần thêm</param>
        /// <returns>Bản ghi sau khi thêm</returns>
        Task<T> Insert(T entity);
        /// <summary>
        /// Cập nhật dữ liệu cho bản ghi
        /// </summary>
        /// <param name="id">ID của bản ghi cần cập nhật</param>
        /// <param name="entity">Dữ liệu mới của bản ghi</param>
        /// <returns>Bản ghi sau khi cập nhật</returns>
        Task<T> Update(Guid id, T entity);
        /// <summary>
        /// Xóa bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần xóa</param>
        /// <returns>ID của bản ghi đã bị xóa</returns>
        Task<Guid> DeleteById(Guid id);

    }
}
