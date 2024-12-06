using System;

namespace _123.Models
{
    public class Inventories
    {
        public int InventoryId { get; set; }   // inventory_id
        public string ProductId { get; set; }   // product_id
        public int QuantityInStock { get; set; } // quantity_in_stock
        public DateTime LastUpdated { get; set; } // last_updated
        public bool IsDeleted { get; set; } = false; // is_deleted (mặc định là false)

        // Quan hệ với Product
        public virtual Product Product { get; set; }

        // Constructor mặc định
        public Inventories() { }

        // Constructor đầy đủ
        public Inventories(string productId, int quantityInStock, DateTime lastUpdated, bool isDeleted)
        {
            ProductId = productId;
            QuantityInStock = quantityInStock;
            LastUpdated = lastUpdated;
            IsDeleted = isDeleted;
        }

        public override string ToString()
        {
            return $"Inventory: ProductId = {ProductId}, QuantityInStock = {QuantityInStock}, LastUpdated = {LastUpdated}, IsDeleted = {IsDeleted}";
        }
    }
}
