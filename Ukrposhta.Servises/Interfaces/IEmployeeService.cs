using Ukrposhta.Entities;
using Ukrposhta.Models;

namespace Ukrposhta.Servises.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeModel> GetByIdAsync(int id);
        Task<IEnumerable<EmployeeModel>> GetAllAsync();
        Task<IEnumerable<EmployeeModel>> GetAllFilteredAsync(EmployeeFilterModel filter);
        Task Create(EmployeeModel model);
        Task Update(EmployeeModel model);

        Task Delete(int id);
    }
}
