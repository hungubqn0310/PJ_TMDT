using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;

namespace _123.Services
{
    public static class ProductSupplierService
    {
        // Phương thức tạo mới một bản ghi ProductSupplier
        public static int CreateProductSupplier(ProductSupplier productSupplier)
        {
            string query = @"INSERT INTO Product_Supplier (product_id, supplier_id, is_deleted)
                            VALUES (@product_id, @supplier_id, 0)";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = productSupplier.ProductId },
                new MySqlParameter("@supplier_id", MySqlDbType.Int32) { Value = productSupplier.SupplierId },
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Phương thức lấy tất cả các bản ghi ProductSupplier chưa bị xóa
        public static List<ProductSupplier> GetProductSuppliers()
        {
            string query = "SELECT product_id, supplier_id, is_deleted FROM Product_Supplier WHERE is_deleted = 0";
            
            var productSuppliers = new List<ProductSupplier>();

            try
            {
                // Thực thi câu lệnh SQL và lấy kết quả
                DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    productSuppliers.Add(new ProductSupplier
                    {
                        ProductId = row["product_id"].ToString(),
                        SupplierId = Convert.ToInt32(row["supplier_id"]),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"]),
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách ProductSupplier: {ex.Message}");
                throw;
            }

            return productSuppliers;
        }

        // Phương thức lấy thông tin ProductSupplier theo product_id và supplier_id
        public static ProductSupplier GetProductSupplierById(string productId, int supplierId)
        {
            string query = "SELECT product_id, supplier_id, is_deleted FROM Product_Supplier WHERE product_id = @product_id AND supplier_id = @supplier_id AND is_deleted = 0";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = productId },
                new MySqlParameter("@supplier_id", MySqlDbType.Int32) { Value = supplierId },
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new ProductSupplier
                {
                    ProductId = row["product_id"].ToString(),
                    SupplierId = Convert.ToInt32(row["supplier_id"]),
                    IsDeleted = Convert.ToBoolean(row["is_deleted"]),
                };
            }

            return null;
        }

        // Phương thức cập nhật thông tin ProductSupplier
        public static int UpdateProductSupplier(ProductSupplier productSupplier)
        {
            string query = @"UPDATE Product_Supplier
                             SET is_deleted = @is_deleted
                             WHERE product_id = @product_id AND supplier_id = @supplier_id AND is_deleted = 0";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = productSupplier.ProductId },
                new MySqlParameter("@supplier_id", MySqlDbType.Int32) { Value = productSupplier.SupplierId },
                new MySqlParameter("@is_deleted", MySqlDbType.Int32) { Value = productSupplier.IsDeleted ? 1 : 0 },
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Phương thức xóa tạm thời ProductSupplier (đánh dấu is_deleted = 1)
        public static int DeleteProductSupplier(int supplierId)
        {
            string query = @"UPDATE Product_Supplier
                             SET is_deleted = 1
                             WHERE product_id = @product_id AND supplier_id = @supplier_id AND is_deleted = 0";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@supplier_id", MySqlDbType.Int32) { Value = supplierId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
