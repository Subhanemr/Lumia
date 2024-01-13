using System.ComponentModel.DataAnnotations;

namespace Lumia.Areas.Admin.ViewModels
{
    public class UpdateCategoryVM
    {
        [Required(ErrorMessage = "Is valid")]
        public string Name { get; set; }
    }
}
