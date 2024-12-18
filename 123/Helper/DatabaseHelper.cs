using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace _123.Helpers
{
    public static class DatabaseHelper
    {
        // Chuỗi kết nối đến cơ sở dữ liệu
        private static string _connectionString = "Server=localhost;Database=handk;User=root;Password=123456;";

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

        /// <summary>
        /// Hàm thực thi câu lệnh SQL trả về giá trị đầu tiên.
        /// </summary>
        /// <param name="query">Câu lệnh SQL</param>
        /// <param name="parameters">Danh sách tham số (tuỳ chọn)</param>
        /// <returns>Giá trị đầu tiên của kết quả</returns>
        public static object ExecuteScalar(string query, MySqlParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    return command.ExecuteScalar();
                }
            }
        }
    }
}