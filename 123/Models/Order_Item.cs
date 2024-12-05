public class Order_Item
{
    public int order_item_id { get; set; }
    public int order_id { get; set; }
    public string product_name { get; set; }
    public int quantity { get; set; }
    public decimal price { get; set; }
    public bool is_deleted { get; set; }
}