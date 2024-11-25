using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ukrposhta.Entities;
using Ukrposhta.Models;
using Ukrposhta.Servises;
using Ukrposhta.Servises.Interfaces;

namespace Ukrposhta.Client.Pages.Employees
{
    public class EditEmployeeModel : PageModel
    {
        private readonly IEmployeeService _emploeeService;
        private readonly IDepartmenService _departmenService;

        [BindProperty]
        public EmployeeModel Employee { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }

        public EditEmployeeModel(IEmployeeService employeeService, IDepartmenService departmenService)
        {
            _emploeeService = employeeService;
            _departmenService = departmenService;

            Employee = new EmployeeModel();
            DepartmentList = new List<SelectListItem>();
        }

        public async Task OnGet(int? id)
        {
            if (id == null)
            {
                return;
            }

            var employeeEntity = await _emploeeService.GetByIdAsync(id.Value);
            Employee = new EmployeeModel
            {
                Id = employeeEntity.Id,
                Address = employeeEntity.Address,
                DateOfBirth = employeeEntity.DateOfBirth,
                DateOfEmploiment = employeeEntity.DateOfEmploiment,
                Description = employeeEntity.Description,
                FullName = employeeEntity.FullName,
                Phone = employeeEntity.Phone,
                Position = employeeEntity.Position,
                Salary = employeeEntity.Salary,
                DepartmentId = employeeEntity.DepartmentId,
                Department = new DepartmentModel
                {
                    Name = employeeEntity.Department.Name,
                    Id = employeeEntity.DepartmentId
                }
            };

            var departments = await _departmenService.GetAllAsync();

            foreach (var department in departments)
            {
                DepartmentList.Add(new SelectListItem
                {
                    Value = department.Id.ToString(),
                    Text = department.Name
                });
            }
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                await _emploeeService.Update(Employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return RedirectToPage("/Employees/Employees");
        }
    }
}
