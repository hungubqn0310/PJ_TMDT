using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using _123.Helpers;

namespace _123.Services
{
    public static class ProductDiscountService
    {
        // Thêm khuyến mãi cho sản phẩm
        public static int CreateProductDiscount(ProductDiscount productDiscount)
        {
            // Câu lệnh SQL để thêm khuyến mãi cho sản phẩm
            string query = @"INSERT INTO Product_Discount (product_id, discount_id, is_deleted)
                             VALUES (@product_id, @discount_id, 0)";

            // Các tham số truyền vào câu lệnh SQL
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = productDiscount.ProductId },
                new MySqlParameter("@discount_id", MySqlDbType.Int32) { Value = productDiscount.DiscountId }
            };

            // Thực thi câu lệnh INSERT và trả về số dòng bị ảnh hưởng
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Lấy danh sách tất cả các khuyến mãi cho sản phẩm (chưa bị xóa)
        public static List<ProductDiscount> GetProductDiscounts()
        {
            string query = "SELECT pd_id, product_id, discount_id, is_deleted FROM Product_Discount WHERE is_deleted = 0";

            var productDiscounts = new List<ProductDiscount>();

            try
            {
                // Thực thi câu lệnh SQL và lấy kết quả
                DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

                // Duyệt qua từng dòng trong DataTable và tạo danh sách ProductDiscount
                foreach (DataRow row in dataTable.Rows)
                {
                    productDiscounts.Add(new ProductDiscount
                    {
                        PdId = Convert.ToInt32(row["pd_id"]),
                        ProductId = row["product_id"].ToString(),
                        DiscountId = Convert.ToInt32(row["discount_id"]),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách ProductDiscounts: {ex.Message}");
                throw;
            }

            return productDiscounts;
        }

        // Lấy thông tin khuyến mãi của sản phẩm theo product_id
        public static ProductDiscount GetDiscountsByProductId(int pd)
        {
            string query = "SELECT pd_id, product_id, discount_id, is_deleted FROM Product_Discount WHERE product_id = @product_id AND is_deleted = 0";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@pd_id", MySqlDbType.VarChar) { Value = pd }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0]; // Lấy dòng đầu tiên
                return new ProductDiscount
                {
                    PdId = Convert.ToInt32(row["pd_id"]),
                    ProductId = row["product_id"].ToString(),
                    DiscountId = Convert.ToInt32(row["discount_id"]),
                    IsDeleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null; // Nếu không tìm thấy
        }

        // Cập nhật thông tin khuyến mãi của sản phẩm
        public static int UpdateProductDiscount(ProductDiscount productDiscount)
        {
            string query = @"
                UPDATE Product_Discount
                SET product_id = @product_id,
                    discount_id = @discount_id,
                    is_deleted = @is_deleted
                WHERE pd_id = @pd_id ";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@pd_id", MySqlDbType.Int32) { Value = productDiscount.PdId },
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = productDiscount.ProductId },
                new MySqlParameter("@discount_id", MySqlDbType.Int32) { Value = productDiscount.DiscountId },
                new MySqlParameter("@is_deleted", MySqlDbType.Int32) { Value = productDiscount.IsDeleted ? 1 : 0 }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }


        // Xóa tạm thời khuyến mãi của sản phẩm (đánh dấu is_deleted = 1)
        public static int DeleteProductDiscount(int pdId)
        {
            string query = @"UPDATE Product_Discount
                             SET is_deleted = 1
                             WHERE pd_id = @pd_id ";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@pd_id", MySqlDbType.Int32) { Value = pdId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }  
}
