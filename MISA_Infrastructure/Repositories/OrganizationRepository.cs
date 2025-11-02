using Microsoft.EntityFrameworkCore;
using MISA.Infrastructure.Database;
using MISA_Core.Entities;
using MISA_Core.Interface.Repositories;
using MISA_Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Infrastructure.Repositories
{
    public class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepo
    {
        private readonly string _connectionString;
        public OrganizationRepository(AppDbContext context) : base(context)
        {
            _connectionString = context.Database.GetDbConnection().ConnectionString;
        }
    }
}
