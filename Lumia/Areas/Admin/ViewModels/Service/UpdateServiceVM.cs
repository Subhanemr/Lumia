using System.ComponentModel.DataAnnotations;

namespace Lumia.Areas.Admin.ViewModels
{
    public class UpdateServiceVM
    {
        [Required(ErrorMessage = "Is valid")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Is valid")]
        public string SubTitle { get; set; }

        [Required(ErrorMessage = "Is valid")]
        public string Icon { get; set; }
    }
}
