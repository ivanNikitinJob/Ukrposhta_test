using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ukrposhta.Models;
using Ukrposhta.Servises.Interfaces;

namespace Ukrposhta.Client.Pages.Employees
{
    public class CreateEmployeeModel : PageModel
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmenService _departmenService;

        [BindProperty]
        public EmployeeModel Employee { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }

        public CreateEmployeeModel(IEmployeeService emploeeService, IDepartmenService departmenService)
        {
            _employeeService = emploeeService;
            _departmenService = departmenService;

            Employee = new EmployeeModel();
            DepartmentList = new List<SelectListItem>();
        }

        public async Task OnGet()
        {
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
                await _employeeService.Create(Employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return RedirectToPage("/Employees/Employees");
        }
    }
}
