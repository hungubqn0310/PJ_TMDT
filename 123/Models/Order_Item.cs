using System;

namespace _123.Models
{
public class OrderItem
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool IsDeleted { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

}}