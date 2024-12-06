using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;
using _123.Models;


namespace _123.Services
{
    public static class OrderService
    {
        // Thêm đơn hàng mới
        public static int CreateOrder(Order order)
        {
            string query = @"INSERT INTO Orders (user_id, order_date, status, total_amount, is_deleted)
                             VALUES (@user_id, @order_date, @status, @total_amount, 0)";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = order.UserId },
                new MySqlParameter("@order_date", MySqlDbType.DateTime) { Value = order.OrderDate },
                new MySqlParameter("@status", MySqlDbType.VarChar) { Value = order.Status },
                new MySqlParameter("@total_amount", MySqlDbType.Decimal) { Value = order.TotalAmount }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Lấy danh sách các đơn hàng
        public static List<Order> GetOrders()
        {
            string query = "SELECT order_id, user_id, order_date, status, total_amount, is_deleted FROM Orders WHERE is_deleted = 0";
            
            var orders = new List<Order>();

            try
            {
                DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    orders.Add(new Order
                    {
                        OrderId = Convert.ToInt32(row["order_id"]),
                        UserId = Convert.ToInt32(row["user_id"]),
OrderDate = Convert.ToDateTime(row["order_date"]),
                        Status = row["status"].ToString(),
                        TotalAmount = Convert.ToDecimal(row["total_amount"]),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách đơn hàng: {ex.Message}");
                throw;
            }

            return orders;
        }

        // Lấy đơn hàng theo ID
        public static Order GetOrderById(int orderId)
        {
            string query = "SELECT order_id, user_id, order_date, status, total_amount, is_deleted FROM Orders WHERE order_id = @orderId AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@orderId", MySqlDbType.Int32) { Value = orderId }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new Order
                {
                    OrderId = Convert.ToInt32(row["order_id"]),
                        UserId = Convert.ToInt32(row["user_id"]),
OrderDate = Convert.ToDateTime(row["order_date"]),
                        Status = row["status"].ToString(),
                        TotalAmount = Convert.ToDecimal(row["total_amount"]),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        // Cập nhật đơn hàng
        public static int UpdateOrder(Order order)
        {
            string query = @"UPDATE Orders
                             SET status = @status,
                                 total_amount = @total_amount
                             WHERE order_id = @order_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = order.UserId },
                new MySqlParameter("@order_date", MySqlDbType.DateTime) { Value = order.OrderDate },
                new MySqlParameter("@status", MySqlDbType.VarChar) { Value = order.Status },
                new MySqlParameter("@total_amount", MySqlDbType.Decimal) { Value = order.TotalAmount }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Xóa tạm thời đơn hàng
        public static int DeleteOrder(int orderId)
        {
            string query = @"UPDATE Orders
                             SET is_deleted = 1
                             WHERE order_id = @order_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@order_id", MySqlDbType.Int32) { Value = orderId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
