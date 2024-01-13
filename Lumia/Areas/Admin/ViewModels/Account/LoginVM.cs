using System.ComponentModel.DataAnnotations;

namespace Lumia.Areas.Admin.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Is valid")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Is valid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Is valid")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
