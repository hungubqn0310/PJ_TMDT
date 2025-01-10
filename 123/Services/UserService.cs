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
            // Câu lệnh SQL để thêm người dùng mới, có thêm role_id
            string query = @"INSERT INTO Users (username, password, email, phone_number, address, role_id, is_deleted)
                            VALUES (@username, @password, @email, @phone_number, @address, @role_id, 0)";
            
            // Các tham số truyền vào câu lệnh SQL
            var parameters = new MySqlParameter[] 
            {
                new MySqlParameter("@username", MySqlDbType.VarChar) { Value = user.username },
                new MySqlParameter("@password", MySqlDbType.VarChar) { Value = user.password },
                new MySqlParameter("@email", MySqlDbType.VarChar) { Value = user.email },
                new MySqlParameter("@phone_number", MySqlDbType.VarChar) { Value = user.phone_number },
                new MySqlParameter("@address", MySqlDbType.VarChar) { Value = user.address },
                new MySqlParameter("@role_id", MySqlDbType.Int32) { Value = user.role_id } // Thêm role_id
            };

            // Thực thi câu lệnh INSERT và trả về số dòng bị ảnh hưởng
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static List<User> GetUsers()
        {
            string query = "SELECT user_id, username, password, email, phone_number, address, role_id, is_deleted FROM Users WHERE is_deleted = 0";
            
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
                        role_id = Convert.ToInt32(row["role_id"]), // Thêm role_id
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

        public static (int role1Count, int role3Count, decimal totalPrice) GetUserRoleCounts()
        {
            string query = "SELECT role_id, COUNT(*) AS role_count FROM Users WHERE is_deleted = 0 GROUP BY role_id";
            string queryTotalPrice = "SELECT SUM(total_amount) AS total_amount FROM Orders WHERE status = 'Completed'";

            int role1Count = 0;
            int role3Count = 0;
            decimal totalPrice = 0;
            try
            {
                // Gọi hàm ExecuteQuery từ DatabaseHelper
                DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    int roleId = Convert.ToInt32(row["role_id"]);
                    int count = Convert.ToInt32(row["role_count"]);

                    if (roleId == 1)
                    {
                        role1Count = count;
                    }
                    else if (roleId == 3)
                    {
                        role3Count = count;
                    }
                }
                DataTable orderDataTable = DatabaseHelper.ExecuteQuery(queryTotalPrice);
                if (orderDataTable.Rows.Count > 0)
                {
                    totalPrice = Convert.ToDecimal(orderDataTable.Rows[0]["total_amount"]);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy tổng số users theo role: {ex.Message}");
                throw;
            }

            return (role1Count, role3Count, totalPrice);
        }
   
        public static User GetUserById(int userId)
        {
            // Câu lệnh SQL để lấy thông tin user theo user_id
            string query = @"SELECT user_id, username, password, email, phone_number, address, role_id, is_deleted 
                             FROM Users 
                             WHERE user_id = @userId AND is_deleted = 0";
                    
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
                var row = result.Rows[0];
                return new User
                {
                    user_id = Convert.ToInt32(row["user_id"]),
                    username = row["username"].ToString(),
                    password = row["password"].ToString(),
                    email = row["email"].ToString(),
                    phone_number = row["phone_number"]?.ToString(),
                    address = row["address"]?.ToString(),
                    role_id = Convert.ToInt32(row["role_id"]), // Thêm role_id
                    is_deleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        public static int UpdateUser(User user)
        {
            // Câu lệnh SQL cập nhật thông tin user
            string query = @"UPDATE Users
                            SET username = @username,
                                email = @email,
                                phone_number = @phone_number,
                                address = @address,
                                role_id = @role_id
                            WHERE user_id = @user_id AND is_deleted = 0";
                    
            // Các tham số truyền vào câu lệnh SQL
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = user.user_id },
                new MySqlParameter("@username", MySqlDbType.VarChar) { Value = user.username },
                new MySqlParameter("@email", MySqlDbType.VarChar) { Value = user.email },
                new MySqlParameter("@phone_number", MySqlDbType.VarChar) { Value = user.phone_number },
                new MySqlParameter("@address", MySqlDbType.VarChar) { Value = user.address },
                new MySqlParameter("@role_id", MySqlDbType.Int32) { Value = user.role_id } // Thêm role_id
            };

            // Thực thi câu lệnh UPDATE và trả về số dòng bị ảnh hưởng
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static int DeleteUser(int userId)
        {
            string query = @"UPDATE Users
                            SET is_deleted = 1
                            WHERE user_id = @user_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[] 
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = userId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        } 
        public static int ChangePassword(int userId, string newPassword)
        {
            // Câu lệnh SQL để đổi mật khẩu
            string query = @"UPDATE Users
                            SET password = @newPassword
                            WHERE user_id = @userId AND is_deleted = 0";

            // Các tham số truyền vào câu lệnh SQL
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@userId", MySqlDbType.Int32) { Value = userId },
                new MySqlParameter("@newPassword", MySqlDbType.VarChar) { Value = newPassword }
            };

            // Thực thi câu lệnh UPDATE và trả về số dòng bị ảnh hưởng
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
