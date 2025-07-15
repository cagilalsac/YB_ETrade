namespace CORE.Entities
{
    public abstract class Entity : Data
    {
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
