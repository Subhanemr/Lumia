namespace Lumia.Models
{
    public class Category : BaseNameEntity
    {
        public ICollection<Product>? Products { get; set; }
    }
}
