using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;

namespace _123.Services
{
    public static class ShoppingCartService
    {
        public static int AddToCart(Shopping_Cart cart)
        {
            // Câu lệnh SQL để thêm sản phẩm vào giỏ hàng
            string query = @"INSERT INTO Shopping_Cart (user_id, product_id, quantity, added_at, is_deleted)
                            VALUES (@user_id, @product_id, @quantity, @added_at, 0)";
            
            // Các tham số truyền vào câu lệnh SQL
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = cart.user_id },
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = cart.product_id },
                new MySqlParameter("@quantity", MySqlDbType.Int32) { Value = cart.quantity },
                new MySqlParameter("@added_at", MySqlDbType.DateTime) { Value = cart.added_at }
            };

            // Thực thi câu lệnh INSERT và trả về số dòng bị ảnh hưởng
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static List<Shopping_Cart> GetCartItems()
        {
            string query = @"SELECT cart_id, user_id, product_id, quantity, added_at, is_deleted 
                             FROM Shopping_Cart 
                             WHERE is_deleted = 0";
            

            var cartItems = new List<Shopping_Cart>();

            try
            {
                // Gọi hàm ExecuteQuery từ DatabaseHelper
                DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    cartItems.Add(new Shopping_Cart
                    {
                        cart_id = Convert.ToInt32(row["cart_id"]),
                        user_id = Convert.ToInt32(row["user_id"]),
                        product_id = row["product_id"].ToString(),
                        quantity = Convert.ToInt32(row["quantity"]),
                        added_at = Convert.ToDateTime(row["added_at"]),
                        is_deleted = Convert.ToBoolean(row["is_deleted"])
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

        public static Shopping_Cart GetCartItemById(int cartId)
        {
            string query = @"SELECT cart_id, user_id, product_id, quantity, added_at, is_deleted 
                             FROM Shopping_Cart 
                             WHERE cart_id = @cart_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@cart_id", MySqlDbType.Int32) { Value = cartId }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new Shopping_Cart
                {
                    cart_id = Convert.ToInt32(row["cart_id"]),
                    user_id = Convert.ToInt32(row["user_id"]),
                    product_id = row["product_id"].ToString(),
                    quantity = Convert.ToInt32(row["quantity"]),
                    added_at = Convert.ToDateTime(row["added_at"]),
                    is_deleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        public static int UpdateCartItem(Shopping_Cart cart)
        {
            string query = @"UPDATE Shopping_Cart
                            SET quantity = @quantity
                            WHERE cart_id = @cart_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@cart_id", MySqlDbType.Int32) { Value = cart.cart_id },
                new MySqlParameter("@quantity", MySqlDbType.Int32) { Value = cart.quantity }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public static int DeleteCartItem(int cartId)
        {
            string query = @"UPDATE Shopping_Cart
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
