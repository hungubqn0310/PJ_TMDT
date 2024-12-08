public class Review
{
    public int review_id { get; set; }
    public string product_id { get; set; }
    public int user_id { get; set; }
    public int rating { get; set; }
    public string comment { get; set; }
    public DateTime review_date { get; set; }
    public bool is_deleted { get; set; }
}
