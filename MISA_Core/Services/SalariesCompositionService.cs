using FluentValidation;
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
    public class SalariesCompositionService: ISalariesCompositionService
    {
        private readonly ISalariesCompositionRepo _salariesCompositionRepo;
        private readonly SalariesCompositionValidator _validator;

        public SalariesCompositionService(ISalariesCompositionRepo salariesCompostionRepo)
        {
            _salariesCompositionRepo = salariesCompostionRepo;
            _validator = new SalariesCompositionValidator();
        }

        public async Task<bool> ChangeSalaryStatus(List<Guid> ids, int status)
        {
            return await _salariesCompositionRepo.ChangeSalaryStatus(ids, status);
        }

        public async Task<Guid> DeleteById(Guid id)
        {
            return await _salariesCompositionRepo.DeleteById(id);
        }

        public async Task<bool> DeleteByIds(List<Guid> ids)
        {
            return await _salariesCompositionRepo.DeleteByIds(ids);
        }

        public async Task<IEnumerable<SalariesComposition>?> GetAll()
        {
            return await _salariesCompositionRepo.GetAll();
        }

        public async Task<PageDataResponse<SalariesComposition>?> GetAllWithFilter(string? search, int limit, int page, int statusIndex)
        {
            return await _salariesCompositionRepo.GetAllWithFilter(search, limit, page, statusIndex);
        }

        public async Task<SalariesComposition?> GetById(Guid id)
        {
            return await _salariesCompositionRepo.GetById(id);
        }

        public async Task<SalariesComposition> Insert(SalariesComposition salary)
        {
            ValidateSalaryComposition(salary);
            if (salary.ComponentCode != null)
            {
                var checkCodeExist = await _salariesCompositionRepo.CheckComponentCodeExist(salary.ComponentCode);
                if (checkCodeExist)
                {
                    var errors = new Dictionary<string, string>{
                    { nameof(salary.ComponentCode), "Mã thành phần lương đã tồn tại trong hệ thống." }
                };
                    throw new ValidateException(errors);
                }
            }
            salary.SalaryCompositionId = Guid.NewGuid();
            salary.Status = 1;
            salary.CreateSource = 1;
            salary.CreateAt = DateTime.Now;
            salary.UpdateAt = DateTime.Now;

            return await _salariesCompositionRepo.Insert(salary);
        }

        public async Task<SalariesComposition> Update(Guid id, SalariesComposition salary)
        {
            ValidateSalaryComposition(salary);
            salary.UpdateAt = DateTime.Now;
            return await _salariesCompositionRepo.Update(id, salary);
        }

        private void ValidateSalaryComposition(SalariesComposition salary)
        {
            var errors = _validator.Validate(salary);
            if (errors.Count > 0)
            {
                throw new ValidateException(errors);
            }
        }
    }
}
