using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;

namespace _123.Services
{
    public static class UserService
    {
        public static List<User> GetUsers()
        {
            string query = "SELECT user_id, username, password, email, phone_number, address, is_deleted FROM Users WHERE is_deleted = 0";
            
            var users = new List<User>();

            try
            {
                // Gọi hàm ExecuteQuery từ DatabaseHelper
                DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    users.Add(new User
                    {
                        user_id = Convert.ToInt32(row["user_id"]),
                        username = row["username"].ToString(),
                        password = row["password"].ToString(),
                        email = row["email"].ToString(),
                        phone_number = row["phone_number"]?.ToString(),
                        address = row["address"]?.ToString(),
                        is_deleted = Convert.ToBoolean(row["is_deleted"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách users: {ex.Message}");
                throw;
            }

            return users;
        }
    }

    
}
