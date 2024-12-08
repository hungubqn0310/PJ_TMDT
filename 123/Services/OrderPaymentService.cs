using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;

namespace _123.Services
{
    public static class Order_PaymentService
    {
        // Thêm một khoản thanh toán cho đơn hàng
        public static int CreateOrder_Payment(Order_Payment Order_Payment)
        {
            string query = @"INSERT INTO Order_Payments (order_id, payment_method_id, amount_paid, payment_date, is_deleted)
                             VALUES (@order_id, @payment_method_id, @amount_paid, @payment_date, 0)";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@order_id", MySqlDbType.Int32) { Value = Order_Payment.order_id },
                new MySqlParameter("@payment_method_id", MySqlDbType.Int32) { Value = Order_Payment.payment_method_id },
                new MySqlParameter("@amount_paid", MySqlDbType.Decimal) { Value = Order_Payment.amount_paid },
                new MySqlParameter("@payment_date", MySqlDbType.DateTime) { Value = Order_Payment.payment_date }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

public static List<Order_Payment> GetOrderPayments()
    {
        string query = "SELECT order_payment_id, order_id, payment_method_id, amount_paid, payment_date, is_deleted FROM Order_Payments WHERE is_deleted = 0";
        var orderPayments = new List<Order_Payment>();

        try
        {
            DataTable dataTable = DatabaseHelper.ExecuteQuery(query);
            foreach (DataRow row in dataTable.Rows)
            {
                orderPayments.Add(new Order_Payment
                {
                    order_payment_id = Convert.ToInt32(row["order_payment_id"]),
                    order_id = Convert.ToInt32(row["order_id"]),
                    payment_method_id = Convert.ToInt32(row["payment_method_id"]),
                    amount_paid = Convert.ToDecimal(row["amount_paid"]),
                    payment_date = Convert.ToDateTime(row["payment_date"]),
                    is_deleted = Convert.ToBoolean(row["is_deleted"])
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi lấy danh sách thanh toán đơn hàng: {ex.Message}");
            throw;
        }

        return orderPayments;
    }
        // Lấy tất cả các khoản thanh toán cho đơn hàng
        public static List<Order_Payment> GetOrder_PaymentsByOrderId(int orderId)
        {
            string query = "SELECT order_payment_id, order_id, payment_method_id, amount_paid, payment_date, is_deleted FROM Order_Payments WHERE order_id = @orderId AND is_deleted = 0";
            
            var Order_Payments = new List<Order_Payment>();

            try
            {
                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@orderId", MySqlDbType.Int32) { Value = orderId }
                };

                DataTable dataTable = DatabaseHelper.ExecuteQuery(query, parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    Order_Payments.Add(new Order_Payment
                    {
                        order_payment_id = Convert.ToInt32(row["order_payment_id"]),
                        order_id = Convert.ToInt32(row["order_id"]),
                        payment_method_id = Convert.ToInt32(row["payment_method_id"]),
                        amount_paid = Convert.ToDecimal(row["amount_paid"]),
                        payment_date = Convert.ToDateTime(row["payment_date"]),
                        is_deleted = Convert.ToBoolean(row["is_deleted"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách thanh toán: {ex.Message}");
                throw;
            }

            return Order_Payments;
        }

        // Lấy khoản thanh toán theo ID
        public static Order_Payment GetOrder_PaymentById(int Order_PaymentId)
        {
            string query = "SELECT order_payment_id, order_id, payment_method_id, amount_paid, payment_date, is_deleted FROM Order_Payments WHERE order_payment_id = @Order_PaymentId AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Order_PaymentId", MySqlDbType.Int32) { Value = Order_PaymentId }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new Order_Payment
                {
                    order_payment_id = Convert.ToInt32(row["order_payment_id"]),
                    order_id = Convert.ToInt32(row["order_id"]),
                    payment_method_id = Convert.ToInt32(row["payment_method_id"]),
                    amount_paid = Convert.ToDecimal(row["amount_paid"]),
                    payment_date = Convert.ToDateTime(row["payment_date"]),
                    is_deleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        // Cập nhật khoản thanh toán cho đơn hàng
        public static int UpdateOrder_Payment(Order_Payment Order_Payment)
        {
            string query = @"UPDATE Order_Payments
                             SET payment_method_id = @payment_method_id,
                                 amount_paid = @amount_paid,
                                 payment_date = @payment_date
                             WHERE order_payment_id = @order_payment_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@order_payment_id", MySqlDbType.Int32) { Value = Order_Payment.order_payment_id },
                new MySqlParameter("@payment_method_id", MySqlDbType.Int32) { Value = Order_Payment.payment_method_id },
                new MySqlParameter("@amount_paid", MySqlDbType.Decimal) { Value = Order_Payment.amount_paid },
                new MySqlParameter("@payment_date", MySqlDbType.DateTime) { Value = Order_Payment.payment_date }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Xóa tạm thời khoản thanh toán
        public static int DeleteOrder_Payment(int Order_PaymentId)
        {
            string query = @"UPDATE Order_Payments
                             SET is_deleted = 1
                             WHERE order_payment_id = @order_payment_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@order_payment_id", MySqlDbType.Int32) { Value = Order_PaymentId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
