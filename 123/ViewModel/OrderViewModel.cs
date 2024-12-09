using _123.Models;

namespace _123 {
  public class OrderViewModel
  {
    public List<Order> Orders {get; set;}

    public Order Order {get; set;}

        public IEnumerable<User> Users { get; set; }  = new List<User>();


  }
}
