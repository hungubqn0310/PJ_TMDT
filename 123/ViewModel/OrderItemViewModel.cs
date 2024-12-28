using _123.Models;

namespace _123 {
  public class OrderItemViewModel
  {
    public List<OrderItem> Order_Items {get; set;}

    public  OrderItem Order_Item {get; set;}

        public IEnumerable<OrderItem> OrderItems { get; set; }   = new List<OrderItem>();

        public IEnumerable<Product> Products { get; set; }  = new List<Product>();

  }
}
