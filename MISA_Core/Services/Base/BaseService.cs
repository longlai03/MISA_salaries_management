using FluentValidation;
using MISA_Core.Interface.Repositories.Base;
using MISA_Core.Interface.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA_Core.Services.Base
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IBaseRepository<T> _repository;

        /// <summary>
        /// Khởi tạo service với repository và validator tương ứng
        /// </summary>
        /// <param name="repository">Đối tượng repository thao tác dữ liệu</param>
        /// <param name="validator">Đối tượng validator để kiểm tra dữ liệu (có thể null)</param>
        public BaseService(IBaseRepository<T> repository, IValidator<T>? validator = null)
        {
            _repository = repository;
        }
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public virtual async Task<IEnumerable<T>?> GetAll()
        {
            return await _repository.GetAll();
        }
        /// <summary>
        /// Lấy bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần lấy</param>
        /// <returns>Bản ghi tương ứng hoặc null nếu không tồn tại</returns>
        public virtual async Task<T?> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }
        /// <summary>
        /// Thêm mới một bản ghi sau khi kiểm tra dữ liệu hợp lệ
        /// </summary>
        /// <param name="entity">Thực thể cần thêm mới</param>
        /// <returns>Bản ghi sau khi thêm</returns>
        public virtual async Task<T> Insert(T entity)
        {
            return await _repository.Insert(entity);
        }
        /// <summary>
        /// Cập nhật bản ghi theo ID sau khi kiểm tra dữ liệu hợp lệ
        /// </summary>
        /// <param name="id">ID của bản ghi cần cập nhật</param>
        /// <param name="entity">Dữ liệu mới của bản ghi</param>
        /// <returns>Bản ghi sau khi cập nhật</returns>
        public virtual async Task<T> Update(Guid id, T entity)
        {
            return await _repository.Update(id, entity);
        }
        /// <summary>
        /// Xóa bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần xóa</param>
        /// <returns>ID của bản ghi đã bị xóa</returns>
        public virtual async Task<Guid> DeleteById(Guid id)
        {
            return await _repository.DeleteById(id);
        }
    }
}
