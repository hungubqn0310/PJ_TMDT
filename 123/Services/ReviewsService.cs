using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;
using _123.Models;

namespace _123.Services
{
    public static class ReviewService
    {
        // Thêm một review cho sản phẩm
        public static int CreateReview(Review review)
        {
            string query = @"INSERT INTO Reviews (product_id, user_id, rating, comment, review_date, is_deleted)
                             VALUES (@product_id, @user_id, @rating, @comment, @review_date, 0)";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = review.ProductId },
                new MySqlParameter("@user_id", MySqlDbType.Int32) { Value = review.UserId },
                new MySqlParameter("@rating", MySqlDbType.Int32) { Value = review.Rating },
                new MySqlParameter("@comment", MySqlDbType.Text) { Value = review.Comment },
                new MySqlParameter("@review_date", MySqlDbType.DateTime) { Value = review.ReviewDate }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
public static List<Review> GetAllReviews()
{
    string query = @"SELECT r.review_id, r.product_id, r.user_id, r.rating, r.comment, r.review_date, r.is_deleted , p.product_name, u.username
    FROM Reviews r
    LEFT JOIN Products  p ON p.product_id = r.product_id
    LEFT JOIN Users  u ON u.user_id = r.user_id
     WHERE r.is_deleted = 0";
    
    var reviews = new List<Review>();

    try
    {
        // Execute the query to fetch all non-deleted reviews
        DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

        foreach (DataRow row in dataTable.Rows)
        {
            reviews.Add(new Review
            {
                ReviewId = Convert.ToInt32(row["review_id"]),
                ProductId = row["product_id"].ToString(),
                UserId = Convert.ToInt32(row["user_id"]),
                Rating = Convert.ToInt32(row["rating"]),
                Comment = row["comment"].ToString(),
                ReviewDate = Convert.ToDateTime(row["review_date"]),
                IsDeleted = Convert.ToBoolean(row["is_deleted"]),
                Product = row["product_name"] == DBNull.Value ? null : new Product
                {
                    ProductId = (row["product_id"]).ToString(),
                    ProductName = row["product_name"].ToString()
                },
                User = row["username"] == DBNull.Value ? null : new User
                {
                    user_id = Convert.ToInt32(row["user_id"]),
                    username = row["username"].ToString()
                },
            });
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Lỗi khi lấy danh sách tất cả reviews: {ex.Message}");
        throw;
    }

    return reviews;
}

        // Lấy tất cả reviews cho một sản phẩm
        public static List<Review> GetReviewsByproduct_id(string product_id)
        {
            string query = "SELECT review_id, product_id, user_id, rating, comment, review_date, is_deleted FROM Reviews WHERE product_id = @product_id AND is_deleted = 0";
            
            var reviews = new List<Review>();

            try
            {
                var parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@product_id", MySqlDbType.VarChar) { Value = product_id }
                };

                DataTable dataTable = DatabaseHelper.ExecuteQuery(query, parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    reviews.Add(new Review
                    {
                       ReviewId = Convert.ToInt32(row["review_id"]),
                ProductId = row["product_id"].ToString(),
                UserId = Convert.ToInt32(row["user_id"]),
                Rating = Convert.ToInt32(row["rating"]),
                Comment = row["comment"].ToString(),
                ReviewDate = Convert.ToDateTime(row["review_date"]),
                IsDeleted = Convert.ToBoolean(row["is_deleted"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách reviews: {ex.Message}");
                throw;
            }

            return reviews;
        }

        // Lấy review theo ID
        public static Review GetReviewById(int reviewId)
        {
            string query = "SELECT review_id, product_id, user_id, rating, comment, review_date, is_deleted FROM Reviews WHERE review_id = @reviewId AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@reviewId", MySqlDbType.Int32) { Value = reviewId }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new Review
                {
                   ReviewId = Convert.ToInt32(row["review_id"]),
                ProductId = row["product_id"].ToString(),
                UserId = Convert.ToInt32(row["user_id"]),
                Rating = Convert.ToInt32(row["rating"]),
                Comment = row["comment"].ToString(),
                ReviewDate = Convert.ToDateTime(row["review_date"]),
                IsDeleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        // Cập nhật review
       public static int UpdateReview(Review review)
{
    string query = @"UPDATE Reviews
                     SET rating = @rating,
                         comment = @comment
                     WHERE review_id = @review_id AND is_deleted = 0";
    
    var parameters = new MySqlParameter[]
    {
        new MySqlParameter("@review_id", MySqlDbType.Int32) { Value = review.ReviewId },
        new MySqlParameter("@rating", MySqlDbType.Int32) { Value = review.Rating },
        new MySqlParameter("@comment", MySqlDbType.Text) { Value = review.Comment }
    };

    return DatabaseHelper.ExecuteNonQuery(query, parameters);
}


        // Xóa review (đánh dấu bị xóa)
        public static int DeleteReview(int reviewId)
        {
            string query = @"UPDATE Reviews
                             SET is_deleted = 1
                             WHERE review_id = @review_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@review_id", MySqlDbType.Int32) { Value = reviewId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
