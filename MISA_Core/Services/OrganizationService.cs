using MISA_Core.Entities;
using MISA_Core.Interface.Services;
using MISA_Core.Interface.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Core.Services
{
    public class OrganizationService : IBaseService<Organization>, IOrganizationService
    {
        public Task<Guid> DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Organization>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Organization?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Organization> Insert(Organization entity)
        {
            throw new NotImplementedException();
        }

        public Task<Organization> Update(Guid id, Organization entity)
        {
            throw new NotImplementedException();
        }
    }
}
