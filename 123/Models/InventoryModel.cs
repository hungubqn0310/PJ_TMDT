using _123.Models;

public class Inventory
{
    public int InventoryId { get; set; }   // inventory_id
    public string ProductId { get; set; }   // product_id
    public int QuantityInStock { get; set; } // quantity_in_stock
    public DateTime LastUpdated { get; set; } // last_updated
    public bool IsDeleted { get; set; } = false; // is_deleted (mặc định là false)
    public virtual Product Product { get; set; }
}