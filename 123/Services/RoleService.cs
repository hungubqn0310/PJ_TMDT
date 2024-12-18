using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;
using _123.Models;

namespace _123.Services
{
    public static class RoleService
    {
        // Thêm mới một Role
        public static int CreateRole(Role role)
        {
            string query = @"INSERT INTO Role (code) VALUES (@code)";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@code", MySqlDbType.VarChar) { Value = role.Code }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Lấy danh sách tất cả các Role
        public static List<Role> GetRoles()
        {
            string query = "SELECT id, code FROM Role";
            var roles = new List<Role>();

            try
            {
                DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    roles.Add(new Role
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Code = row["code"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách roles: {ex.Message}");
                throw;
            }

            return roles;
        }

        // Lấy thông tin Role theo Id
        public static Role GetRoleById(int roleId)
        {
            string query = "SELECT id, code FROM Role WHERE id = @roleId";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@roleId", MySqlDbType.Int32) { Value = roleId }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new Role
                {
                    Id = Convert.ToInt32(row["id"]),
                    Code = row["code"].ToString()
                };
            }

            return null;
        }

        // Cập nhật thông tin Role
        public static int UpdateRole(Role role)
        {
            string query = @"UPDATE Role
                            SET code = @code
                            WHERE id = @id";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@id", MySqlDbType.Int32) { Value = role.Id },
                new MySqlParameter("@code", MySqlDbType.VarChar) { Value = role.Code }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Xóa Role theo Id
        public static int DeleteRole(int roleId)
        {
            string query = @"DELETE FROM Role WHERE id = @roleId";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@roleId", MySqlDbType.Int32) { Value = roleId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
