using System.ComponentModel.DataAnnotations;

namespace IdentitySample.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Compare("Password")]
        //[Required]
        //public string ConfimPassword { get; set; }
    }
}