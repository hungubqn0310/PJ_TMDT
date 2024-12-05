using _123.Models;

public class ProductSupplier
{
    public string ProductId { get; set; }   // Mã sản phẩm
    public int SupplierId { get; set; }   // Mã nhà cung cấp
    public bool IsDeleted { get; set; }   // Trạng thái xóa (0 hoặc 1)

    // Điều này sẽ giúp định nghĩa mối quan hệ giữa Product và Supplier (nếu cần thiết trong Entity Framework)
    public Product Product { get; set; }
    public Supplier Supplier { get; set; }
}
