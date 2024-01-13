using Lumia.Models;
using System.ComponentModel.DataAnnotations;

namespace Lumia.Areas.Admin.ViewModels
{
    public class UpdateProductVM
    {
        [Required(ErrorMessage = "Is valid")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Is valid")]
        public decimal Price { get; set; }
        public string? Description { get; set; }

        public string? Img { get; set; }
        public IFormFile? Photo { get; set; }


        [Required(ErrorMessage = "Is valid")]
        public int CategoryId { get; set; }
        public ICollection<Category>? Categories { get; set; }
    }
}
