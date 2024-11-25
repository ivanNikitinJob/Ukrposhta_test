using Ukrposhta.Entities;
using Ukrposhta.Models;
using Ukrposhta.Repositories.Interfaces;
using Ukrposhta.Servises.Interfaces;

namespace Ukrposhta.Servises
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository emploeeRepository)
        {
            _employeeRepository = emploeeRepository;
        }

        public async Task<EmployeeModel> GetByIdAsync(int id)
        {
            return MapEntityToModel(await _employeeRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<EmployeeModel>> GetAllAsync()
        {
            List<EmployeeModel> result = new List<EmployeeModel>();
            try
            {
                var employeesList = await _employeeRepository.GetAllAsync();
                foreach (var employee in employeesList)
                {
                    result.Add(MapEntityToModel(employee));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public async Task<IEnumerable<EmployeeModel>> GetAllFilteredAsync(EmployeeFilterModel filter)
        {
            List<EmployeeModel> result = new List<EmployeeModel>();
            var filterDictionary = new Dictionary<string, string>();
            try
            {
                var propertiesList = typeof(EmployeeFilterModel).GetProperties();
                foreach (var property in propertiesList)
                {
                    var value = property.GetValue(filter);
                    if (value != null)
                    {
                        filterDictionary.Add(property.Name, value.ToString());
                    }
                }

                var employeesList = await _employeeRepository.GetFilteredList(filterDictionary);
                foreach (var employee in employeesList)
                {
                    result.Add(MapEntityToModel(employee));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public async Task Create(EmployeeModel model)
        {
            try
            {
                await _employeeRepository.CreateAsync(MapModelToEntity(model));

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Update(EmployeeModel model)
        {
            try
            {
                await _employeeRepository.UpdateAsync(MapModelToEntity(model));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task Delete(int id)
        {
            try
            {
                await _employeeRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private Employee MapModelToEntity(EmployeeModel model)
        {
            return new Employee
            {
                Id = model.Id,
                FullName = model.FullName,
                Address = model.Address,
                Phone = model.Phone,
                DateOfBirth = model.DateOfBirth,
                DateOfEmploiment = model.DateOfEmploiment,
                Position = model.Position,
                DepartmentId = model.DepartmentId,
                Description = model.Description,
                Salary = model.Salary.Value
            };
        }

        private EmployeeModel MapEntityToModel(Employee employee)
        {
            return new EmployeeModel
            {
                Id = employee.Id,
                Address = employee.Address,
                DateOfBirth = employee.DateOfBirth,
                DateOfEmploiment = employee.DateOfEmploiment,
                Description = employee.Description,
                FullName = employee.FullName,
                Phone = employee.Phone,
                Salary = employee.Salary,
                Position = employee.Position,
                DepartmentId = employee.DepartmentId,
                Department = new DepartmentModel
                {
                    Name = employee.Department.Name,
                }
            };
        }

    }
}
