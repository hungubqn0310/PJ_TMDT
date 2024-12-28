using _123.Models;

namespace _123 {
  public class ProductSupplierViewModel
  {
    public List<ProductSupplier> ProductSuppliers {get; set;}

    public ProductSupplier ProductSupplier {get; set;}
    public string ProductId { get; set; } // Thêm thuộc tính ProductId
    public int SupplierId { get; set; } 
    public IEnumerable<Product> Products { get; set; }  = new List<Product>();
    public IEnumerable<Supplier> Suppliers { get; set; }  = new List<Supplier>();
  }
}