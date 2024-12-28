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
            string query = @"INSERT INTO Shopping_Cart (user_id, product_id, quantity, added_at, is_deleted)
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
                             FROM Shopping_Cart sc
                             LEFT JOIN Users u ON u.user_id = sc.user_id
                            LEFT JOIN Products p ON p.product_id = sc.product_id
                             WHERE sc.is_deleted = 0";
            

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
                        IsDeleted = Convert.ToBoolean(row["is_deleted"]),
                        User = row["username"] == DBNull.Value ? null : new User
                {
                    user_id = Convert.ToInt32(row["user_id"]),
                    username = row["username"].ToString()
                },
                Product = row["product_name"] == DBNull.Value ? null : new Product
                {
                    ProductId = (row["product_id"]).ToString(),
                    ProductName = row["product_name"].ToString()
                }
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
    string query = @"
        UPDATE Shopping_Cart
        SET 
            quantity = @quantity,
            added_at = @added_at
        WHERE 
            cart_id = @cart_id 
            AND user_id = @user_id 
            AND product_id = @product_id
            AND is_deleted = 0";

    var parameters = new MySqlParameter[]
    {
        new MySqlParameter("@cart_id", MySqlDbType.Int32) { Value = cart.CartId },
        new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = cart.UserId },
        new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = cart.ProductId },
        new MySqlParameter("@quantity", MySqlDbType.Int32) { Value = cart.Quantity },
        new MySqlParameter("@added_at", MySqlDbType.DateTime) { Value = cart.AddedAt }
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

        public static int UpdateCartToDeleteByUserId(int userId)
{
    string query = @"UPDATE Shopping_Cart
                     SET is_deleted = 1
                     WHERE user_id = @user_id AND is_deleted = 0";
    
    var parameters = new MySqlParameter[]
    {
        new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = userId }
    };

    return DatabaseHelper.ExecuteNonQuery(query, parameters);
}
    
        public static List<dynamic> GetCartItemsByUserId(int userId)
        {
            string query = @"SELECT sc.cart_id, sc.user_id, sc.product_id, sc.quantity, sc.added_at, sc.is_deleted, 
                                    p.product_name, p.price, dis.discount_percent, p.image_url
                            FROM Shopping_Cart sc
                            LEFT JOIN Products p ON sc.product_id = p.product_id
                            LEFT JOIN Product_Discount pdis ON pdis.product_id = p.product_id
                            LEFT JOIN Discounts  dis ON dis.discount_id = pdis.discount_id 
                            WHERE sc.user_id = @user_id AND sc.is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = userId }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            List<dynamic> cartItems = new List<dynamic>();

            foreach (DataRow row in result.Rows)
            {
                cartItems.Add(new
                {
                    CartId = Convert.ToInt32(row["cart_id"]),
                    UserId = Convert.ToInt32(row["user_id"]),
                    ProductId = row["product_id"].ToString(),
                    ProductName = row["product_name"].ToString(),
                    DiscountPercent = row["discount_percent"].ToString(),
                    ImageUrl = row["image_url"].ToString(),
                    Price = Convert.ToDecimal(row["price"]),
                    Quantity = Convert.ToInt32(row["quantity"]),
                    AddedAt = Convert.ToDateTime(row["added_at"]),
                    IsDeleted = Convert.ToBoolean(row["is_deleted"])
                });
            }

            return cartItems;
        }

        public static int AddOrUpdateToCart(ShoppingCart cart)
{
    // Câu lệnh SQL để kiểm tra xem sản phẩm đã có trong giỏ hàng của người dùng chưa
    string checkQuery = @"SELECT COUNT(*) FROM Shopping_Cart WHERE user_id = @user_id AND product_id = @product_id AND is_deleted = 0";
    
    // Các tham số truyền vào câu lệnh kiểm tra
    var checkParameters = new MySqlParameter[]
    {
        new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = cart.UserId },
        new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = cart.ProductId }
    };

    // Kiểm tra số lượng sản phẩm có trong giỏ
    int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkQuery, checkParameters));

    if (count > 0)
    {
        // Nếu đã có sản phẩm trong giỏ hàng, thực hiện cập nhật số lượng
        string updateQuery = @"UPDATE Shopping_Cart SET quantity = quantity + @quantity, added_at = @added_at
                               WHERE user_id = @user_id AND product_id = @product_id AND is_deleted = 0";
        
        var updateParameters = new MySqlParameter[]
        {
            new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = cart.UserId },
            new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = cart.ProductId },
            new MySqlParameter("@quantity", MySqlDbType.Int32) { Value = cart.Quantity },
            new MySqlParameter("@added_at", MySqlDbType.DateTime) { Value = cart.AddedAt }
        };

        // Thực thi câu lệnh UPDATE và trả về số dòng bị ảnh hưởng
        return DatabaseHelper.ExecuteNonQuery(updateQuery, updateParameters);
    }
    else
    {
        // Nếu chưa có sản phẩm trong giỏ hàng, thực hiện thêm mới
        string insertQuery = @"INSERT INTO Shopping_Cart (user_id, product_id, quantity, added_at, is_deleted)
                               VALUES (@user_id, @product_id, @quantity, @added_at, 0)";
        
        var insertParameters = new MySqlParameter[]
        {
            new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = cart.UserId },
            new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = cart.ProductId },
            new MySqlParameter("@quantity", MySqlDbType.Int32) { Value = cart.Quantity },
            new MySqlParameter("@added_at", MySqlDbType.DateTime) { Value = cart.AddedAt }
        };

        // Thực thi câu lệnh INSERT và trả về số dòng bị ảnh hưởng
        return DatabaseHelper.ExecuteNonQuery(insertQuery, insertParameters);
    }
}

public static int AddOrUpdateToManyCart(ShoppingCart[] carts)
{
    int affectedRows = 0;

    foreach (var cart in carts)
    {
        // Câu lệnh SQL để kiểm tra xem sản phẩm đã có trong giỏ hàng của người dùng chưa
        string checkQuery = @"SELECT COUNT(*) FROM Shopping_Cart WHERE user_id = @user_id AND product_id = @product_id AND is_deleted = 0";
        
        // Các tham số truyền vào câu lệnh kiểm tra
        var checkParameters = new MySqlParameter[]
        {
            new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = cart.UserId },
            new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = cart.ProductId }
        };

        // Kiểm tra số lượng sản phẩm có trong giỏ
        int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkQuery, checkParameters));

        if (count > 0)
        {
            // Nếu đã có sản phẩm trong giỏ hàng, thực hiện cập nhật số lượng
            string updateQuery = @"UPDATE Shopping_Cart SET quantity = quantity + @quantity, added_at = @added_at
                                   WHERE user_id = @user_id AND product_id = @product_id AND is_deleted = 0";
            
            var updateParameters = new MySqlParameter[]
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = cart.UserId },
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = cart.ProductId },
                new MySqlParameter("@quantity", MySqlDbType.Int32) { Value = cart.Quantity },
                new MySqlParameter("@added_at", MySqlDbType.DateTime) { Value = cart.AddedAt }
            };

            // Thực thi câu lệnh UPDATE và tăng số dòng bị ảnh hưởng
            affectedRows += DatabaseHelper.ExecuteNonQuery(updateQuery, updateParameters);
        }
        else
        {
            // Nếu chưa có sản phẩm trong giỏ hàng, thực hiện thêm mới
            string insertQuery = @"INSERT INTO Shopping_Cart (user_id, product_id, quantity, added_at, is_deleted)
                                   VALUES (@user_id, @product_id, @quantity, @added_at, 0)";
            
            var insertParameters = new MySqlParameter[]
            {
                new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = cart.UserId },
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = cart.ProductId },
                new MySqlParameter("@quantity", MySqlDbType.Int32) { Value = cart.Quantity },
                new MySqlParameter("@added_at", MySqlDbType.DateTime) { Value = cart.AddedAt }
            };

            // Thực thi câu lệnh INSERT và tăng số dòng bị ảnh hưởng
            affectedRows += DatabaseHelper.ExecuteNonQuery(insertQuery, insertParameters);
        }
    }

    // Trả về tổng số dòng bị ảnh hưởng từ các thao tác INSERT/UPDATE
    return affectedRows;
}



    }
}
