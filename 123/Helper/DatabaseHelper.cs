using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace _123.Helpers
{
    public static class DatabaseHelper
    {
        private static string _connectionString = "Server=localhost;Database=hanDK;User=root;Password=new_password;";

        /// <summary>
        /// Mở kết nối đến cơ sở dữ liệu.
        /// </summary>
        /// <returns>MySqlConnection</returns>
        public static MySqlConnection GetConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi mở kết nối: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Hàm thực thi câu lệnh SQL trả về DataTable.
        /// </summary>
        /// <param name="query">Câu lệnh SQL</param>
        /// <param name="parameters">Danh sách tham số (tuỳ chọn)</param>
        /// <returns>DataTable chứa kết quả truy vấn</returns>
        public static DataTable ExecuteQuery(string query, MySqlParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        /// <summary>
        /// Hàm thực thi câu lệnh SQL không trả về kết quả (INSERT, UPDATE, DELETE).
        /// </summary>
        /// <param name="query">Câu lệnh SQL</param>
        /// <param name="parameters">Danh sách tham số (tuỳ chọn)</param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        public static int ExecuteNonQuery(string query, MySqlParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    return command.ExecuteNonQuery();
                }
            }
        }
    
	public static object ExecuteScalar(string query, MySqlParameter[] parameters = null)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        // Thêm các tham số nếu có
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        // Thực thi câu lệnh và trả về giá trị đầu tiên
                        return command.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi thực hiện ExecuteScalar: {ex.Message}");
                    throw;
                }
            }
        }
	}
}