using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Interface.Services.Base
{
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        Task<IEnumerable<T>?> GetAll();

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        Task<T?> GetById(Guid id);

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        Task<T> Insert(T entity);

        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        Task<T> Update(Guid id, T entity);

        /// <summary>
        /// Xóa bản ghi theo Id
        /// </summary>
        Task<Guid> DeleteById(Guid id);
    }
}
