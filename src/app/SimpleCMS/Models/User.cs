using System.ComponentModel.DataAnnotations;

namespace SimpleCMS.Models
{
    public class User : DataModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public virtual string UserName { get; set; }

        public virtual string Email { get; set; }
        public virtual string EncryptedPassword { get; set; }
        public virtual string PasswordSalt { get; set; }
        public virtual string ResetPasswordToken { get; set; }
        public virtual string RememberToken { get; set; }
        public virtual string RememeberCreateDate { get; set; }
        public virtual string SignInCount { get; set; } //default to 0
        public virtual string CurrentLoginDate { get; set; }
        public virtual string LastLoginDate { get; set; }
        public virtual string CurrentLoginIP { get; set; }
        public virtual string LastLoginIP { get; set; }
    }
}