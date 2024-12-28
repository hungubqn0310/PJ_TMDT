using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using _123.Models;
using MySql.Data.MySqlClient;

namespace _123.Services
{
    public static class AuthService
    {
        public static User Login(string username, string password)
{
    // Câu lệnh SQL để kiểm tra thông tin đăng nhập
    string query = @"SELECT user_id, username, password, email, phone_number, address, role_id,is_deleted
                     FROM Users
                     WHERE username = @username AND password = @password AND is_deleted = 0";
    
    // Tham số truyền vào câu lệnh SQL
    var parameters = new MySqlParameter[]
    {
        new MySqlParameter("@username", MySqlDbType.VarChar) { Value = username },
        new MySqlParameter("@password", MySqlDbType.VarChar) { Value = password }
    };

    try
    {
        // Thực thi câu lệnh SQL và nhận về DataTable
        DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

        if (result.Rows.Count > 0)
        {
            var row = result.Rows[0]; // Lấy dòng đầu tiên
            return new User
            {
                user_id = Convert.ToInt32(row["user_id"]),
                username = row["username"].ToString(),
                password = row["password"].ToString(), // Trả về mật khẩu (nếu cần)
                email = row["email"].ToString(),
                role_id = Convert.ToInt32(row["role_id"]),
                phone_number = row["phone_number"]?.ToString(),
                address = row["address"]?.ToString(),
                is_deleted = Convert.ToBoolean(row["is_deleted"])
            };
        }

        // Nếu không tìm thấy, trả về null
        return null;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Lỗi khi thực hiện đăng nhập: {ex.Message}");
        throw;
    }
}

        public static string Signup(Users user)
{
    // Kiểm tra xem username hoặc email đã tồn tại chưa
    string checkQuery = @"SELECT COUNT(*) 
                          FROM Users 
                          WHERE (username = @username OR email = @email) AND is_deleted = 0";

    var checkParams = new MySqlParameter[]
    {
        new MySqlParameter("@username", MySqlDbType.VarChar) { Value = user.Username },
        new MySqlParameter("@email", MySqlDbType.VarChar) { Value = user.Password }
    };

    try
    {
        // Kiểm tra xem có người dùng trùng username hoặc email
        object result = DatabaseHelper.ExecuteScalar(checkQuery, checkParams);
        int count = Convert.ToInt32(result);

        if (count > 0)
        {
            return "Username hoặc email đã được sử dụng.";
        }

        // Nếu không tồn tại, tiến hành thêm người dùng mới
        string insertQuery = @"INSERT INTO Users (username, password, email, phone_number, address, is_deleted)
                               VALUES (@username, @password, @email, @phone_number, @address, 0)";

        var insertParams = new MySqlParameter[]
        {
            new MySqlParameter("@username", MySqlDbType.VarChar) { Value = user.Username },
            new MySqlParameter("@password", MySqlDbType.VarChar) { Value = user.Password }, // Plain-text, nên hash trong thực tế
            new MySqlParameter("@email", MySqlDbType.VarChar) { Value = user.Email },
            new MySqlParameter("@phone_number", MySqlDbType.VarChar) { Value = user.PhoneNumber },
            new MySqlParameter("@address", MySqlDbType.VarChar) { Value = user.Address }
        };

        int rowsAffected = DatabaseHelper.ExecuteNonQuery(insertQuery, insertParams);

        if (rowsAffected > 0)
        {
            return "Đăng ký thành công.";
        }
        else
        {
            return "Có lỗi xảy ra khi đăng ký. Vui lòng thử lại.";
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Lỗi khi thực hiện đăng ký: {ex.Message}");
        throw;
    }
}

}
}