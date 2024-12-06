using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;
using _123.Models;


namespace _123.Services
{
    public static class ShoppingCartService
    {
        public static int AddToCart(ShoppingCart cart)
        {
            // Câu lệnh SQL để thêm sản phẩm vào giỏ hàng
            string query = @"INSERT INTO ShoppingCart (user_id, product_id, quantity, added_at, is_deleted)
                            VALUES (@user_id, @product_id, @quantity, @added_at, 0)";
            
            // Các tham số truyền vào câu lệnh SQL
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = cart.UserId },
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = cart.ProductId },
                new MySqlParameter("@quantity", MySqlDbType.Int32) { Value = cart.Quantity },
                new MySqlParameter("@added_at", MySqlDbType.DateTime) { Value = cart.AddedAt }
            };

            // Thực thi câu lệnh INSERT và trả về số dòng bị ảnh hưởng
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static List<ShoppingCart> GetCartItems()
        {
            string query = @"SELECT sc.cart_id, 
                            sc.user_id, 
                            sc.product_id, 
                            sc.quantity, 
                            sc.added_at, 
                            sc.is_deleted ,
                            p.product_name,
                            u.username 
                             FROM ShoppingCart sc
                             LEFT JOIN Users u ON u.user_id = sc.user_id
                            LEFT JOIN Products p ON p.product_id = sc.product_id
                             WHERE is_deleted = 0";
            

            var cartItems = new List<ShoppingCart>();

            try
            {
                // Gọi hàm ExecuteQuery từ DatabaseHelper
                DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    cartItems.Add(new ShoppingCart
                    {
                        CartId = Convert.ToInt32(row["cart_id"]),
                        UserId = Convert.ToInt32(row["user_id"]),
                        ProductId = row["product_id"].ToString(),
                        Quantity = Convert.ToInt32(row["quantity"]),
                        AddedAt = Convert.ToDateTime(row["added_at"]),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách giỏ hàng: {ex.Message}");
                throw;
            }

            return cartItems;
        }

        public static ShoppingCart GetCartItemById(int cartId)
        {
            string query = @"SELECT cart_id, user_id, product_id, quantity, added_at, is_deleted 
                             FROM ShoppingCart 
                             WHERE cart_id = @cart_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@cart_id", MySqlDbType.Int32) { Value = cartId }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new ShoppingCart
                {
                     CartId = Convert.ToInt32(row["cart_id"]),
                        UserId = Convert.ToInt32(row["user_id"]),
                        ProductId = row["product_id"].ToString(),
                        Quantity = Convert.ToInt32(row["quantity"]),
                        AddedAt = Convert.ToDateTime(row["added_at"]),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        public static int UpdateCartItem(ShoppingCart cart)
        {
            string query = @"UPDATE ShoppingCart
                            SET quantity = @quantity
                            WHERE cart_id = @cart_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = cart.UserId },
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = cart.ProductId },
                new MySqlParameter("@quantity", MySqlDbType.Int32) { Value = cart.Quantity },
                new MySqlParameter("@added_at", MySqlDbType.DateTime) { Value = cart.AddedAt }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static int DeleteCartItem(int cartId)
        {
            string query = @"UPDATE ShoppingCart
                            SET is_deleted = 1
                            WHERE cart_id = @cart_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@cart_id", MySqlDbType.Int32) { Value = cartId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
