using _123.Models;

public class ProductDiscount
{
    public int PdId { get; set; }        // ID của bản ghi
    public string ProductId { get; set; } // ID của sản phẩm
    public int DiscountId { get; set; }   // ID của khuyến mãi
    public bool IsDeleted { get; set; }   // Trạng thái xóa (is_deleted)
    public Product Product { get; set; }
    public Discount Discount { get; set; }
}