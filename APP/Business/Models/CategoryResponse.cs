using CORE.Models;
using System.ComponentModel;

namespace APP.Business.Models
{
    public class CategoryResponse : Response
    {
        [DisplayName("Category Name")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
