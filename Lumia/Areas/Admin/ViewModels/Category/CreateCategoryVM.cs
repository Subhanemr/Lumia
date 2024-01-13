using System.ComponentModel.DataAnnotations;

namespace Lumia.Areas.Admin.ViewModels
{
    public class CreateCategoryVM
    {
        [Required(ErrorMessage = "Is valid")]
        public string Name { get; set; }
    }
}
