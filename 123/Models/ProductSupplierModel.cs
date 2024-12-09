using _123.Models;

public class ProductSupplier
{
    public int PsId { get; set; }
    public string ProductId { get; set; }
    public int SupplierId { get; set; }
    public bool IsDeleted { get; set; }
    
    public Product Product { get; set; }
    public Supplier Supplier { get; set; }
}
