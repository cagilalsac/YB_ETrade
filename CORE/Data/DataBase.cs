namespace CORE.Data
{
    public abstract class DataBase 
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
