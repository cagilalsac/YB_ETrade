using CORE.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Business.Models
{
    public class StoreRequest : Request
    {
        [Required(ErrorMessage = "{0} is required!")] 
        [StringLength(200, MinimumLength = 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        public string Name { get; set; }

        [DisplayName("Status")]
        public bool IsVirtual { get; set; }
    }
}
