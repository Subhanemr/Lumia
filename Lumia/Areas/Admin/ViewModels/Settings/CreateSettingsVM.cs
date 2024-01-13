using System.ComponentModel.DataAnnotations;

namespace Lumia.Areas.Admin.ViewModels
{
    public class CreateSettingsVM
    {
        [Required(ErrorMessage = "Is valid")]
        public string Key { get; set; }

        [Required(ErrorMessage = "Is valid")]
        public string Value { get; set; }
    }
}
