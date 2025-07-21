using CORE.Entities;
using System.ComponentModel.DataAnnotations;

namespace APP.DataAccess
{
    public class Product : Entity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public int? StockAmount { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public bool IsContinued { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
