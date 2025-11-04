using FluentValidation;
using MISA.Core.Exceptions;
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
    public class SalariesCompositionService: IBaseService<SalariesComposition>, ISalariesCompositionService
    {
        private readonly ISalariesCompositionRepo _salariesCompositionRepo;
        //private readonly IValidator<SalariesComposition>? _validator;

        public SalariesCompositionService(ISalariesCompositionRepo salariesCompostionRepo, IValidator<SalariesComposition>? validator)
        {
            _salariesCompositionRepo = salariesCompostionRepo;
            //_validator = validator;
        }

        public async Task<bool> ChangeSalaryStatus(List<Guid> ids, bool status)
        {
            return await _salariesCompositionRepo.ChangeSalaryStatus(ids, status);
        }

        public async Task<bool> CheckComponentCodeExist(string code)
        {
            return await _salariesCompositionRepo.CheckComponentCodeExist(code);
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

        public async Task<IEnumerable<SalariesComposition>?> GetAllWithFilter(string? search, int limit, int page)
        {
            return await _salariesCompositionRepo.GetAllWithFilter(search, limit, page);
        }

        public async Task<SalariesComposition?> GetById(Guid id)
        {
            return await _salariesCompositionRepo.GetById(id);
        }

        public async Task<SalariesComposition> Insert(SalariesComposition salary)
        {
            //var errors = new Dictionary<string, string>();
            //var result = _validator.Validate(salary);

            //if (!result.IsValid)
            //{
            //    errors = result.Errors
            //        .GroupBy(e => e.PropertyName)
            //        .ToDictionary(
            //            g => g.Key,
            //            g => string.Join("; ", g.Select(e => e.ErrorMessage))
            //        );

            //    throw new ValidateException(errors);
            //}

            //if (!string.IsNullOrEmpty(salary.Email))
            //{
            //    var checkEmailExisting = _salariesCompositionRepo.checkExistedEmail(candidate.Email);
            //    if (checkEmailExisting)
            //    {
            //        errors[nameof(Candidate.Email)] = "Email đã tồn tại";
            //        throw new ValidateException(errors);

            //    }
            //}

            salary.SalaryCompositionId = Guid.NewGuid();
            salary.Status = true;
            salary.CreateSource = 1;
            salary.CreateAt = DateTime.Now;
            salary.UpdateAt = DateTime.Now;

            return await _salariesCompositionRepo.Insert(salary);
        }

        public async Task<SalariesComposition> Update(Guid id, SalariesComposition salary)
        {
            salary.UpdateAt = DateTime.Now;
            return await _salariesCompositionRepo.Update(id, salary);
        }
    }
}
