using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;
using _123.Models;


namespace _123.Services
{
    public static class OrderPaymentService
    {
        // Thêm một khoản thanh toán cho đơn hàng
        public static int CreateOrderPayment(OrderPayment OrderPayment)
        {
            string query = @"INSERT INTO Order_Items (order_id, PaymentMethod_id, amount_paid, payment_date, is_deleted)
                             VALUES (@order_id, @PaymentMethod_id, @amount_paid, @payment_date, 0)";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@order_id", MySqlDbType.Int32) { Value = OrderPayment.OrderId },
                new MySqlParameter("@PaymentMethod_id", MySqlDbType.Int32) { Value = OrderPayment.OrderPaymentId },
                new MySqlParameter("@amount_paid", MySqlDbType.Decimal) { Value = OrderPayment.AmountPaid },
                new MySqlParameter("@payment_date", MySqlDbType.DateTime) { Value = OrderPayment.PaymentDate }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

public static List<OrderPayment> GetOrderPayments()
    {
        string query = "SELECT OrderPayment_id, order_id, PaymentMethod_id, amount_paid, payment_date, is_deleted FROM Order_Items WHERE is_deleted = 0";
        var orderPayments = new List<OrderPayment>();

        try
        {
            DataTable dataTable = DatabaseHelper.ExecuteQuery(query);
            foreach (DataRow row in dataTable.Rows)
            {
                orderPayments.Add(new OrderPayment
                {
                    OrderPaymentId = Convert.ToInt32(row["OrderPayment_id"]),
                    OrderId = Convert.ToInt32(row["order_id"]),
                    PaymentMethodId = Convert.ToInt32(row["PaymentMethod_id"]),
                    AmountPaid = Convert.ToDecimal(row["amount_paid"]),
                    PaymentDate = Convert.ToDateTime(row["payment_date"]),
                    IsDeleted = Convert.ToBoolean(row["is_deleted"])
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
        public static List<OrderPayment> GetOrderPaymentsByOrderId(int orderId)
        {
            string query = "SELECT OrderPayment_id, order_id, PaymentMethod_id, amount_paid, payment_date, is_deleted FROM Order_Items WHERE order_id = @orderId AND is_deleted = 0";
            
            var OrderPayments = new List<OrderPayment>();

            try
            {
                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@orderId", MySqlDbType.Int32) { Value = orderId }
                };

                DataTable dataTable = DatabaseHelper.ExecuteQuery(query, parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    OrderPayments.Add(new OrderPayment
                    {
                        OrderPaymentId = Convert.ToInt32(row["OrderPayment_id"]),
                    OrderId = Convert.ToInt32(row["order_id"]),
                    PaymentMethodId = Convert.ToInt32(row["PaymentMethod_id"]),
                    AmountPaid = Convert.ToDecimal(row["amount_paid"]),
                    PaymentDate = Convert.ToDateTime(row["payment_date"]),
                    IsDeleted = Convert.ToBoolean(row["is_deleted"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách thanh toán: {ex.Message}");
                throw;
            }

            return OrderPayments;
        }

        // Lấy khoản thanh toán theo ID
        public static OrderPayment GetOrderPaymentById(int OrderPaymentId)
        {
            string query = "SELECT OrderPayment_id, order_id, PaymentMethod_id, amount_paid, payment_date, is_deleted FROM Order_Items WHERE OrderPayment_id = @OrderPaymentId AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@OrderPaymentId", MySqlDbType.Int32) { Value = OrderPaymentId }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new OrderPayment
                {
                    OrderPaymentId = Convert.ToInt32(row["OrderPayment_id"]),
                    OrderId = Convert.ToInt32(row["order_id"]),
                    PaymentMethodId = Convert.ToInt32(row["PaymentMethod_id"]),
                    AmountPaid = Convert.ToDecimal(row["amount_paid"]),
                    PaymentDate = Convert.ToDateTime(row["payment_date"]),
                    IsDeleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        // Cập nhật khoản thanh toán cho đơn hàng
        public static int UpdateOrderPayment(OrderPayment OrderPayment)
        {
            string query = @"UPDATE Order_Items
                             SET PaymentMethod_id = @PaymentMethod_id,
                                 amount_paid = @amount_paid,
                                 payment_date = @payment_date
                             WHERE OrderPayment_id = @OrderPayment_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@order_id", MySqlDbType.Int32) { Value = OrderPayment.OrderId },
                new MySqlParameter("@PaymentMethod_id", MySqlDbType.Int32) { Value = OrderPayment.OrderPaymentId },
                new MySqlParameter("@amount_paid", MySqlDbType.Decimal) { Value = OrderPayment.AmountPaid },
                new MySqlParameter("@payment_date", MySqlDbType.DateTime) { Value = OrderPayment.PaymentDate }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Xóa tạm thời khoản thanh toán
        public static int DeleteOrderPayment(int OrderPaymentId)
        {
            string query = @"UPDATE Order_Items
                             SET is_deleted = 1
                             WHERE OrderPayment_id = @OrderPayment_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@OrderPayment_id", MySqlDbType.Int32) { Value = OrderPaymentId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
