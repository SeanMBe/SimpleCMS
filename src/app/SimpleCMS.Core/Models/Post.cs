using System.ComponentModel.DataAnnotations;

namespace SimpleCMS.Core.Models
{
    public class Post : DataModel
    {
        [Required(ErrorMessage = "Title is required")]
        public virtual string Title { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Body is required")]
        public virtual string Body { get; set; }
        public virtual Account Author { get; set; }
    }
}