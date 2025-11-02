using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Interface.Repositories.Base
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T>? GetAll();
        T? GetById(Guid id);
        T Insert(T entity);
        T Update(Guid id, T entity);
        Guid Delete(Guid id);
    }
}
