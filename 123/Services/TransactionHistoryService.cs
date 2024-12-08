using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;

namespace _123.Services
{
    public static class Transaction_HistoryService
    {
        // Thêm mới giao dịch vào Transaction_History
        public static int CreateTransaction(Transaction_History transaction)
        {
            string query = @"INSERT INTO Transaction_History (user_id, order_id, transaction_date, amount, payment_method_id, status, is_deleted)
                             VALUES (@user_id, @order_id, @transaction_date, @amount, @payment_method_id, @status, 0)";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = transaction.user_id },
                new MySqlParameter("@order_id", MySqlDbType.Int32) { Value = transaction.order_id },
                new MySqlParameter("@transaction_date", MySqlDbType.DateTime) { Value = transaction.transaction_date },
                new MySqlParameter("@amount", MySqlDbType.Decimal) { Value = transaction.amount },
                new MySqlParameter("@payment_method_id", MySqlDbType.Int32) { Value = transaction.payment_method_id },
                new MySqlParameter("@status", MySqlDbType.VarChar) { Value = transaction.status }
            };

            // Chạy câu lệnh INSERT và trả về số dòng bị ảnh hưởng
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
public static List<Transaction_History> GetAllTransactions()
{
    string query = "SELECT transaction_id, user_id, order_id, transaction_date, amount, payment_method_id, status, is_deleted FROM Transaction_History WHERE is_deleted = 0";
    
    var transactions = new List<Transaction_History>();

    try
    {
        // No need for parameters here since we're not filtering by user
        DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

        foreach (DataRow row in dataTable.Rows)
        {
            transactions.Add(new Transaction_History
            {
                transaction_id = Convert.ToInt32(row["transaction_id"]),
                user_id = Convert.ToInt32(row["user_id"]),
                order_id = Convert.ToInt32(row["order_id"]),
                transaction_date = Convert.ToDateTime(row["transaction_date"]),
                amount = Convert.ToDecimal(row["amount"]),
                payment_method_id = Convert.ToInt32(row["payment_method_id"]),
                status = row["status"].ToString(),
                is_deleted = Convert.ToBoolean(row["is_deleted"])
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
        public static List<Transaction_History> GetTransactionsByuser_id(int user_id)
        {
            string query = "SELECT transaction_id, user_id, order_id, transaction_date, amount, payment_method_id, status, is_deleted FROM Transaction_History WHERE user_id = @user_id AND is_deleted = 0";
            
            var transactions = new List<Transaction_History>();

            try
            {
                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = user_id }
                };

                DataTable dataTable = DatabaseHelper.ExecuteQuery(query, parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    transactions.Add(new Transaction_History
                    {
                        transaction_id = Convert.ToInt32(row["transaction_id"]),
                        user_id = Convert.ToInt32(row["user_id"]),
                        order_id = Convert.ToInt32(row["order_id"]),
                        transaction_date = Convert.ToDateTime(row["transaction_date"]),
                        amount = Convert.ToDecimal(row["amount"]),
                        payment_method_id = Convert.ToInt32(row["payment_method_id"]),
                        status = row["status"].ToString(),
                        is_deleted = Convert.ToBoolean(row["is_deleted"])
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
        public static Transaction_History GetTransactionById(int transaction_id)
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
                return new Transaction_History
                {
                    transaction_id = Convert.ToInt32(row["transaction_id"]),
                    user_id = Convert.ToInt32(row["user_id"]),
                    order_id = Convert.ToInt32(row["order_id"]),
                    transaction_date = Convert.ToDateTime(row["transaction_date"]),
                    amount = Convert.ToDecimal(row["amount"]),
                    payment_method_id = Convert.ToInt32(row["payment_method_id"]),
                    status = row["status"].ToString(),
                    is_deleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        // Cập nhật thông tin giao dịch
        public static int UpdateTransaction(Transaction_History transaction)
        {
            string query = @"UPDATE Transaction_History
                             SET amount = @amount,
                                 status = @status
                             WHERE transaction_id = @transaction_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@transaction_id", MySqlDbType.Int32) { Value = transaction.transaction_id },
                new MySqlParameter("@amount", MySqlDbType.Decimal) { Value = transaction.amount },
                new MySqlParameter("@status", MySqlDbType.VarChar) { Value = transaction.status }
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
