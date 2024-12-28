using _123.Models;

namespace _123 {
  public class ProductDetailViewModel
  {
    public List<ProductDetail> ProductDetails {get; set;}

    public ProductDetail ProductDetail {get; set;}
    public IEnumerable<Product> Products { get; set; }  = new List<Product>();
    
  }
}