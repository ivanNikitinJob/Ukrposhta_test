namespace Ukrposhta.Models
{
    public class SalaryReportModel
    {
        public string DepartmentName { get; set; }
        public decimal SalarySum { get; set; }
        public decimal SalaryAverage { get; set; }
        public int EmployeeCount { get; set; }
        public int PositionsCount { get; set; }
    }
}
