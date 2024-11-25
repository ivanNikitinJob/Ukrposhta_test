using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ukrposhta.Entities.Interfaces;

namespace Ukrposhta.Entities
{
    public class BaseEntity: IEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
    }
}
