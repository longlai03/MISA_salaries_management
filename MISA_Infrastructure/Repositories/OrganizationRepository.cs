using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MISA.Infrastructure.Repositories;
using MISA_Core.Entities;
using MISA_Core.Interface.Repositories;
using MISA_Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Infrastructure.Repositories
{
    public class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepo
    {
        public OrganizationRepository(IConfiguration config) : base(config)
        {

        }
    }
}
