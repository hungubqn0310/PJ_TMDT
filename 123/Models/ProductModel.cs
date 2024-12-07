using System;

namespace _123.Models
{
    public class Product
    {
        public string ProductId { get; set; } // `product_id`
        public string ProductName { get; set; } // `product_name`
        public string Description { get; set; } // `description`
        public decimal Price { get; set; } // `price`
        public int? MaterialId { get; set; } // `material_id`
        public int? CategoryId { get; set; } // `category_id`
        public string ImageUrl { get; set; } // `image_url`
        public bool IsDeleted { get; set; } // `is_deleted`
        public virtual Category Category { get; set; }
        public virtual Material Material { get; set; }
    }
}
