using CORE.Models;
using System.ComponentModel;

namespace APP.Business.Models
{
    public class StoreResponse : Response
    {
        public string Name { get; set; }
        public bool IsVirtual { get; set; }

        [DisplayName("Status")]
        public string IsVirtualF { get; set; }

        [DisplayName("Product Count")]
        public int ProductCount { get; set; }

        public string Products { get; set; }
    }
}
