using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MISA.Infrastructure.Repositories;
using MISA_Core.Dtos.Response;
using MISA_Core.Entities;
using MISA_Core.Interface.Repositories;
using MISA_Core.MISAAttribute;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace MISA_Infrastructure.Repositories
{
    public class SalariesCompositionRepository : BaseRepository<SalariesComposition>, ISalariesCompositionRepo
    {
        public SalariesCompositionRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<PageDataResponse<SalariesComposition>?> GetAllWithFilter(string? search, int limit, int page, int statusIndex)
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
                FROM pa_salary_composition
                {whereSql}
                ORDER BY create_at DESC
                LIMIT @Limit OFFSET @Offset;";

            // Tổng số bản ghi
            var sqlCount = $@"
                SELECT COUNT(*)
                FROM pa_salary_composition
                {whereSql};";

            parameters.Add("@Limit", limit);
            parameters.Add("@Offset", (page - 1) * limit);

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Lấy dữ liệu
                var data = await connection.QueryAsync<SalariesComposition>(sqlData, parameters);

                // Lấy tổng số bản ghi
                var total = await connection.ExecuteScalarAsync<int>(sqlCount, parameters);

                return new PageDataResponse<SalariesComposition>
                {
                    CustomData = null,
                    PageData = data,
                    SummaryData = null,
                    Total = total
                };
            }
        }
        public async Task<bool> CheckComponentCodeExist(string code)
        {
            var sql = "SELECT COUNT(1) FROM pa_salary_composition WHERE component_code = @Code";

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var count = await connection.ExecuteScalarAsync<int>(sql, new { Code = code });
                return count > 0;
            }
        }
        public async Task<bool> ChangeSalaryStatus(List<Guid> ids, int status)
        {
            var sql = @"
                UPDATE pa_salary_composition 
                SET status = @Status
                WHERE salary_composition_id IN @Ids;
            ";

            var parameters = new DynamicParameters();
            parameters.Add("@Status", status);
            parameters.Add("@Ids", ids);

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(sql, parameters);
                return result > 0;
            }
        }
        public async Task<bool> DeleteByIds(List<Guid> ids)
        {
            var sql = @"
                DELETE FROM pa_salary_composition 
                WHERE salary_composition_id IN @Ids;
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

        public async Task<bool> InsertMany(List<SalariesComposition> salariesCompositions)
        {
            var tableName = GetTableName();
            var columns = GetColumnMappings()
                .Select(c => c.ColumnName)
                .ToList();

            var parameters = GetColumnMappings()
                .Select(c => "@" + c.Property.Name)
                .ToList();

            var sql = $@"
                    INSERT INTO {tableName} ({string.Join(", ", columns)})
                    VALUES ({string.Join(", ", parameters)});
                ";

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        var result = await connection.ExecuteAsync(sql, salariesCompositions, transaction);

                        await transaction.CommitAsync();
                        return result > 0;
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }
    }
}
