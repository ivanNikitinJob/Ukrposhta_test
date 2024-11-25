using Dapper;
using Microsoft.Extensions.Configuration;
using System.Text;
using Ukrposhta.Entities;
using Ukrposhta.Repositories.Interfaces;

namespace Ukrposhta.Repositories
{
    public class EmploeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmploeeRepository(IConfiguration configuration) : base(configuration)
        { }


        public override async Task<IEnumerable<Employee>> GetAllAsync()
        {
            IEnumerable<Employee> result;
            try
            {
                var departmentType = typeof(Department);
                var departmentTable = GetTableName(departmentType);
                var innerPrefix = "E";
                var outterPrefix = "D";
                string query = $"SELECT {GetColumnsAsProperties(innerPrefix)}, {GetColumnsAsProperties(departmentType, outterPrefix, true)} FROM {GetTableName()} {innerPrefix} INNER JOIN {departmentTable} {outterPrefix} ON {outterPrefix}.Id = {innerPrefix}.DepartmentId";
                using (var connection = CreateConnection())
                {
                    result = await connection.QueryAsync<Employee, Department, Employee>(query,
                        (employee, department) =>
                        {
                            employee.Department = department;
                            employee.DepartmentId = department.Id;

                            return employee;
                        }, splitOn: "DepartmentId");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public async Task<IEnumerable<Employee>> GetFilteredList(Dictionary<string, string> filterDictionary)
        {

            IEnumerable<Employee> result;
            try
            {
                var departmentType = typeof(Department);
                var departmentTable = GetTableName(departmentType);
                var innerPrefix = "E";
                var outterPrefix = "D";
                StringBuilder filter = new StringBuilder();

                for (int i = 0; i < filterDictionary.Count; i++)
                {
                    if (i == 0)
                    {
                        filter.Append(" WHERE ");
                    }

                    if (i != 0)
                    {
                        filter.Append(" AND ");
                    }

                    var filterItem = filterDictionary.ElementAt(i);
                    filter.Append($"{filterItem.Key} = '{filterItem.Value}'");
                }

                string query = $"SELECT {GetColumnsAsProperties(innerPrefix)}, {GetColumnsAsProperties(departmentType, outterPrefix, true)} FROM {GetTableName()} {innerPrefix} INNER JOIN {departmentTable} {outterPrefix} ON {outterPrefix}.Id = {innerPrefix}.DepartmentId {filter}";
                using (var connection = CreateConnection())
                {
                    result = await connection.QueryAsync<Employee, Department, Employee>(query,
                        (employee, department) =>
                        {
                            employee.Department = department;
                            employee.DepartmentId = department.Id;

                            return employee;
                        }, splitOn: "DepartmentId");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public override async Task<Employee> GetByIdAsync(int id)
        {
            Employee? employee;
            try
            {
                var departmentType = typeof(Department);
                var departmentTable = GetTableName(departmentType);
                var innerPrefix = "E";
                var outterPrefix = "D";
                string query = $"SELECT TOP(1) {GetColumnsAsProperties(innerPrefix)}, {GetColumnsAsProperties(departmentType, outterPrefix, true)} FROM {GetTableName()} {innerPrefix} INNER JOIN {departmentTable} {outterPrefix} ON {outterPrefix}.Id = {innerPrefix}.DepartmentId  WHERE {innerPrefix}.Id = '{id}'";

                using (var connection = CreateConnection())
                {
                    employee = (await connection.QueryAsync<Employee, Department, Employee>(query,
                        (employee, department) =>
                        {
                            employee.Department = department;
                            employee.DepartmentId = department.Id;
                            return employee;
                        }, splitOn: "DepartmentId")).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return employee;
        }
    }
}
