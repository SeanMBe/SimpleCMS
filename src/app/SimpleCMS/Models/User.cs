using System.ComponentModel.DataAnnotations;

namespace SimpleCMS.Models
{
    public class User : DataModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public virtual string UserName { get; set; }
    }
}