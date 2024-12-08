using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;

namespace _123.Services
{
    public static class MaterialService
    {
        // Thêm mới Material
        public static int CreateMaterial(Material material)
        {
            
            string query = @"INSERT INTO Materials (material_name, description, is_deleted)
                            VALUES (@material_name, @description, 0)";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@material_name", MySqlDbType.VarChar) { Value = material.MaterialName },
                new MySqlParameter("@description", MySqlDbType.Text) { Value = material.Description ?? (object)DBNull.Value }
            };

            try
            {
                return DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi thực thi truy vấn
                Console.WriteLine($"Error occurred while adding material: {ex.Message}");
                throw;
            }
        }

        // Lấy danh sách Material
        public static List<Material> GetMaterials()
        {
            string query = "SELECT material_id, material_name, description, is_deleted FROM Materials WHERE is_deleted = 0";
            
            var materials = new List<Material>();

            try
            {
                DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    materials.Add(new Material
                    {
                        MaterialId = Convert.ToInt32(row["material_id"]),
                        MaterialName = row["material_name"].ToString(),
                        Description = row["description"]?.ToString(),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách materials: {ex.Message}");
                throw;
            }

            return materials;
        }

        // Lấy Material theo ID
        public static Material GetMaterialById(int materialId)
        {
            string query = "SELECT material_id, material_name, description, is_deleted FROM Materials WHERE material_id = @material_id AND is_deleted = 0";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@material_id", MySqlDbType.Int32) { Value = materialId }
            };

            try
            {
                DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

                if (result.Rows.Count > 0)
                {
                    var row = result.Rows[0];
                    return new Material
                    {
                        MaterialId = Convert.ToInt32(row["material_id"]),
                        MaterialName = row["material_name"].ToString(),
                        Description = row["description"]?.ToString(),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"])
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while fetching material by ID: {ex.Message}");
                throw;
            }

            return null;
        }

        // Cập nhật Material
        public static int UpdateMaterial(Material material)
        {
            if (string.IsNullOrEmpty(material.MaterialName))
            {
                throw new ArgumentException("Material name cannot be null or empty.");
            }

            string query = @"UPDATE Materials
                            SET material_name = @material_name,
                                description = @description
                            WHERE material_id = @material_id AND is_deleted = 0";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@material_id", MySqlDbType.Int32) { Value = material.MaterialId },
                new MySqlParameter("@material_name", MySqlDbType.VarChar) { Value = material.MaterialName },
                new MySqlParameter("@description", MySqlDbType.Text) { Value = material.Description ?? (object)DBNull.Value }
            };

            try
            {
                return DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi thực thi truy vấn
                Console.WriteLine($"Error occurred while updating material: {ex.Message}");
                throw;
            }
        }

        // Xóa Material (soft delete)
        public static int DeleteMaterial(int materialId)
        {
            string query = @"UPDATE Materials
                            SET is_deleted = 1
                            WHERE material_id = @material_id AND is_deleted = 0";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@material_id", MySqlDbType.Int32) { Value = materialId }
            };

            try
            {
                return DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi thực thi truy vấn
                Console.WriteLine($"Error occurred while deleting material: {ex.Message}");
                throw;
            }
        }
    }
}
