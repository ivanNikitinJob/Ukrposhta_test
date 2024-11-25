using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using Ukrposhta.Entities;
using Ukrposhta.Models;
using Ukrposhta.Servises.Interfaces;

namespace Ukrposhta.Client.Pages
{
    public class ReportsPageModel : PageModel
    {
        private readonly IDepartmenService _departmenService;
        private readonly IReportService _reportService;
        private readonly IEmployeeService _emploeeService;

        [BindProperty]
        public int? DepartmentId { get; set; }
        public List<EmployeeModel> EmployeesList { get; set; }
        [BindProperty]
        public List<SelectListItem> DepartmentList { get; set; }
        [BindProperty]
        public SalaryReportModel SalaryReport { get; set; }
        [BindProperty]
        public EmployeeFilterModel? EmployeeFilter { get; set; }

        public ReportsPageModel(IDepartmenService departmenService, IReportService reportService, IEmployeeService employeeService)
        {
            _departmenService = departmenService;
            _reportService = reportService;
            _emploeeService = employeeService;

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
                throw;
            }
        }

        public async Task OnPostDepartmentSalary()
        {
            try
            {
                EmployeeFilter.DepartmentId = DepartmentId;
                await RefreshData();
                SalaryReport = await _reportService.GetDepartmentSalaryReport(DepartmentId.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<IActionResult> OnPostSaveFile(int id)
        {
            try
            {
                DepartmentId = id;
                EmployeeFilter.DepartmentId = DepartmentId;
                await RefreshData();

                SalaryReport = await _reportService.GetDepartmentSalaryReport(DepartmentId.Value);

                string filename = $"{SalaryReport.DepartmentName} Department Salary Report.txt";
                string content = $"{JsonConvert.SerializeObject(SalaryReport)} \r\n {JsonConvert.SerializeObject(EmployeesList)}";

                Byte[] buffer = Encoding.ASCII.GetBytes(content);

                return File(buffer, "application/text", filename);
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
