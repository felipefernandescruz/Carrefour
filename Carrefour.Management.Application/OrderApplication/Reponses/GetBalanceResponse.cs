using System.ComponentModel.DataAnnotations;

namespace Carrefour.Management.Application.OrderApplication.Reponses
{
    public class GetBalanceResponse
    {
        public double StartBalance { get; set; }
        public double EndBalance { get; set; }
        public double TotalCredit { get; set; }
        public double TotalDebit { get; set; }
    }
}
