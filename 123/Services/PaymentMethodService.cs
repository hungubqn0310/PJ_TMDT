using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;
using _123.Models;


namespace _123.Services
{
    public static class PaymentMethodService
    {
        // Thêm phương thức thanh toán mới
        public static int CreatePaymentMethod(PaymentMethod paymentMethod)
        {
            string query = @"INSERT INTO Payment_Methods (payment_method_name, is_deleted)
                            VALUES (@payment_method_name, 0)";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@payment_method_name", MySqlDbType.VarChar) { Value = paymentMethod.PaymentMethodName }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Lấy danh sách phương thức thanh toán
        public static List<PaymentMethod> GetPaymentMethods()
        {
            string query = "SELECT payment_method_id, payment_method_name, is_deleted FROM Payment_Methods WHERE is_deleted = 0";
            
            var paymentMethods = new List<PaymentMethod>();

            try
            {
                DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    paymentMethods.Add(new PaymentMethod
                    {
                        PaymentMethodId = Convert.ToInt32(row["payment_method_id"]),
                        PaymentMethodName = row["payment_method_name"].ToString(),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách phương thức thanh toán: {ex.Message}");
                throw;
            }

            return paymentMethods;
        }

        // Lấy phương thức thanh toán theo ID
        public static PaymentMethod GetPaymentMethodById(int paymentMethodId)
        {
            string query = "SELECT payment_method_id, payment_method_name, is_deleted FROM Payment_Methods WHERE payment_method_id = @paymentMethodId AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@paymentMethodId", MySqlDbType.Int32) { Value = paymentMethodId }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new PaymentMethod
                {
                    PaymentMethodId = Convert.ToInt32(row["payment_method_id"]),
                        PaymentMethodName = row["payment_method_name"].ToString(),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        // Cập nhật thông tin phương thức thanh toán
        public static int UpdatePaymentMethod(PaymentMethod paymentMethod)
        {
            string query = @"UPDATE Payment_Methods
                             SET payment_method_name = @payment_method_name
                             WHERE payment_method_id = @payment_method_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@payment_method_id", MySqlDbType.Int32) { Value = paymentMethod.PaymentMethodId },
                                new MySqlParameter("@payment_method_name", MySqlDbType.VarChar) { Value = paymentMethod.PaymentMethodName }

            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Xóa tạm thời phương thức thanh toán
        public static int DeletePaymentMethod(int paymentMethodId)
        {
            string query = @"UPDATE Payment_Methods
                             SET is_deleted = 1
                             WHERE payment_method_id = @payment_method_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@payment_method_id", MySqlDbType.Int32) { Value = paymentMethodId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
