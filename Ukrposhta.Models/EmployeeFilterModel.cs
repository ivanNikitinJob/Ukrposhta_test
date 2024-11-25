namespace Ukrposhta.Models
{
    public class EmployeeFilterModel
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfEmploiment { get; set; }
        public decimal? Salary { get; set; }
        public string? Description { get; set; }
        public int? DepartmentId { get; set; }
        public string? Position { get; set; }
    }
}
