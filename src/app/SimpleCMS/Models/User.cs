namespace SimpleCMS.Models
{
    public class User : DataModel
    {
        public virtual string DisplayName { get; set; }
        public virtual string UserName { get; set; }
    }
}