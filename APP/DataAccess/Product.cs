using CORE.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public Category Category { get; set; } // navigational property

        public List<ProductStore> ProductStores { get; set; } = new List<ProductStore>(); // navigation property

        [NotMapped]
        public List<int> StoreIds 
        { 
            get => ProductStores.Select(productStore => productStore.StoreId).ToList();
            set => ProductStores = value.Select(storeId => new ProductStore { StoreId = storeId }).ToList(); 
        }
    }
}
