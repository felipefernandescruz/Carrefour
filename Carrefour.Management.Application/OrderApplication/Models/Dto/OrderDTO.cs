using System.ComponentModel.DataAnnotations;

namespace Carrefour.Management.Application.OrderApplication.Models.Dto
{
    public class OrderDTO
    {
        public string Description { get; set; }
        [Required]
        public double TotalOrder { get; set; }
    }
}
