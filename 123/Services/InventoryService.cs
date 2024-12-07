using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using _123.Models;
using MySql.Data.MySqlClient;

namespace _123.Services
{
    public static class InventoryService
    {
        public static int CreateInventory(Inventory inventory)
        {
            string query = @"INSERT INTO Inventory (product_id, quantity_in_stock, last_updated, is_deleted)
                            VALUES (@product_id, @quantity_in_stock, @last_updated, 0)";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = inventory.ProductId },
                new MySqlParameter("@quantity_in_stock", MySqlDbType.Int32) { Value = inventory.QuantityInStock },
                new MySqlParameter("@last_updated", MySqlDbType.DateTime) { Value = inventory.LastUpdated }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static List<Inventory> GetInventories()
        {
            string query = @"
                SELECT 
                    i.inventory_id, 
                    i.product_id, 
                    i.quantity_in_stock, 
                    i.last_updated, 
                    i.is_deleted,
                    p.product_name
                FROM Inventory i
                LEFT JOIN Products p ON i.product_id = p.product_id
                WHERE i.is_deleted = 0";

            var inventories = new List<Inventory>();

            try
            {
                DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    inventories.Add(new Inventory
                    {
                        InventoryId = Convert.ToInt32(row["inventory_id"]),
                        ProductId = row["product_id"].ToString(),
                        QuantityInStock = Convert.ToInt32(row["quantity_in_stock"]),
                        LastUpdated = Convert.ToDateTime(row["last_updated"]),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"]),
                        Product = new Product
                        {
                            ProductId = row["product_id"].ToString(),
                            ProductName = row["product_name"]?.ToString()
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving inventories: {ex.Message}");
                throw;
            }

            return inventories;
        }

        public static Inventory GetInventoryById(int inventoryId)
        {
            string query = @"
                SELECT 
                    i.inventory_id, 
                    i.product_id, 
                    i.quantity_in_stock, 
                    i.last_updated, 
                    i.is_deleted,
                    p.product_name
                FROM Inventory i
                LEFT JOIN Products p ON i.product_id = p.product_id
                WHERE i.inventory_id = @inventory_id AND i.is_deleted = 0";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@inventory_id", MySqlDbType.Int32) { Value = inventoryId }
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
                    IsDeleted = Convert.ToBoolean(row["is_deleted"]),
                    Product = new Product
                    {
                        ProductId = row["product_id"].ToString(),
                        ProductName = row["product_name"]?.ToString()
                    }
                };
            }

            return null;
        }

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
