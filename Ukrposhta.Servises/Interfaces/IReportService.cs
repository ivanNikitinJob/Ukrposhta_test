using Ukrposhta.Models;

namespace Ukrposhta.Servises.Interfaces
{
    public interface IReportService
    {
        Task<SalaryReportModel> GetDepartmentSalaryReport(int departmentId);
    }
}
