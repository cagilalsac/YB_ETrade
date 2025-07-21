using CORE.Entities;
using System.ComponentModel.DataAnnotations;

namespace APP.DataAccess
{
    public class Category : Entity
    {
        [Required]
        // Way 1:
        //[MaxLength(100)]
        //[MinLength(3)]
        // Way 2:
        //[Length(3, 100)]
        // Way 3:
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
