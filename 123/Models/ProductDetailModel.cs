using System;

namespace _123.Models
{
    public class ProductDetail
    {
        public int ProductDetailId { get; set; } // `product_detail_id`
        public string ProductId { get; set; } // `product_id`
        public string NiTay { get; set; } // `ni_tay`
        public string KieuDang { get; set; } // `kieu_dang`
        public string KieuVienChu { get; set; } // `kieu_vien_chu`
        public string KichThuocVienChu { get; set; } // `kich_thuoc_vien_chu`
        public string GioiTinh { get; set; } // `gioi_tinh`
        public string Others { get; set; } // `others`
        public string MauKimLoai { get; set; } // `mau_kim_loai`
        public string DaTam { get; set; } // `da_tam`
        public bool IsDeleted { get; set; } // `is_deleted`

        // Navigation property
        public virtual Product Product { get; set; } // Quan hệ với Product
    }
}
