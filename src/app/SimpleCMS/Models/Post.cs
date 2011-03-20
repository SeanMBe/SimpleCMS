using System.ComponentModel.DataAnnotations;

namespace SimpleCMS.Models
{
    public class Post : DataModel
    {
        public virtual string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public virtual string Body { get; set; }
        public virtual int AuthorId { get; set; }
        public virtual User Author { get; set; }
    }
}