using System.ComponentModel.DataAnnotations;

namespace Lumia.Areas.Admin.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Is valid")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Is valid")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Is valid")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Is valid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Is valid")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Is valid")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage ="Is compare with Password")]
        public string ConfirmPassword { get; set; }
    }
}
