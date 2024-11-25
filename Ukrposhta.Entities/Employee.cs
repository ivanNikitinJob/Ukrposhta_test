using System.ComponentModel.DataAnnotations.Schema;

namespace Ukrposhta.Entities
{
    [Table("Employees")]
    public class Employee: BaseEntity
    {
        [Column("FullName")]
        public string FullName { get; set; }
        [Column("Address")]
        public string Address { get; set; }
        [Column("Phone")]
        public string Phone { get; set; }
        [Column("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        [Column("DateOfEmploiment")]
        public DateTime DateOfEmploiment { get; set; }
        [Column("Salary")]
        public decimal Salary { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("Position")]
        public string Position { get; set; }
        [Column("DepartmentId")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

    }
}
