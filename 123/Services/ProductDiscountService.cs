using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;

namespace _123.Services
{
    public static class ProductDiscountService
    {
        // Thêm khuyến mãi cho sản phẩm
        public static int CreateProductDiscount(string productId, int discountId)
        {
            // Câu lệnh SQL để thêm khuyến mãi cho sản phẩm
            string query = @"INSERT INTO Product_Discount (product_id, discount_id, is_deleted)
                            VALUES (@product_id, @discount_id, 0)";
            
            // Các tham số truyền vào câu lệnh SQL
            var parameters = new MySqlParameter[] 
            {
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = productId },
                new MySqlParameter("@discount_id", MySqlDbType.Int32) { Value = discountId }
            };

            // Thực thi câu lệnh INSERT và trả về số dòng bị ảnh hưởng
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Lấy danh sách tất cả các khuyến mãi cho sản phẩm
        public static List<ProductDiscount> GetProductDiscounts()
        {
            string query = "SELECT product_id, discount_id, is_deleted FROM Product_Discount WHERE is_deleted = 0";
            
            var productDiscounts = new List<ProductDiscount>();

            try
            {
                // Gọi hàm ExecuteQuery từ DatabaseHelper
                DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    productDiscounts.Add(new ProductDiscount
                    {
                        ProductId = row["product_id"].ToString(),
                        DiscountId = Convert.ToInt32(row["discount_id"]),
                        is_deleted = Convert.ToBoolean(row["is_deleted"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách product discounts: {ex.Message}");
                throw;
            }

            return productDiscounts;
        }

        // Lấy thông tin khuyến mãi của sản phẩm theo product_id
        public static ProductDiscount GetDiscountsByProductId(string productId)
        {
            string query = "SELECT product_id, discount_id, is_deleted FROM Product_Discount WHERE product_id = @product_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[] 
            {
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = productId }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0]; // Lấy dòng đầu tiên
                return new ProductDiscount
                {
                    ProductId = row["product_id"].ToString(),
                    DiscountId = Convert.ToInt32(row["discount_id"]),
                    is_deleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null; // Nếu không tìm thấy
        }

        // Cập nhật thông tin khuyến mãi của sản phẩm
        public static int UpdateProductDiscount(ProductDiscount productDiscount)
        {
            string query = @"UPDATE Product_Discount
                            SET discount_id = @discount_id
                            WHERE product_id = @product_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[] 
            {
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = productDiscount.ProductId },
                new MySqlParameter("@discount_id", MySqlDbType.Int32) { Value = productDiscount.DiscountId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Xóa tạm thời khuyến mãi của sản phẩm
        public static int DeleteProductDiscount(string productId, int discountId)
        {
            string query = @"UPDATE Product_Discount
                            SET is_deleted = 1
                            WHERE product_id = @product_id AND discount_id = @discount_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[] 
            {
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = productId },
                new MySqlParameter("@discount_id", MySqlDbType.Int32) { Value = discountId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
