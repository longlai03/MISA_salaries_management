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
    public class SalariesCompositionRepository: BaseRepository<SalariesComposition>, ISalariesCompostionRepo
    {
        private readonly string _connectionString;
        public SalariesCompositionRepository(AppDbContext context) : base(context)
        {
            _connectionString = context.Database.GetDbConnection().ConnectionString;
        }
    }
}
