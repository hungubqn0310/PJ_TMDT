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
            string query = @"INSERT INTO Transaction_History (user_id, order_id, transaction_date, amount, PaymentMethod_id, status, is_deleted)
                             VALUES (@user_id, @order_id, @transaction_date, @amount, @PaymentMethod_id, @status, 0)";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = transaction.UserId },
                new MySqlParameter("@order_id", MySqlDbType.Int32) { Value = transaction.OrderId },
                new MySqlParameter("@transaction_date", MySqlDbType.DateTime) { Value = transaction.TransactionDate },
                new MySqlParameter("@amount", MySqlDbType.Decimal) { Value = transaction.Amount },
                new MySqlParameter("@PaymentMethod_id", MySqlDbType.Int32) { Value = transaction.PaymentMethodId },
                new MySqlParameter("@status", MySqlDbType.VarChar) { Value = transaction.Status }
            };

            // Chạy câu lệnh INSERT và trả về số dòng bị ảnh hưởng
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
public static List<TransactionHistory> GetAllTransactions()
{
    string query = "SELECT transaction_id, user_id, order_id, transaction_date, amount, PaymentMethod_id, status, is_deleted FROM Transaction_History WHERE is_deleted = 0";
    
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
                PaymentMethodId = Convert.ToInt32(row["PaymentMethod_id"]),
                Status = row["status"].ToString(),
                IsDeleted = Convert.ToBoolean(row["is_deleted"])
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
            string query = "SELECT transaction_id, user_id, order_id, transaction_date, amount, PaymentMethod_id, status, is_deleted FROM TransactionHistory WHERE user_id = @user_id AND is_deleted = 0";
            
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
                        PaymentMethodId = Convert.ToInt32(row["PaymentMethod_id"]),
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
            string query = "SELECT transaction_id, user_id, order_id, transaction_date, amount, PaymentMethod_id, status, is_deleted FROM TransactionHistory WHERE transaction_id = @transaction_id AND is_deleted = 0";
            
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
                PaymentMethodId = Convert.ToInt32(row["PaymentMethod_id"]),
                Status = row["status"].ToString(),
                IsDeleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        // Cập nhật thông tin giao dịch
        public static int UpdateTransaction(TransactionHistory transaction)
        {
            string query = @"UPDATE TransactionHistory
                             SET amount = @amount,
                                 status = @status
                             WHERE transaction_id = @transaction_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = transaction.UserId },
                new MySqlParameter("@order_id", MySqlDbType.Int32) { Value = transaction.OrderId },
                new MySqlParameter("@transaction_date", MySqlDbType.DateTime) { Value = transaction.TransactionDate },
                new MySqlParameter("@amount", MySqlDbType.Decimal) { Value = transaction.Amount },
                new MySqlParameter("@PaymentMethod_id", MySqlDbType.Int32) { Value = transaction.PaymentMethodId },
                new MySqlParameter("@status", MySqlDbType.VarChar) { Value = transaction.Status }
            };

            // Chạy câu lệnh UPDATE và trả về số dòng bị ảnh hưởng
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Xóa giao dịch (đánh dấu bị xóa)
        public static int DeleteTransaction(int transaction_id)
        {
            string query = @"UPDATE TransactionHistory
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
