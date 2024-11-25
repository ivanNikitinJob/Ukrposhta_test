using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Ukrposhta.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ukrposhta.Entities.Interfaces;

namespace Ukrposhta.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : IEntity
    {

        private readonly IConfiguration _configuration;

        protected BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("UkrposhtaDatabase"));
        }

        internal string GetTableName()
        {
            var type = typeof(T);
            return GetTableName(type);
        }

        public string GetTableName(Type type)
        {
            var tableAttribute = type.GetCustomAttribute<TableAttribute>();
            if (tableAttribute != null)
            {
                return tableAttribute.Name;
            }

            return type.Name;
        }

        internal string GetColumnsAsProperties(string prefix = null, bool excludeKey = false)
        {
            var type = typeof(T);
            return GetColumnsAsProperties(type, prefix, excludeKey);
        }

        public string GetColumnsAsProperties(Type type, string prefix = null, bool excludeKey = false)
        {
            var prefixWithDot = !string.IsNullOrEmpty(prefix) ? $"{prefix}." : string.Empty;

            var typeProperties = type.GetProperties().ToList();

            var columnsAsProperties = string.Join(", ", typeProperties
                .OrderByDescending(p => p.IsDefined(typeof(KeyAttribute)))
                .Where(p => !excludeKey || !p.IsDefined(typeof(KeyAttribute)))
                .Where(p => !p.GetMethod.IsVirtual || p.IsDefined(typeof(KeyAttribute)))
                .Select(p =>
                {
                    var columnAttribute = p.GetCustomAttribute<ColumnAttribute>();
                    return columnAttribute != null ? $"{prefixWithDot}{columnAttribute.Name} AS {p.Name}" : $"{prefixWithDot}{p.Name}";
                }));

            return columnsAsProperties;
        }

        internal string GetPropertyNames(bool excludeKey = false)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null)
                .Where(p => !p.GetMethod.IsVirtual || p.IsDefined(typeof(KeyAttribute)));

            var values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            return values;
        }

        internal string GetColumns(bool excludeKey = false)
        {
            var type = typeof(T);
            var columns = string.Join(", ", type.GetProperties()
                .Where(p => !excludeKey || !p.IsDefined(typeof(KeyAttribute)))
                .Where(p => !p.GetMethod.IsVirtual || p.IsDefined(typeof(KeyAttribute)))
                .Select(p =>
                {
                    var columnAttribute = p.GetCustomAttribute<ColumnAttribute>();
                    return columnAttribute != null ? columnAttribute.Name : p.Name;
                }));

            return columns;
        }

        internal IEnumerable<PropertyInfo> GetProperties(bool excludeKey = false)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null)
                .Where(p => !p.GetMethod.IsVirtual || p.IsDefined(typeof(KeyAttribute)));

            return properties;
        }

        public async Task<int> CreateAsync(T entity)
        {
            try
            {
                string query = $"INSERT INTO {GetTableName()} ({GetColumns(true)}) VALUES ({GetPropertyNames(true)})";

                using (var connection = CreateConnection())
                {
                    return await connection.ExecuteAsync(query, entity);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            IEnumerable<T> result;
            try
            {
                string query = $"SELECT {GetColumnsAsProperties()} FROM {GetTableName()}";
                using (var connection = CreateConnection())
                {
                    result = await connection.QueryAsync<T>(query);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            T result;
            try
            {
                string query = $"SELECT {GetColumnsAsProperties()} FROM {GetTableName()} WHERE Id = '{id}'";
                using (var connection = CreateConnection())
                {
                    result = await connection.QueryFirstOrDefaultAsync<T>(query);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public async Task<int> UpdateAsync(T entity)
        {
            try
            {
                var query = new StringBuilder();
                query.Append($"UPDATE {GetTableName()} SET ");

                foreach (var property in GetProperties(true))
                {
                    var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();

                    string propertyName = property.Name;
                    string columnName = columnAttribute?.Name ?? "";

                    query.Append($"{columnName} = @{propertyName},");
                }

                query.Remove(query.Length - 1, 1);
                query.Append($" WHERE Id = @{nameof(entity.Id)}");

                using (var connection = CreateConnection())
                {
                    return await connection.ExecuteAsync(query.ToString(), entity);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                string query = $"DELETE FROM {GetTableName()} WHERE Id = {id}";

                using (var connection = CreateConnection())
                {
                    return await connection.ExecuteAsync(query);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
