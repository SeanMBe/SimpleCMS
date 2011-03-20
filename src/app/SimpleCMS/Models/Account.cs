using System.ComponentModel.DataAnnotations;

namespace SimpleCMS.Models
{
    public class Account : DataModel
    {
        [Required(ErrorMessage = "Email is required")]
        public virtual string Email { get; set; }
        public virtual string EncryptedPassword { get; set; }
        public virtual string PasswordSalt { get; set; }
        public virtual string ResetPasswordToken { get; set; }
        public virtual string SignInCount { get; set; } //default to 0
        public virtual string CurrentLogin { get; set; }
        public virtual string CurrentLoginDate { get; set; }
        public virtual string LastLogin { get; set; }
        public virtual string LastLoginDate { get; set; }
    }
}