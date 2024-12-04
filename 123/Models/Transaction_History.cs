public class Transaction_History
{
    public int transaction_id { get; set; }
    public int user_id { get; set; }
    public int order_id { get; set; }
    public DateTime transaction_date { get; set; }
    public decimal amount { get; set; }
    public int payment_method_id { get; set; }
    public string status { get; set; }
    public bool is_deleted { get; set; }
}
