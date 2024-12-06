using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;
using _123.Models;

namespace _123.Services
{
    public static class UserService
    {
        public static int CreateUser(User user)
        {
            // Câu lệnh SQL để thêm người dùng mới
            string query = @"INSERT INTO Users (username, password,email, phone_number, address, is_deleted)
                            VALUES (@username, @password, @email, @phone_number, @address, 0)";
            
            // Các tham số truyền vào câu lệnh SQL
            var parameters = new MySqlParameter[] 
            {
                new MySqlParameter("@username", MySqlDbType.VarChar) { Value = user.username },
                new MySqlParameter("@password", MySqlDbType.VarChar) { Value = user.password },
                new MySqlParameter("@email", MySqlDbType.VarChar) { Value = user.email },
                new MySqlParameter("@phone_number", MySqlDbType.VarChar) { Value = user.phone_number },
                new MySqlParameter("@address", MySqlDbType.VarChar) { Value = user.address }
            };

            // Thực thi câu lệnh INSERT và trả về số dòng bị ảnh hưởng
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
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
   
        public static User GetUserById(int userId)
                {
                    // Câu lệnh SQL để lấy thông tin user theo user_id
                    string query = "SELECT user_id, username, password, email, phone_number, address, is_deleted FROM Users WHERE user_id = @userId AND is_deleted = 0";
                    
                    // Tham số truyền vào câu lệnh SQL
                    var parameters = new MySqlParameter[]
                    {
                        new MySqlParameter("@userId", MySqlDbType.Int32) { Value = userId }
                    };

                    // Thực thi câu lệnh SQL và nhận về DataTable
                    DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

                    // Kiểm tra nếu có kết quả và ánh xạ vào đối tượng User
                    if (result.Rows.Count > 0)
                    {
                        var row = result.Rows[0]; // Chỉ lấy dòng đầu tiên (vì user_id là duy nhất)
                        return new User
                        {
                            user_id = Convert.ToInt32(row["user_id"]),
                            username = row["username"].ToString(),
                            password = row["password"].ToString(),
                            email = row["email"].ToString(),
                            phone_number = row["phone_number"]?.ToString(),
                            address = row["address"]?.ToString(),
                            is_deleted = Convert.ToBoolean(row["is_deleted"])
                        };
                    }

                    // Nếu không tìm thấy, trả về null
                    return null;
                }

        public static int UpdateUser(User user)
                {
                    // Câu lệnh SQL cập nhật thông tin user
                    string query = @"UPDATE Users
                                    SET username = @username,
                                        email = @email,
                                        phone_number = @phone_number,
                                        address = @address
                                    WHERE user_id = @user_id AND is_deleted = 0";
                    
                    // Các tham số truyền vào câu lệnh SQL
                    var parameters = new MySqlParameter[]
                    {
                        new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = user.user_id },
                        new MySqlParameter("@username", MySqlDbType.VarChar) { Value = user.username },
                        new MySqlParameter("@email", MySqlDbType.VarChar) { Value = user.email },
                        new MySqlParameter("@phone_number", MySqlDbType.VarChar) { Value = user.phone_number },
                        new MySqlParameter("@address", MySqlDbType.VarChar) { Value = user.address }
                    };

                    // Thực thi câu lệnh UPDATE và trả về số dòng bị ảnh hưởng
                    return DatabaseHelper.ExecuteNonQuery(query, parameters);
                }

        public static int DeleteUser(int userId)
        {
            // Câu lệnh SQL để đánh dấu người dùng là bị xóa tạm thời
            string query = @"UPDATE Users
                            SET is_deleted = 1
                            WHERE user_id = @user_id AND is_deleted = 0";
            
            // Tham số truyền vào câu lệnh SQL
            var parameters = new MySqlParameter[] 
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = userId }
            };

            // Thực thi câu lệnh UPDATE và trả về số dòng bị ảnh hưởng
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        } 
    }
}
