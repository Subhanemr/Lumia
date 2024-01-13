using Lumia.Models;
using System.ComponentModel.DataAnnotations;

namespace Lumia.Areas.Admin.ViewModels
{
    public class CreateProductVM
    {
        [Required(ErrorMessage = "Is valid")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Is valid")]
        public decimal Price { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "Is valid")]
        public IFormFile Photo { get; set; }


        [Required(ErrorMessage = "Is valid")]
        public int CategoryId { get; set; }

        public ICollection<Category>? Categories { get; set; }
    }
}
