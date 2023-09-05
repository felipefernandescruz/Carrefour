using Carrefour.Management.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrefour.Management.Application.OrderApplication.Models.Dto
{
    public class NewOrderDTO
    {
        public string Description { get; set; }
        [Required]
        public double TotalOrder { get; set; }
        [Required]
        public int OrderTypeId { get; set; }
    }
}
