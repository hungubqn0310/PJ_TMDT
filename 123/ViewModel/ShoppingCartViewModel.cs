using _123.Models;

//shopping
namespace _123 {
  public class ShoppingCartViewModel
  {
    public List<ShoppingCart> ShoppingCarts {get; set;}

    public ShoppingCart ShoppingCart {get; set;}

    public IEnumerable<User> Users { get; set; }  = new List<User>();

    public IEnumerable<Product> Products { get; set; }  = new List<Product>();


  }
}
