using CORE.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Business.Models
{
    public class ProductRequest : Request
    {
        [Required(ErrorMessage = "{0} is required!")] // Product Name is required!
        [StringLength(200, MinimumLength = 3, ErrorMessage = "{0} must be maximum {1} minimum {2} characters!")]
        // Product Name must be maximum 200 minimum 3 characters!
        [DisplayName("Product Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0.01, 1000000, ErrorMessage = "{0} must be between {1} and {2}!")]
        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "{0} must be positive!")]
        [DisplayName("Stock Amount")]
        public int? StockAmount { get; set; }

        [DisplayName("Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [DisplayName("Continued")]
        public bool IsContinued { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }
    }
}
