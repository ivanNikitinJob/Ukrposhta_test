using Ukrposhta.Entities;
using Ukrposhta.Models;
using Ukrposhta.Repositories.Interfaces;
using Ukrposhta.Servises.Interfaces;

namespace Ukrposhta.Servises
{
    public class ReportService : IReportService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public ReportService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<SalaryReportModel> GetDepartmentSalaryReport(int departmentId)
        {
            var result = new SalaryReportModel();
            try
            {
                var filter = new Dictionary<string, string>();
                filter.Add(nameof(Employee.DepartmentId), departmentId.ToString());
                var employeesList = await _employeeRepository.GetFilteredList(filter);

                if (employeesList.Count() < 1) 
                {
                        return result;
                }

                result.EmployeeCount = employeesList.Count();
                result.SalarySum = employeesList.Sum(x => x.Salary);
                result.SalaryAverage = employeesList.Average(x => x.Salary);
                result.PositionsCount = employeesList.Select(x => x.Position).Distinct().Count();
                result.DepartmentName = employeesList.FirstOrDefault().Department.Name;
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }
    }
}
