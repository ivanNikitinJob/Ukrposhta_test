using Ukrposhta.Entities;
using Ukrposhta.Repositories.Interfaces;
using Ukrposhta.Servises.Interfaces;

namespace Ukrposhta.Servises
{
    public class DepartmentService : IDepartmenService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            IEnumerable<Department> result;
            try
            {
                result = await _departmentRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }
}
