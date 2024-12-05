using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;

namespace _123.Services
{
    public static class InventoryService
    {
        // Thêm mới thông tin kho
        public static int CreateInventory(Inventory inventory)
        {
            // Câu lệnh SQL để thêm thông tin kho mới
            string query = @"INSERT INTO Inventory (product_id, quantity_in_stock, last_updated, is_deleted)
                            VALUES (@product_id, @quantity_in_stock, @last_updated, 0)";
            
            // Các tham số truyền vào câu lệnh SQL
            var parameters = new MySqlParameter[] 
            {
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = inventory.ProductId },
                new MySqlParameter("@quantity_in_stock", MySqlDbType.Int32) { Value = inventory.QuantityInStock },
                new MySqlParameter("@last_updated", MySqlDbType.DateTime) { Value = inventory.LastUpdated }
            };

            // Thực thi câu lệnh INSERT và trả về số dòng bị ảnh hưởng
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Lấy danh sách các tồn kho (chưa xóa)
        public static List<Inventory> GetInventories()
        {
            string query = "SELECT inventory_id, product_id, quantity_in_stock, last_updated, is_deleted FROM Inventory WHERE is_deleted = 0";
            
            var inventories = new List<Inventory>();

            try
            {
                // Gọi hàm ExecuteQuery từ DatabaseHelper
                DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    inventories.Add(new Inventory
                    {
                        InventoryId = Convert.ToInt32(row["inventory_id"]),
                        ProductId = row["product_id"].ToString(),
                        QuantityInStock = Convert.ToInt32(row["quantity_in_stock"]),
                        LastUpdated = Convert.ToDateTime(row["last_updated"]),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách tồn kho: {ex.Message}");
                throw;
            }

            return inventories;
        }

        // Lấy thông tin tồn kho theo inventory_id
        public static Inventory GetInventoryById(int inventoryId)
        {
            string query = "SELECT inventory_id, product_id, quantity_in_stock, last_updated, is_deleted FROM Inventory WHERE inventory_id = @inventoryId AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@inventoryId", MySqlDbType.Int32) { Value = inventoryId }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new Inventory
                {
                    InventoryId = Convert.ToInt32(row["inventory_id"]),
                    ProductId = row["product_id"].ToString(),
                    QuantityInStock = Convert.ToInt32(row["quantity_in_stock"]),
                    LastUpdated = Convert.ToDateTime(row["last_updated"]),
                    IsDeleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        // Cập nhật thông tin tồn kho
        public static int UpdateInventory(Inventory inventory)
        {
            string query = @"UPDATE Inventory
                            SET quantity_in_stock = @quantity_in_stock,
                                last_updated = @last_updated
                            WHERE inventory_id = @inventory_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@inventory_id", MySqlDbType.Int32) { Value = inventory.InventoryId },
                new MySqlParameter("@quantity_in_stock", MySqlDbType.Int32) { Value = inventory.QuantityInStock },
                new MySqlParameter("@last_updated", MySqlDbType.DateTime) { Value = inventory.LastUpdated }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Xóa tồn kho (đánh dấu is_deleted = 1)
        public static int DeleteInventory(int inventoryId)
        {
            string query = @"UPDATE Inventory
                            SET is_deleted = 1
                            WHERE inventory_id = @inventory_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[] 
            {
                new MySqlParameter("@inventory_id", MySqlDbType.Int32) { Value = inventoryId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
