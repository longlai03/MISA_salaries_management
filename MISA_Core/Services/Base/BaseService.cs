using FluentValidation;
using MISA_Core.Interface.Repositories.Base;
using MISA_Core.Interface.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Services.Base
{
    public class BaseService<T>: IBaseService<T>
    {
        protected readonly IBaseRepository<T> _repository;
        protected readonly IValidator<T>? _validator;

        public BaseService(IBaseRepository<T> repository, IValidator<T>? validator = null)
        {
            _repository = repository;
            _validator = validator;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual T GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public virtual T Insert(T entity)
        {
            ValidateEntity(entity);
            return _repository.Insert(entity);
        }

        public virtual T Update(Guid id, T entity)
        {
            ValidateEntity(entity);
            return _repository.Update(id, entity);
        }

        public virtual Guid DeleteById(Guid id)
        {
            return _repository.Delete(id);
        }

        protected void ValidateEntity(T entity)
        {
            if (_validator == null) return;

            var result = _validator.Validate(entity);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => string.Join("; ", g.Select(e => e.ErrorMessage))
                    );

                //throw new ValidateException(errors);
            }
        }
    }
}
