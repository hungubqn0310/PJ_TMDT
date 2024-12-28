using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;
using _123.Models;


namespace _123.Services
{
    public static class TransactionHistoryService
    {
        // Thêm mới giao dịch vào Transaction_History
        public static int CreateTransaction(TransactionHistory transaction)
        {
            try { 
            string query = @"INSERT INTO Transaction_History (user_id, order_id, transaction_date, amount, payment_method_id, status, is_deleted)
                             VALUES (@user_id, @order_id, @transaction_date, @amount, @payment_method_id, @status, 0)";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = transaction.UserId },
                new MySqlParameter("@order_id", MySqlDbType.Int32) { Value = transaction.OrderId },
                new MySqlParameter("@transaction_date", MySqlDbType.DateTime) { Value = transaction.TransactionDate },
                new MySqlParameter("@amount", MySqlDbType.Decimal) { Value = transaction.Amount },
                new MySqlParameter("@payment_method_id", MySqlDbType.Int32) { Value = transaction.PaymentMethodId },
                new MySqlParameter("@status", MySqlDbType.VarChar) { Value = transaction.Status }
            };

            // Chạy câu lệnh INSERT và trả về số dòng bị ảnh hưởng
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
            } catch (Exception ex) {
                Console.WriteLine("Error");
                return 0;
            }
        }
public static List<TransactionHistory> GetAllTransactions()
{
    string query = @"SELECT th.transaction_id, th.user_id, th.order_id, th.transaction_date, th.amount, 
    th.payment_method_id, th.status, th.is_deleted , u.username, pm.payment_method_name
    FROM Transaction_History th
    LEFT JOIN Users  u ON u.user_id = th.user_id
    LEFT JOIN Orders   o ON o.order_id = th.order_id
    LEFT JOIN Payment_Methods  pm ON pm.payment_method_id = th.payment_method_id
    WHERE th.is_deleted = 0";
    
    var transactions = new List<TransactionHistory>();

    try
    {
        // No need for parameters here since we're not filtering by user
        DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

        foreach (DataRow row in dataTable.Rows)
        {
            transactions.Add(new TransactionHistory
            {
                TransactionId = Convert.ToInt32(row["transaction_id"]),
                UserId = Convert.ToInt32(row["user_id"]),
                OrderId = Convert.ToInt32(row["order_id"]),
                TransactionDate = Convert.ToDateTime(row["transaction_date"]),
                Amount = Convert.ToDecimal(row["amount"]),
                PaymentMethodId = Convert.ToInt32(row["payment_method_id"]),
                Status = row["status"].ToString(),
                IsDeleted = Convert.ToBoolean(row["is_deleted"]),
                User = row["username"] == DBNull.Value ? null : new User
                {
                    user_id = Convert.ToInt32(row["user_id"]),
                    username = row["username"].ToString()
                },
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
        Console.WriteLine($"Lỗi khi lấy danh sách tất cả giao dịch: {ex.Message}");
        throw;
    }

    return transactions;
}

        // Lấy danh sách giao dịch của người dùng
        public static List<TransactionHistory> GetTransactionsByuser_id(int user_id)
        {
            string query = "SELECT transaction_id, user_id, order_id, transaction_date, amount, payment_method_id, status, is_deleted FROM Transaction_History WHERE user_id = @user_id AND is_deleted = 0";
            
            var transactions = new List<TransactionHistory>();

            try
            {
                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = user_id }
                };

                DataTable dataTable = DatabaseHelper.ExecuteQuery(query, parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    transactions.Add(new TransactionHistory
                    {
                         TransactionId = Convert.ToInt32(row["transaction_id"]),
                        UserId = Convert.ToInt32(row["user_id"]),
                        OrderId = Convert.ToInt32(row["order_id"]),
                        TransactionDate = Convert.ToDateTime(row["transaction_date"]),
                        Amount = Convert.ToDecimal(row["amount"]),
                        PaymentMethodId = Convert.ToInt32(row["payment_method_id"]),
                        Status = row["status"].ToString(),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách giao dịch: {ex.Message}");
                throw;
            }

            return transactions;
        }

        // Lấy giao dịch theo ID
        public static TransactionHistory GetTransactionById(int transaction_id)
        {
            string query = "SELECT transaction_id, user_id, order_id, transaction_date, amount, payment_method_id, status, is_deleted FROM Transaction_History WHERE transaction_id = @transaction_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@transaction_id", MySqlDbType.Int32) { Value = transaction_id }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new TransactionHistory
                {
                     TransactionId = Convert.ToInt32(row["transaction_id"]),
                UserId = Convert.ToInt32(row["user_id"]),
                OrderId = Convert.ToInt32(row["order_id"]),
                TransactionDate = Convert.ToDateTime(row["transaction_date"]),
                Amount = Convert.ToDecimal(row["amount"]),
                PaymentMethodId = Convert.ToInt32(row["payment_method_id"]),
                Status = row["status"].ToString(),
                IsDeleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        // Cập nhật thông tin giao dịch
        public static int UpdateTransaction(TransactionHistory transaction)
        {
            string query = @"UPDATE Transaction_History
                             SET amount = @amount,
                                 status = @status
                             WHERE transaction_id = @transaction_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = transaction.UserId },
                new MySqlParameter("@order_id", MySqlDbType.Int32) { Value = transaction.OrderId },
                new MySqlParameter("@transaction_date", MySqlDbType.DateTime) { Value = transaction.TransactionDate },
                new MySqlParameter("@amount", MySqlDbType.Decimal) { Value = transaction.Amount },
                new MySqlParameter("@payment_method_id", MySqlDbType.Int32) { Value = transaction.PaymentMethodId },
                new MySqlParameter("@status", MySqlDbType.VarChar) { Value = transaction.Status }
            };

            // Chạy câu lệnh UPDATE và trả về số dòng bị ảnh hưởng
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Xóa giao dịch (đánh dấu bị xóa)
        public static int DeleteTransaction(int transaction_id)
        {
            string query = @"UPDATE Transaction_History
                             SET is_deleted = 1
                             WHERE transaction_id = @transaction_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@transaction_id", MySqlDbType.Int32) { Value = transaction_id }
            };

            // Chạy câu lệnh UPDATE và trả về số dòng bị ảnh hưởng
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }

    
}
