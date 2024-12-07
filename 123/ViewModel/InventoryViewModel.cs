using _123.Models;
namespace _123 {
  public class InventoryViewModel
  {
    public List<Inventory> Inventories {get; set;}

    public Inventory Inventory {get; set;}
    public IEnumerable<Product> Products { get; set; }  = new List<Product>();
  }
}
