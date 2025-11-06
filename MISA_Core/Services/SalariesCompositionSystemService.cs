using MISA.Core.Exceptions;
using MISA_Core.Dtos.Response;
using MISA_Core.Entities;
using MISA_Core.Interface.Repositories;
using MISA_Core.Interface.Services;
using MISA_Core.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Services
{
    public class SalariesCompositionSystemService : ISalariesCompositionSystemService
    {
        private readonly ISalariesCompositionSystemRepo _salariesCompositionSystemRepo;
        private readonly ISalariesCompositionRepo _salariesCompositionRepo;
        private readonly SalariesCompositionSystemValidator _validator;


        /// <summary>
        /// Khởi tạo service với repository quản lý dữ liệu thành phần lương hệ thống
        /// </summary>
        /// <param name="salariesCompositionSystemRepo">Repository của danh mục thành phần lương hệ thống</param>

        public SalariesCompositionSystemService(ISalariesCompositionSystemRepo salariesCompositionSystemRepo, ISalariesCompositionRepo salariesCompositionRepo)
        {
            _salariesCompositionSystemRepo = salariesCompositionSystemRepo;
            _salariesCompositionRepo = salariesCompositionRepo;
            _validator = new SalariesCompositionSystemValidator();
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
            return _salariesCompositionSystemRepo.DeleteByIds(ids);
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
        public async Task<PageDataResponse<SalariesCompositionSystem>?> GetAllWithFilter(string? search, int limit, int page, int statusIndex)
        {
            return await _salariesCompositionSystemRepo.GetAllWithFilter(search, limit, page, statusIndex);
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
        public async Task<SalariesCompositionSystem> Insert(SalariesCompositionSystem entity)
        {
            return await _salariesCompositionSystemRepo.Insert(entity);
        }

        public async Task<bool> TransferDataSalarySystemToSalary(List<Guid> ids)
        {
            var systemDataResponse = await _salariesCompositionSystemRepo.GetByIds(ids);
            if (systemDataResponse == null || systemDataResponse.PageData == null )
            {
                throw new ValidateException("Không tìm thấy dữ liệu nào trong bảng hệ thống để chuyển.");
            }

            var systemData = systemDataResponse.PageData.ToList();

            var targetData = systemData.Select(x => new SalariesComposition
            {
                SalaryCompositionId = x.SalaryCompositionSystemId,
                ComponentCode = x.ComponentCode,
                ComponentName = x.ComponentName,
                OrganizationId = x.OrganizationId,
                OrganizationName = x.OrganizationName,
                ComponentType = x.ComponentType,
                Nature = x.Nature,
                TaxMode = x.TaxMode,
                IsTaxDeductible = x.IsTaxDeductible,
                Quota = x.Quota,
                AllowOverQuota = x.AllowOverQuota,
                ValueType = x.ValueType,
                Value = x.Value,
                ValueCalcScope = x.ValueCalcScope,
                ValueCalcSumEmployeeFormula = x.ValueCalcSumEmployeeFormula,
                ValueCalcManualFormula = x.ValueCalcManualFormula,
                ValueTaxablePortion = x.ValueTaxablePortion,
                ValueTaxFreePortion = x.ValueTaxFreePortion,
                Description = x.Description,
                ShowOnSalary = x.ShowOnSalary,
                CreateSource = x.CreateSource,
                Status = x.Status,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            }).ToList();

            var insertSuccess = await _salariesCompositionRepo.InsertMany(targetData);

            if (!insertSuccess)
            {
                throw new Exception("Chuyển dữ liệu thất bại.");
            }

            var deleteSuccess = await _salariesCompositionSystemRepo.DeleteByIds(ids);
            if (!deleteSuccess)
            {
                throw new Exception("Không thể xóa dữ liệu trong bảng hệ thống sau khi chuyển.");
            }

            return true;
        }

        /// <summary>
        /// Cập nhật thông tin thành phần lương hệ thống theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần cập nhật</param>
        /// <param name="entity">Dữ liệu mới của bản ghi</param>
        /// <returns>Bản ghi sau khi cập nhật</returns>
        public async Task<SalariesCompositionSystem> Update(Guid id, SalariesCompositionSystem salarySystem)
        {
            ValidateSalaryComposition(salarySystem);
            salarySystem.UpdateAt = DateTime.Now;
            return await _salariesCompositionSystemRepo.Update(id, salarySystem);
        }

        private void ValidateSalaryComposition(SalariesCompositionSystem salarySystem)
        {
            var errors = _validator.Validate(salarySystem);
            if (errors.Count > 0)
            {
                throw new ValidateException(errors);
            }
        }
    }
}
