using System.ComponentModel.DataAnnotations;

namespace SimpleCMS.Models
{
    public class Post : DataModel
    {
        [Required(ErrorMessage = "Title is required")]
        public virtual string Title { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Body is required")]
        public virtual string Body { get; set; }
        public virtual User Author { get; set; }
    }
}