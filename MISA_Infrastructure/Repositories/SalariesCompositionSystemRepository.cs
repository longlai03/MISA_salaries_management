using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MISA.Infrastructure.Repositories;
using MISA_Core.Dtos.Response;
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

        public async Task<PageDataResponse<SalariesCompositionSystem>?> GetAllWithFilter(string? search, int limit, int page, int statusIndex)
        {
            var whereClauses = new List<string>();
            var parameters = new DynamicParameters();

            // Tìm kiếm
            if (!string.IsNullOrWhiteSpace(search))
            {
                whereClauses.Add("(component_code LIKE @Search OR component_name LIKE @Search)");
                parameters.Add("@Search", $"%{search}%");
            }

            // Lọc theo trạng thái
            if (statusIndex != -1)
            {
                whereClauses.Add("status = @StatusIndex");
                parameters.Add("@StatusIndex", statusIndex);
            }

            // Không có điều kiện nào
            var whereSql = whereClauses.Count > 0
                ? "WHERE " + string.Join(" AND ", whereClauses)
                : string.Empty;

            // Truy vấn dữ liệu
            var sqlData = $@"
                SELECT *
                FROM pa_salary_composition_system
                {whereSql}
                ORDER BY create_at DESC
                LIMIT @Limit OFFSET @Offset;";


            // Tổng số bản ghi
            var sqlCount = $@"
                SELECT COUNT(*)
                FROM pa_salary_composition_system
                {whereSql};";

            parameters.Add("@Limit", limit);
            parameters.Add("@Offset", (page - 1) * limit);

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                //await connection.OpenAsync();

                // Lấy dữ liệu
                var data = await connection.QueryAsync<SalariesCompositionSystem>(sqlData, parameters);

                // Lấy tổng số bản ghi
                var total = await connection.ExecuteScalarAsync<int>(sqlCount, parameters);

                return new PageDataResponse<SalariesCompositionSystem>
                {
                    CustomData = null,
                    PageData = data,
                    SummaryData = null,
                    Total = total
                };
            }
        }

        public async Task<bool> DeleteByIds(List<Guid> ids)
        {
            var sql = @"
                DELETE FROM pa_salary_composition_system 
                WHERE salary_composition_system_id IN @Ids;
            ";

            var parameters = new DynamicParameters();
            parameters.Add("@Ids", ids);

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(sql, parameters);
                return result > 0;
            }
        }

        public async Task<PageDataResponse<SalariesCompositionSystem>?> GetByIds(List<Guid> ids)
        {
            var sql = @"
                SELECT *
                FROM pa_salary_composition_system
                WHERE salary_composition_system_id IN @Ids;
            ";
            var parameters = new DynamicParameters();
            parameters.Add("@Ids", ids);
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var data = await connection.QueryAsync<SalariesCompositionSystem>(sql, parameters);
                return new PageDataResponse<SalariesCompositionSystem>
                {
                    CustomData = null,
                    PageData = data,
                    SummaryData = null,
                    Total = data.Count()
                };
            }
        }
    }
}
