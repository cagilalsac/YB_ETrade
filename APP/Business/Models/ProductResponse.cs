using CORE.Models;
using System.ComponentModel;

namespace APP.Business.Models
{
    public class ProductResponse : Response
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        [DisplayName("Unit Price")]
        public string UnitPriceF { get; set; }

        [DisplayName("Stock Amount")]
        public int? StockAmount { get; set; }

        public DateTime? ExpirationDate { get; set; }

        [DisplayName("Expiration Date")]
        public string ExpirationDateF { get; set; }

        public bool IsContinued { get; set; }

        [DisplayName("Status")] // Continued or Discontinued
        public string IsContinuedF { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }
    }
}
