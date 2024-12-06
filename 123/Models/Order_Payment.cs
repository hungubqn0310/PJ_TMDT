public class Order_Payment
{
    public int order_payment_id { get; set; }
    public int order_id { get; set; }
    public int payment_method_id { get; set; }
    public decimal amount_paid { get; set; }
    public DateTime payment_date { get; set; }
    public bool is_deleted { get; set; }
}