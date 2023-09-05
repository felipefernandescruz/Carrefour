using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carrefour.Management.Repository.Entities
{
    public class Order : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
        public double TotalOrder { get; set; }
        public int OrderTypeId { get; set; }

        public virtual OrderType OrderType { get; set; }
    }
}
