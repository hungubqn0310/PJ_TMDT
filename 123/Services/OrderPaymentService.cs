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
            string query = @"INSERT INTO Order_Payments (order_id, payment_method_id, amount_paid, payment_date, is_deleted)
                             VALUES (@order_id, @payment_method_id, @amount_paid, @payment_date, 0)";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@order_id", MySqlDbType.Int32) { Value = OrderPayment.OrderId },
                new MySqlParameter("@payment_method_id", MySqlDbType.Int32) { Value = OrderPayment.PaymentMethodId },
                new MySqlParameter("@amount_paid", MySqlDbType.Decimal) { Value = OrderPayment.AmountPaid },
                new MySqlParameter("@payment_date", MySqlDbType.DateTime) { Value = OrderPayment.PaymentDate }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

public static List<OrderPayment> GetOrderPayments()
    {
        string query = @"SELECT op.order_payment_id, op.order_id, op.payment_method_id, op.amount_paid, op.payment_date, op.is_deleted, pm.payment_method_name
        FROM Order_Payments op
        LEFT JOIN Orders o ON o.order_id = op.order_id
        LEFT JOIN Payment_Methods pm ON pm.payment_method_id = op.payment_method_id
        WHERE op.is_deleted = 0";
        var orderPayments = new List<OrderPayment>();

        try
        {
            DataTable dataTable = DatabaseHelper.ExecuteQuery(query);
            foreach (DataRow row in dataTable.Rows)
            {
                orderPayments.Add(new OrderPayment
                {
                    OrderPaymentId = Convert.ToInt32(row["order_payment_id"]),
                    OrderId = Convert.ToInt32(row["order_id"]),
                    PaymentMethodId = Convert.ToInt32(row["payment_method_id"]),
                    AmountPaid = Convert.ToDecimal(row["amount_paid"]),
                    PaymentDate = Convert.ToDateTime(row["payment_date"]),
                    IsDeleted = Convert.ToBoolean(row["is_deleted"]),
                    Order = row["order_id"] == DBNull.Value ? null : new Order
                    {
                                OrderId = Convert.ToInt32(row["order_id"]),
                            },
                            PaymentMethod = row["payment_method_name"] == DBNull.Value ? null : new PaymentMethod
                        {
                                PaymentMethodId = Convert.ToInt32(row["payment_method_id"]),
                                PaymentMethodName = row["payment_method_name"].ToString()
                            },
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
            string query = "SELECT order_payment_id, order_id, payment_method_id, amount_paid, payment_date, is_deleted FROM Order_Payments WHERE order_id = @orderId AND is_deleted = 0";
            
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
                        OrderPaymentId = Convert.ToInt32(row["order_payment_id"]),
                    OrderId = Convert.ToInt32(row["order_id"]),
                    PaymentMethodId = Convert.ToInt32(row["payment_method_id"]),
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
            string query = "SELECT order_payment_id, order_id, payment_method_id, amount_paid, payment_date, is_deleted FROM Order_Payments WHERE order_payment_id = @OrderPaymentId AND is_deleted = 0";
            
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
                    OrderPaymentId = Convert.ToInt32(row["order_payment_id"]),
                    OrderId = Convert.ToInt32(row["order_id"]),
                    PaymentMethodId = Convert.ToInt32(row["payment_method_id"]),
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
    string query = @"UPDATE Order_Payments
                     SET payment_method_id = @payment_method_id,
                         amount_paid = @amount_paid,
                         payment_date = @payment_date
                     WHERE order_payment_id = @order_payment_id AND is_deleted = 0";
    
    var parameters = new MySqlParameter[]
    {
        new MySqlParameter("@payment_method_id", MySqlDbType.Int32) { Value = OrderPayment.PaymentMethodId },
        new MySqlParameter("@amount_paid", MySqlDbType.Decimal) { Value = OrderPayment.AmountPaid },
        new MySqlParameter("@payment_date", MySqlDbType.DateTime) { Value = OrderPayment.PaymentDate },
        new MySqlParameter("@order_payment_id", MySqlDbType.Int32) { Value = OrderPayment.OrderPaymentId }
    };

    return DatabaseHelper.ExecuteNonQuery(query, parameters);
}


        // Xóa tạm thời khoản thanh toán
        public static int DeleteOrderPayment(int OrderPaymentId)
        {
            string query = @"UPDATE Order_Payments
                             SET is_deleted = 1
                             WHERE order_payment_id = @order_payment_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@order_payment_id", MySqlDbType.Int32) { Value = OrderPaymentId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
