using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;

namespace _123.Services
{
    public static class OrderItemService
    {
        // Thêm một món hàng vào đơn hàng
        public static int CreateOrderItem(Order_Item orderItem)
        {
            string query = @"INSERT INTO Order_Items (order_id, product_name, quantity, price, is_deleted)
                             VALUES (@order_id, @product_name, @quantity, @price, 0)";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@order_id", MySqlDbType.Int32) { Value = orderItem.order_id },
                new MySqlParameter("@product_name", MySqlDbType.VarChar) { Value = orderItem.product_name },
                new MySqlParameter("@quantity", MySqlDbType.Int32) { Value = orderItem.quantity },
                new MySqlParameter("@price", MySqlDbType.Decimal) { Value = orderItem.price }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

// Lấy tất cả các món hàng (không phân theo order_id)
public static List<Order_Item> GetAllOrderItems()
{
    string query = "SELECT order_item_id, order_id, product_name, quantity, price, is_deleted FROM Order_Items WHERE is_deleted = 0";
    
    var orderItems = new List<Order_Item>();

    try
    {
        DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

        foreach (DataRow row in dataTable.Rows)
        {
            orderItems.Add(new Order_Item
            {
                order_item_id = Convert.ToInt32(row["order_item_id"]),
                order_id = Convert.ToInt32(row["order_id"]),
                product_name = row["product_name"].ToString(),
                quantity = Convert.ToInt32(row["quantity"]),
                price = Convert.ToDecimal(row["price"]),
                is_deleted = Convert.ToBoolean(row["is_deleted"])
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
        public static List<Order_Item> GetOrderItemsByOrderId(int orderId)
        {
            string query = "SELECT order_item_id, order_id, product_name, quantity, price, is_deleted FROM Order_Items WHERE order_id = @orderId AND is_deleted = 0";
            
            var orderItems = new List<Order_Item>();

            try
            {
                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@orderId", MySqlDbType.Int32) { Value = orderId }
                };

                DataTable dataTable = DatabaseHelper.ExecuteQuery(query, parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    orderItems.Add(new Order_Item
                    {
                        order_item_id = Convert.ToInt32(row["order_item_id"]),
                        order_id = Convert.ToInt32(row["order_id"]),
                        product_name = row["product_name"].ToString(),
                        quantity = Convert.ToInt32(row["quantity"]),
                        price = Convert.ToDecimal(row["price"]),
                        is_deleted = Convert.ToBoolean(row["is_deleted"])
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
        public static Order_Item GetOrderItemById(int orderItemId)
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
                return new Order_Item
                {
                    order_item_id = Convert.ToInt32(row["order_item_id"]),
                    order_id = Convert.ToInt32(row["order_id"]),
                    product_name = row["product_name"].ToString(),
                    quantity = Convert.ToInt32(row["quantity"]),
                    price = Convert.ToDecimal(row["price"]),
                    is_deleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        // Cập nhật thông tin món hàng
        public static int UpdateOrderItem(Order_Item orderItem)
        {
            string query = @"UPDATE Order_Items
                             SET product_name = @product_name,
                                 quantity = @quantity,
                                 price = @price
                             WHERE order_item_id = @order_item_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@order_item_id", MySqlDbType.Int32) { Value = orderItem.order_item_id },
                new MySqlParameter("@product_name", MySqlDbType.VarChar) { Value = orderItem.product_name },
                new MySqlParameter("@quantity", MySqlDbType.Int32) { Value = orderItem.quantity },
                new MySqlParameter("@price", MySqlDbType.Decimal) { Value = orderItem.price }
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
    }
}
