using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ukrposhta.Entities;
using Ukrposhta.Models;
using Ukrposhta.Servises;
using Ukrposhta.Servises.Interfaces;

namespace Ukrposhta.Client.Pages.Employees
{
    public class EmployeesPageModel : PageModel
    {
        private readonly IEmployeeService _emploeeService;
        private readonly IDepartmenService _departmenService;

        public List<EmployeeModel> EmployeesList { get; set; }
        [BindProperty]
        public List<SelectListItem> DepartmentList { get; set; }
        [BindProperty]
        public EmployeeFilterModel? EmployeeFilter { get; set; }

        public EmployeesPageModel(IEmployeeService employeeService, IDepartmenService departmenService)
        {
            _emploeeService = employeeService;
            _departmenService = departmenService;

            EmployeesList = new List<EmployeeModel>();
            DepartmentList = new List<SelectListItem>();
        }

        public async Task OnGet()
        {
            try
            {
               await RefreshData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task OnPost()
        {
            try
            {
                await RefreshData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task OnPostDelete(int id)
        {
            try
            {
                await _emploeeService.Delete(id);
                await RefreshData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private async Task RefreshData()
        {
            if (EmployeeFilter == null)
            {
                EmployeesList = (await _emploeeService.GetAllAsync()).ToList();
            }

            if (EmployeeFilter != null)
            {
                EmployeesList = (await _emploeeService.GetAllFilteredAsync(EmployeeFilter)).ToList();
            }

            var departments = await _departmenService.GetAllAsync();

            DepartmentList.Add(new SelectListItem
            {
                Disabled = true,
                Selected = true
            });

            foreach (var department in departments)
            {
                DepartmentList.Add(new SelectListItem
                {
                    Value = department.Id.ToString(),
                    Text = department.Name
                });
            }
        }
    }
}
