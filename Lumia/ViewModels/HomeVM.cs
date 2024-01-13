using Lumia.Models;

namespace Lumia.ViewModels
{
    public class HomeVM
    {
        public ICollection<Product> Products { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
