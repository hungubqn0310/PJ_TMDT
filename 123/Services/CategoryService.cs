using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;
using _123.Models;

namespace _123.Services
{
    public static class CategoryService
    {
        // Thêm mới một Category
        public static int CreateCategory(Category category)
        {
            string query = @"INSERT INTO Categories (category_name, description, is_deleted)
                             VALUES (@category_name, @description, 0)";

            var parameters = new MySqlParameter[] 
            {
                new MySqlParameter("@category_name", MySqlDbType.VarChar) { Value = category.CategoryName },
                new MySqlParameter("@description", MySqlDbType.Text) { Value = category.Description }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Lấy danh sách các Category
        public static List<Category> GetCategories()
        {
            string query = "SELECT category_id, category_name, description, is_deleted FROM Categories WHERE is_deleted = 0";
            var categories = new List<Category>();

            try
            {
                DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    categories.Add(new Category
                    {
                        CategoryId = Convert.ToInt32(row["category_id"]),
                        CategoryName = row["category_name"].ToString(),
                        Description = row["description"].ToString(),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách categories: {ex.Message}");
                throw;
            }

            return categories;
        }

        // Lấy thông tin một Category theo ID
        public static Category GetCategoryById(int categoryId)
        {
            string query = "SELECT category_id, category_name, description, is_deleted FROM Categories WHERE category_id = @categoryId AND is_deleted = 0";

            var parameters = new MySqlParameter[] 
            {
                new MySqlParameter("@categoryId", MySqlDbType.Int32) { Value = categoryId }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new Category
                {
                    CategoryId = Convert.ToInt32(row["category_id"]),
                    CategoryName = row["category_name"].ToString(),
                    Description = row["description"].ToString(),
                    IsDeleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        // Cập nhật thông tin một Category
        public static int UpdateCategory(Category category)
        {
            string query = @"UPDATE Categories
                             SET category_name = @category_name,
                                 description = @description
                             WHERE category_id = @category_id AND is_deleted = 0";

            var parameters = new MySqlParameter[] 
            {
                new MySqlParameter("@category_id", MySqlDbType.Int32) { Value = category.CategoryId },
                new MySqlParameter("@category_name", MySqlDbType.VarChar) { Value = category.CategoryName },
                new MySqlParameter("@description", MySqlDbType.Text) { Value = category.Description }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Xóa Category (đánh dấu là xóa)
        public static int DeleteCategory(int categoryId)
        {
            string query = @"UPDATE Categories
                             SET is_deleted = 1
                             WHERE category_id = @category_id AND is_deleted = 0";

            var parameters = new MySqlParameter[] 
            {
                new MySqlParameter("@category_id", MySqlDbType.Int32) { Value = categoryId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
