using _123.Models;

namespace _123
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }

        public Product Product { get; set; }
        public IEnumerable<Category> Categories { get; set; }  = new List<Category>();
        public IEnumerable<Material> Materials { get; set; }  = new List<Material>();
        
    }
}
