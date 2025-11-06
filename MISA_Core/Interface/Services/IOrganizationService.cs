using MISA_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Interface.Services
{
    public interface IOrganizationService
    {
        Task<IEnumerable<Organization>?> GetAll();
        Task<Organization?> GetById(Guid id);
        Task<Organization> Insert(Organization entity);
        Task<Organization> Update(Guid id, Organization entity);
        Task<Guid> DeleteById(Guid id);
    }
}
