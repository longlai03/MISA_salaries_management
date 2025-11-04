using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MISA.Infrastructure.Repositories;
using MISA_Core.Entities;
using MISA_Core.Interface.Repositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MISA_Infrastructure.Repositories
{
    public class SalariesCompositionRepository : BaseRepository<SalariesComposition>, ISalariesCompositionRepo
    {
        public SalariesCompositionRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<SalariesComposition>?> GetAllWithFilter(string? search, int limit, int page)
        {
            var sql = @"
                SELECT * 
                FROM pa_salary_composition 
                WHERE (@Search IS NULL OR component_code LIKE @Search OR component_name LIKE @Search)
                LIMIT @Limit OFFSET @Offset;";

            var parameters = new DynamicParameters();
            parameters.Add("@Search", string.IsNullOrWhiteSpace(search) ? null : $"%{search}%");
            parameters.Add("@Limit", limit);
            parameters.Add("@Offset", (page - 1) * limit);

            var connection = new MySqlConnection(_connectionString);
            var result = await connection.QueryAsync<SalariesComposition>(sql, parameters);
            connection.Close();


            return result;
        }

        public async Task<bool> CheckComponentCodeExist(string code)
        {
            var sql = "SELECT COUNT(1) FROM pa_salary_composition WHERE component_code = @Code";

            var parameters = new DynamicParameters();
            parameters.Add("@Code", code);

            await using var connection = new MySqlConnection(_connectionString);
            var count = await connection.ExecuteScalarAsync<int>(sql, new { Code = code });

            return count > 0;
        }

        public async Task<bool> ChangeSalaryStatus(List<Guid> ids, bool status)
        {

            var sql = @"
                UPDATE pa_salary_composition 
                SET status = @Status
                WHERE salary_composition_id IN @Ids;
            ";

            var parameters = new DynamicParameters();
            parameters.Add("@Status", status);
            parameters.Add("@Ids", ids);

            await using var connection = new MySqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(sql, parameters);

            return result > 0;
        }

        public async Task<bool> DeleteByIds(List<Guid> ids)
        {
            var sql = @"
                DELETE FROM pa_salary_composition 
                WHERE salary_composition_id IN @Ids";
            var parameters = new DynamicParameters();
            parameters.Add("@Ids", ids);
            await using var connection = new MySqlConnection(_connectionString);
            var result = await connection.ExecuteAsync(sql, parameters);

            return result > 0;
        }
    }
}
