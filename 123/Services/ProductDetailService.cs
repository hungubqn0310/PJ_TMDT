using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using _123.Models;
using _123.Helpers;
namespace _123.Services
{
    public static class ProductDetailService
    {
        // Thêm thông tin chi tiết sản phẩm mới
        public static int CreateProductDetail(ProductDetail productDetail)
        {
            string query = @"INSERT INTO ProductDetail (product_id, ni_tay, kieu_dang, kieu_vien_chu, kich_thuoc_vien_chu, gioi_tinh, others, mau_kim_loai, da_tam, is_deleted)
                             VALUES (@product_id, @ni_tay, @kieu_dang, @kieu_vien_chu, @kich_thuoc_vien_chu, @gioi_tinh, @others, @mau_kim_loai, @da_tam, 0)";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = productDetail.ProductId },
                new MySqlParameter("@ni_tay", MySqlDbType.VarChar) { Value = productDetail.NiTay },
                new MySqlParameter("@kieu_dang", MySqlDbType.VarChar) { Value = productDetail.KieuDang },
                new MySqlParameter("@kieu_vien_chu", MySqlDbType.VarChar) { Value = productDetail.KieuVienChu },
                new MySqlParameter("@kich_thuoc_vien_chu", MySqlDbType.VarChar) { Value = productDetail.KichThuocVienChu },
                new MySqlParameter("@gioi_tinh", MySqlDbType.VarChar) { Value = productDetail.GioiTinh },
                new MySqlParameter("@others", MySqlDbType.VarChar) { Value = productDetail.Others },
                new MySqlParameter("@mau_kim_loai", MySqlDbType.VarChar) { Value = productDetail.MauKimLoai },
                new MySqlParameter("@da_tam", MySqlDbType.VarChar) { Value = productDetail.DaTam }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
public static int CreateProductDetails(List<ProductDetail> productDetails)
{
    // Xây dựng câu lệnh SQL với nhiều giá trị để chèn nhiều chi tiết sản phẩm
    var query = @"INSERT INTO ProductDetail (product_id, ni_tay, kieu_dang, kieu_vien_chu, kich_thuoc_vien_chu, gioi_tinh, others, mau_kim_loai, da_tam, is_deleted)
                  VALUES ";

    var parameters = new List<MySqlParameter>();
    var valueStrings = new List<string>();

    // Duyệt qua danh sách chi tiết sản phẩm và tạo các phần values cho câu lệnh SQL
    for (int i = 0; i < productDetails.Count; i++)
    {
        var productDetail = productDetails[i];

        valueStrings.Add(
            $"(@product_id{i}, @ni_tay{i}, @kieu_dang{i}, @kieu_vien_chu{i}, @kich_thuoc_vien_chu{i}, @gioi_tinh{i}, @others{i}, @mau_kim_loai{i}, @da_tam{i}, 0)");

        parameters.Add(new MySqlParameter($"@product_id{i}", MySqlDbType.VarChar) { Value = productDetail.ProductId });
        parameters.Add(new MySqlParameter($"@ni_tay{i}", MySqlDbType.VarChar) { Value = productDetail.NiTay });
        parameters.Add(new MySqlParameter($"@kieu_dang{i}", MySqlDbType.VarChar) { Value = productDetail.KieuDang });
        parameters.Add(new MySqlParameter($"@kieu_vien_chu{i}", MySqlDbType.VarChar) { Value = productDetail.KieuVienChu });
        parameters.Add(new MySqlParameter($"@kich_thuoc_vien_chu{i}", MySqlDbType.VarChar) { Value = productDetail.KichThuocVienChu });
        parameters.Add(new MySqlParameter($"@gioi_tinh{i}", MySqlDbType.VarChar) { Value = productDetail.GioiTinh });
        parameters.Add(new MySqlParameter($"@others{i}", MySqlDbType.VarChar) { Value = productDetail.Others });
        parameters.Add(new MySqlParameter($"@mau_kim_loai{i}", MySqlDbType.VarChar) { Value = productDetail.MauKimLoai });
        parameters.Add(new MySqlParameter($"@da_tam{i}", MySqlDbType.VarChar) { Value = productDetail.DaTam });
    }

    // Kết hợp các phần values vào câu lệnh SQL
    query += string.Join(", ", valueStrings);

    // Thực thi câu lệnh SQL với các tham số
    return DatabaseHelper.ExecuteNonQuery(query, parameters.ToArray());
}

        // Lấy danh sách các chi tiết sản phẩm
        public static List<ProductDetail> GetProductDetails()
        {
            string query = @"SELECT pd.product_detail_id, pd.product_id, pd.ni_tay, pd.kieu_dang, pd.kieu_vien_chu, pd.kich_thuoc_vien_chu, pd.gioi_tinh, 
                                    pd.others, pd.mau_kim_loai, pd.da_tam, pd.is_deleted, p.product_name
                             FROM ProductDetail pd
                             LEFT JOIN Products p ON p.product_id = pd.product_id
                             WHERE pd.is_deleted = 0";
            
            var productDetails = new List<ProductDetail>();

            try
            {
                DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    productDetails.Add(new ProductDetail
                    {
                        ProductDetailId = Convert.ToInt32(row["product_detail_id"]),
                        ProductId = row["product_id"].ToString(),
                        NiTay = row["ni_tay"].ToString(),
                        KieuDang = row["kieu_dang"].ToString(),
                        KieuVienChu = row["kieu_vien_chu"].ToString(),
                        KichThuocVienChu = row["kich_thuoc_vien_chu"].ToString(),
                        GioiTinh = row["gioi_tinh"].ToString(),
                        Others = row["others"].ToString(),
                        MauKimLoai = row["mau_kim_loai"].ToString(),
                        DaTam = row["da_tam"].ToString(),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"]),
                        Product = row["product_name"] == DBNull.Value ? null : new Product
                        {
                            ProductId =  row["product_id"].ToString(),
                            ProductName = row["product_name"].ToString()
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách chi tiết sản phẩm: {ex.Message}");
                throw;
            }

            return productDetails;
        }

        // Lấy chi tiết sản phẩm theo ID
        public static ProductDetail GetProductDetailById(int productDetailId)
        {
            string query = @"SELECT product_detail_id, product_id, ni_tay, kieu_dang, kieu_vien_chu, kich_thuoc_vien_chu, gioi_tinh, 
                                     others, mau_kim_loai, da_tam, is_deleted
                             FROM ProductDetail 
                             WHERE product_detail_id = @productDetailId AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@productDetailId", MySqlDbType.Int32) { Value = productDetailId }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new ProductDetail
                {
                    ProductDetailId = Convert.ToInt32(row["product_detail_id"]),
                    ProductId = row["product_id"].ToString(),
                    NiTay = row["ni_tay"].ToString(),
                    KieuDang = row["kieu_dang"].ToString(),
                    KieuVienChu = row["kieu_vien_chu"].ToString(),
                    KichThuocVienChu = row["kich_thuoc_vien_chu"].ToString(),
                    GioiTinh = row["gioi_tinh"].ToString(),
                    Others = row["others"].ToString(),
                    MauKimLoai = row["mau_kim_loai"].ToString(),
                    DaTam = row["da_tam"].ToString(),
                    IsDeleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        // Cập nhật chi tiết sản phẩm
        public static int UpdateProductDetail(ProductDetail productDetail)
        {
            string query = @"UPDATE ProductDetail
                             SET ni_tay = @ni_tay,
                                 kieu_dang = @kieu_dang,
                                 kieu_vien_chu = @kieu_vien_chu,
                                 kich_thuoc_vien_chu = @kich_thuoc_vien_chu,
                                 gioi_tinh = @gioi_tinh,
                                 others = @others,
                                 mau_kim_loai = @mau_kim_loai,
                                 da_tam = @da_tam
                             WHERE product_detail_id = @product_detail_id AND is_deleted = 0";
    
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@product_detail_id", MySqlDbType.Int32) { Value = productDetail.ProductDetailId },
                new MySqlParameter("@ni_tay", MySqlDbType.VarChar) { Value = productDetail.NiTay },
                new MySqlParameter("@kieu_dang", MySqlDbType.VarChar) { Value = productDetail.KieuDang },
                new MySqlParameter("@kieu_vien_chu", MySqlDbType.VarChar) { Value = productDetail.KieuVienChu },
                new MySqlParameter("@kich_thuoc_vien_chu", MySqlDbType.VarChar) { Value = productDetail.KichThuocVienChu },
                new MySqlParameter("@gioi_tinh", MySqlDbType.VarChar) { Value = productDetail.GioiTinh },
                new MySqlParameter("@others", MySqlDbType.VarChar) { Value = productDetail.Others },
                new MySqlParameter("@mau_kim_loai", MySqlDbType.VarChar) { Value = productDetail.MauKimLoai },
                new MySqlParameter("@da_tam", MySqlDbType.VarChar) { Value = productDetail.DaTam }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Xóa tạm thời chi tiết sản phẩm
        public static int DeleteProductDetail(int productDetailId)
        {
            string query = @"UPDATE ProductDetail
                             SET is_deleted = 1
                             WHERE product_detail_id = @product_detail_id AND is_deleted = 0";
    
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@product_detail_id", MySqlDbType.Int32) { Value = productDetailId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
