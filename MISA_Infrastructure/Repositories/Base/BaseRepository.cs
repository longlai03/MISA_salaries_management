using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Exceptions;
using MISA.Core.MISAAttribute;
using MISA_Core.Interface.Repositories.Base;
using MISA_Core.MISAAttribute;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly string _connectionString;

        /// <summary>
        /// Khởi tạo repository và lấy chuỗi kết nối từ cấu hình
        /// </summary>
        /// <param name="configuration">Đối tượng cấu hình chứa chuỗi kết nối cơ sở dữ liệu</param>
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBConnection");
        }
        /// <summary>
        /// Lấy toàn bộ danh sách bản ghi trong bảng
        /// </summary>
        /// <returns>Danh sách các bản ghi</returns>
        public virtual async Task<IEnumerable<T>?> GetAll()
        {
            var tableName = GetTableName();
            var sql = $"SELECT * FROM {tableName}";

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<T>(sql);
                return result.ToList();
            }
        }
        /// <summary>
        /// Lấy bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần lấy</param>
        /// <returns>Bản ghi tương ứng hoặc null nếu không tồn tại</returns>
        public virtual async Task<T?> GetById(Guid id)
        {
            var tableName = GetTableName();
            var keyColumn = GetKeyColumn();
            var sql = $"SELECT * FROM {tableName} WHERE {keyColumn} = @Id";

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
            }
        }
        /// <summary>
        /// Thêm mới một bản ghi vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entity">Thực thể cần thêm mới</param>
        /// <returns>Thực thể sau khi thêm</returns>
        public virtual async Task<T> Insert(T entity)
        {
            var tableName = GetTableName();
            var props = GetColumnMappings();

            var columnNames = string.Join(", ", props.Select(p => p.ColumnName));
            var paramNames = string.Join(", ", props.Select(p => "@" + p.Property.Name));
            var sql = $"INSERT INTO {tableName} ({columnNames}) VALUES ({paramNames})";

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, entity);
                return entity;
            }
        }
        /// <summary>
        /// Cập nhật dữ liệu bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần cập nhật</param>
        /// <param name="entity">Dữ liệu mới của bản ghi</param>
        /// <returns>Thực thể sau khi cập nhật</returns>
        /// <exception cref="ValidateException">Ném ra khi không tìm thấy bản ghi cần cập nhật</exception>
        public virtual async Task<T> Update(Guid id, T entity)
        {
            var tableName = GetTableName();
            var keyColumn = GetKeyColumn();

            var props = GetColumnMappings().Where(p => p.ColumnName != keyColumn);
            var setClause = string.Join(", ", props.Select(p => $"{p.ColumnName} = @{p.Property.Name}"));
            var sql = $"UPDATE {tableName} SET {setClause} WHERE {keyColumn} = @Id";

            var parameters = new DynamicParameters(entity);
            parameters.Add("Id", id);

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var affected = await connection.ExecuteAsync(sql, parameters);

                if (affected == 0)
                    throw new ValidateException($"Không tìm thấy bản ghi có ID = {id}");

                return entity;
            }
        }
        /// <summary>
        /// Xóa bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần xóa</param>
        /// <returns>ID của bản ghi đã bị xóa</returns>
        /// <exception cref="ValidateException">Ném ra khi không tìm thấy bản ghi cần xóa</exception>
        public virtual async Task<Guid> DeleteById(Guid id)
        {
            var tableName = GetTableName();
            var keyColumn = GetKeyColumn();
            var sql = $"DELETE FROM {tableName} WHERE {keyColumn} = @Id";

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var affected = await connection.ExecuteAsync(sql, new { Id = id });

                if (affected == 0)
                    throw new ValidateException($"Không tìm thấy bản ghi có ID = {id}");

                return id;
            }
        }
        /// <summary>
        /// Lấy tên bảng từ attribute [MISATableName] gắn trên entity
        /// </summary>
        /// <returns>Tên bảng trong cơ sở dữ liệu</returns>
        protected string GetTableName()
        {
            var tableAttr = typeof(T).GetCustomAttribute<MISATableNameAttribute>();
            return tableAttr?.TableName ?? typeof(T).Name.ToLower();
        }

        /// <summary>
        /// Lấy tên cột khóa chính của bảng, ưu tiên property chứa "id"
        /// </summary>
        /// <returns>Tên cột khóa chính</returns>
        protected string GetKeyColumn()
        {
            var keyProp = typeof(T).GetProperties()
                .FirstOrDefault(p => p.Name.ToLower().Contains("id"));
            var attr = keyProp?.GetCustomAttribute<MISAColumnNameAttribute>();
            return attr?.ColumnName ?? keyProp?.Name ?? "id";
        }

        /// <summary>
        /// Lấy danh sách ánh xạ giữa property và tên cột tương ứng trong cơ sở dữ liệu
        /// </summary>
        /// <returns>Tập hợp các cặp (PropertyInfo, ColumnName)</returns>
        protected IEnumerable<(PropertyInfo Property, string ColumnName)> GetColumnMappings()
        {
            return typeof(T).GetProperties()
                .Select(p => (p, p.GetCustomAttribute<MISAColumnNameAttribute>()?.ColumnName ?? p.Name));
        }

    }
}
