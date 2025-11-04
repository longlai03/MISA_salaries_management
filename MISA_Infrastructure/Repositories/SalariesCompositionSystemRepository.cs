using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MISA.Infrastructure.Repositories;
using MISA_Core.Entities;
using MISA_Core.Interface.Repositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_Infrastructure.Repositories
{
    public class SalariesCompositionSystemRepository : BaseRepository<SalariesCompositionSystem>, ISalariesCompositionSystemRepo
    {
        public SalariesCompositionSystemRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<SalariesCompositionSystem>?> GetAllWithFilter(string? search, int limit, int page)
        {
            const string sql = @"
                SELECT * 
                FROM pa_salary_composition_system 
                WHERE (@Search IS NULL OR component_code LIKE @Search OR component_name LIKE @Search)
                LIMIT @Limit OFFSET @Offset;";

            var parameters = new DynamicParameters();
            parameters.Add("@Search", string.IsNullOrWhiteSpace(search) ? null : $"%{search}%");
            parameters.Add("@Limit", limit);
            parameters.Add("@Offset", (page - 1) * limit);

            await using var connection = new MySqlConnection(_connectionString);
            var result = await connection.QueryAsync<SalariesCompositionSystem>(sql, parameters);
            return result;
        }
    }
}
