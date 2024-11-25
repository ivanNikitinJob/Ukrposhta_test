using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ukrposhta.Entities
{
    [Table("Departments")]
    public class Department: BaseEntity
    {
        [MaxLength(50)]
        [Column("Name")]
        public string Name { get; set; }
    }
}
