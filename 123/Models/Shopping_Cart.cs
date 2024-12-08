public class Shopping_Cart
{
    public int cart_id { get; set; }
    public int user_id { get; set; }
    public string product_id { get; set; }
    public int quantity { get; set; }
    public DateTime added_at { get; set; }
    public bool is_deleted { get; set; }
}