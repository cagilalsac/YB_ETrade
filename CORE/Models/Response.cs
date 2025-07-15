using CORE.Entities;

namespace CORE.Models
{
    public abstract class Response : Data
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
