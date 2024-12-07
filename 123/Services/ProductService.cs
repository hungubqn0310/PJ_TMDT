using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using _123.Models;
using MySql.Data.MySqlClient;

namespace _123.Services
{
    public static class ProductService
    {
        public static int CreateProduct(Product product)
        {
            string query = @"INSERT INTO Products (product_id, product_name, description, price, material_id, category_id, image_url, is_deleted)
                            VALUES (@product_id, @product_name, @description, @price, @material_id, @category_id, @image_url, 0)";
            Console.WriteLine(product.ProductId);
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = product.ProductId },
                new MySqlParameter("@product_name", MySqlDbType.VarChar) { Value = product.ProductName },
                new MySqlParameter("@description", MySqlDbType.Text) { Value = product.Description ?? (object)DBNull.Value },
                new MySqlParameter("@price", MySqlDbType.Decimal) { Value = product.Price },
                new MySqlParameter("@material_id", MySqlDbType.Int32) { Value = product.MaterialId ?? (object)DBNull.Value },
                new MySqlParameter("@category_id", MySqlDbType.Int32) { Value = product.CategoryId ?? (object)DBNull.Value },
                new MySqlParameter("@image_url", MySqlDbType.VarChar) { Value = product.ImageUrl ?? (object)DBNull.Value }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static List<Product> GetProducts()
{
            string query = @"
                SELECT 
                    p.product_id, 
                    p.product_name, 
                    p.description, 
                    p.price, 
                    p.image_url, 
                    p.is_deleted, 
                    p.material_id, 
                    m.material_name, 
                    p.category_id, 
                    c.category_name
                FROM Products p
                LEFT JOIN Categories c ON p.category_id = c.category_id
                LEFT JOIN Materials m ON p.material_id = m.material_id
                WHERE p.is_deleted = 0";

    var products = new List<Product>();

    try
    {
        DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

        foreach (DataRow row in dataTable.Rows)
        {
            products.Add(new Product
            {
                ProductId = row["product_id"].ToString(),
                ProductName = row["product_name"].ToString(),
                Description = row["description"]?.ToString(),
                Price = Convert.ToDecimal(row["price"]),
                MaterialId = row["material_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["material_id"]),
                CategoryId = row["category_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["category_id"]),
                ImageUrl = row["image_url"]?.ToString(),
                IsDeleted = Convert.ToBoolean(row["is_deleted"]),
                Material = row["material_name"] == DBNull.Value ? null : new Material
                {
                    MaterialId = Convert.ToInt32(row["material_id"]),
                    MaterialName = row["material_name"].ToString()
                },
                Category = row["category_name"] == DBNull.Value ? null : new Category
                {
                    CategoryId = Convert.ToInt32(row["category_id"]),
                    CategoryName = row["category_name"].ToString()
                }
            });
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error retrieving products: {ex.Message}");
        throw;
    }

    return products;
}

        public static Product GetProductById(string productId)
        {
            string query = "SELECT * FROM Products WHERE product_id = @product_id AND is_deleted = 0";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = productId }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new Product
                {
                    ProductId = row["product_id"].ToString(),
                    ProductName = row["product_name"].ToString(),
                    Description = row["description"]?.ToString(),
                    Price = Convert.ToDecimal(row["price"]),
                    MaterialId = row["material_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["material_id"]),
                    CategoryId = row["category_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["category_id"]),
                    ImageUrl = row["image_url"]?.ToString(),
                    IsDeleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        public static int UpdateProduct(Product product)
        {
            string query = @"UPDATE Products
                            SET product_name = @product_name,
                                description = @description,
                                price = @price,
                                material_id = @material_id,
                                category_id = @category_id,
                                image_url = @image_url
                            WHERE product_id = @product_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = product.ProductId },
                new MySqlParameter("@product_name", MySqlDbType.VarChar) { Value = product.ProductName },
                new MySqlParameter("@description", MySqlDbType.Text) { Value = product.Description ?? (object)DBNull.Value },
                new MySqlParameter("@price", MySqlDbType.Decimal) { Value = product.Price },
                new MySqlParameter("@material_id", MySqlDbType.Int32) { Value = product.MaterialId ?? (object)DBNull.Value },
                new MySqlParameter("@category_id", MySqlDbType.Int32) { Value = product.CategoryId ?? (object)DBNull.Value },
                new MySqlParameter("@image_url", MySqlDbType.VarChar) { Value = product.ImageUrl ?? (object)DBNull.Value }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static int DeleteProduct(string productId)
        {
            string query = @"UPDATE Products
                            SET is_deleted = 1
                            WHERE product_id = @product_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = productId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
