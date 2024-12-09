
using _123.Models;

namespace _123 {
  public class ProductDiscountViewModel
  {
    public List<ProductDiscount> ProductDiscounts {get; set;}

    public ProductDiscount ProductDiscount {get; set;}
    public string ProductId { get; set; } // Thêm thuộc tính ProductId
    public int DiscountId { get; set; } 
    public IEnumerable<Product> Products { get; set; }  = new List<Product>();
    public IEnumerable<Discount> Discounts { get; set; }  = new List<Discount>();
  }
}
