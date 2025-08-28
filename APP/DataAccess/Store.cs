using CORE.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.DataAccess
{
    public class Store : Entity
    {
        [Required, StringLength(200)]
        public string Name { get; set; }

        public bool IsVirtual { get; set; }

        public List<ProductStore> ProductStores { get; set; } = new List<ProductStore>();

        [NotMapped]
        public List<int> ProductIds
        {
            get => ProductStores.Select(productStore => productStore.ProductId).ToList();
            set => ProductStores = value.Select(productId => new ProductStore { ProductId = productId }).ToList();
        }
    }
}
