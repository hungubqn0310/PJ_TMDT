using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using _123.Models;
using MySql.Data.MySqlClient;

namespace _123.Services
{
    public static class OrderItemService
    {
        // Thêm một món hàng vào đơn hàng
        public static int CreateOrderItem(OrderItem orderItem)
        {
            string query = @"INSERT INTO Order_Items (order_id, product_name, quantity, price, is_deleted)
                             VALUES (@order_id, @product_name, @quantity, @price, 0)";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@order_id", MySqlDbType.Int32) { Value = orderItem.OrderId },
                new MySqlParameter("@product_name", MySqlDbType.VarChar) { Value = orderItem.ProductName },
                new MySqlParameter("@quantity", MySqlDbType.Int32) { Value = orderItem.Quantity },
                new MySqlParameter("@price", MySqlDbType.Decimal) { Value = orderItem.Price }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

// Lấy tất cả các món hàng (không phân theo order_id)
public static List<OrderItem> GetAllOrderItems()
{
    string query = @"SELECT oi.order_item_id, oi.order_id, oi.product_name, oi.quantity, oi.price, oi.is_deleted FROM Order_Items oi
    LEFT JOIN Orders o ON o.order_id = oi.order_id
    LEFT JOIN Products p ON p.product_name = oi.product_name
    WHERE oi.is_deleted = 0";
    
    var orderItems = new List<OrderItem>();

    try
    {
        DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

        foreach (DataRow row in dataTable.Rows)
        {
            orderItems.Add(new OrderItem
            {
                OrderItemId = Convert.ToInt32(row["order_item_id"]),
                OrderId = Convert.ToInt32(row["order_id"]),
                ProductName = row["product_name"].ToString(),
                Quantity = Convert.ToInt32(row["quantity"]),
                Price = Convert.ToDecimal(row["price"]),
                IsDeleted = Convert.ToBoolean(row["is_deleted"]),
                Order = row["order_id"] == DBNull.Value ? null : new Order
                {
                    OrderId = Convert.ToInt32(row["order_id"]),
                },
            });
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Lỗi khi lấy danh sách tất cả món hàng: {ex.Message}");
        throw;
    }

    return orderItems;
}

        // Lấy tất cả các món hàng trong một đơn hàng
        public static List<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            string query = "SELECT order_item_id, order_id, product_name, quantity, price, is_deleted FROM Order_Items WHERE order_id = @orderId AND is_deleted = 0";
            
            var orderItems = new List<OrderItem>();

            try
            {
                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@orderId", MySqlDbType.Int32) { Value = orderId }
                };

                DataTable dataTable = DatabaseHelper.ExecuteQuery(query, parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    orderItems.Add(new OrderItem
                    {
                        OrderItemId = Convert.ToInt32(row["order_item_id"]),
                        OrderId = Convert.ToInt32(row["order_id"]),
                        ProductName = row["product_name"].ToString(),
                        Quantity = Convert.ToInt32(row["quantity"]),
                        Price = Convert.ToDecimal(row["price"]),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách món hàng: {ex.Message}");
                throw;
            }

            return orderItems;
        }

        // Lấy một món hàng theo ID
        public static OrderItem GetOrderItemById(int orderItemId)
        {
            string query = "SELECT order_item_id, order_id, product_name, quantity, price, is_deleted FROM Order_Items WHERE order_item_id = @orderItemId AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@orderItemId", MySqlDbType.Int32) { Value = orderItemId }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new OrderItem
                {
                    OrderItemId = Convert.ToInt32(row["order_item_id"]),
                    OrderId = Convert.ToInt32(row["order_id"]),
                    ProductName = row["product_name"].ToString(),
                    Quantity = Convert.ToInt32(row["quantity"]),
                    Price = Convert.ToDecimal(row["price"]),
                    IsDeleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        // Cập nhật thông tin món hàng
        public static int UpdateOrderItem(OrderItem orderItem)
        {
            string query = @"UPDATE Order_Items
                             SET product_name = @product_name,
                                 quantity = @quantity,
                                 price = @price
                             WHERE order_item_id = @order_item_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@order_item_id", MySqlDbType.Int32) { Value = orderItem.OrderItemId },
                new MySqlParameter("@product_name", MySqlDbType.VarChar) { Value = orderItem.ProductName },
                new MySqlParameter("@quantity", MySqlDbType.Int32) { Value = orderItem.Quantity },
                new MySqlParameter("@price", MySqlDbType.Decimal) { Value = orderItem.Price }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Xóa tạm thời món hàng
        public static int DeleteOrderItem(int orderItemId)
        {
            string query = @"UPDATE Order_Items
                             SET is_deleted = 1
                             WHERE order_item_id = @order_item_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@order_item_id", MySqlDbType.Int32) { Value = orderItemId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static int CreateOrderItems(List<OrderItem> orderItems)
{
    int totalInserted = 0;
    string query = @"INSERT INTO Order_Items (order_id, product_name, quantity, price, is_deleted)
                     VALUES (@order_id, @product_name, @quantity, @price, 0)";

    // Loop through each order item and insert
    foreach (var orderItem in orderItems)
    {
        // Define parameters for each orderItem
         var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@order_id", MySqlDbType.Int32) { Value = orderItem.OrderId },
                new MySqlParameter("@product_name", MySqlDbType.VarChar) { Value = orderItem.ProductName },
                new MySqlParameter("@quantity", MySqlDbType.Int32) { Value = orderItem.Quantity },
                new MySqlParameter("@price", MySqlDbType.Decimal) { Value = orderItem.Price }
            };

        // Execute the query using DatabaseHelper.ExecuteNonQuery
        totalInserted += DatabaseHelper.ExecuteNonQuery(query, parameters);
    }

    return totalInserted; // Return the total number of inserted rows
}


    }
}
