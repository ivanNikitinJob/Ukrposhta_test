using Ukrposhta.Entities;

namespace Ukrposhta.Repositories.Interfaces
{
    public interface IEmployeeRepository: IBaseRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetFilteredList(Dictionary<string, string> filterDictionary);
    }
}
