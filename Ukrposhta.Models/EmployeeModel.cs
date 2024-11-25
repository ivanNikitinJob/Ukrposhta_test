namespace Ukrposhta.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Today;
        public DateTime DateOfEmploiment { get; set; } = DateTime.Today;
        public decimal? Salary { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public virtual DepartmentModel Department { get; set; }
        public string Position { get; set; }
    }
}
