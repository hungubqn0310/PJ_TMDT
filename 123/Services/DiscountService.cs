using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using _123.Models;
using MySql.Data.MySqlClient;

namespace _123.Services
{
    public static class DiscountService
    {
        // Thêm giảm giá mới
        public static int CreateDiscount(Discount discount)
        {
            string query = @"INSERT INTO Discounts (discount_name, discount_percent, start_date, end_date, is_deleted)
                            VALUES (@discount_name, @discount_percent, @start_date, @end_date, 0)";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@discount_name", MySqlDbType.VarChar) { Value = discount.DiscountName },
                new MySqlParameter("@discount_percent", MySqlDbType.Decimal) { Value = discount.DiscountPercent },
                new MySqlParameter("@start_date", MySqlDbType.DateTime) { Value = discount.StartDate },
                new MySqlParameter("@end_date", MySqlDbType.DateTime) { Value = discount.EndDate }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Lấy danh sách giảm giá
        public static List<Discount> GetDiscounts()
        {
            string query = "SELECT discount_id, discount_name, discount_percent, start_date, end_date, is_deleted FROM Discounts WHERE is_deleted = 0";

            var discounts = new List<Discount>();

            try
            {
                DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    discounts.Add(new Discount
                    {
                        DiscountId = Convert.ToInt32(row["discount_id"]),
                        DiscountName = row["discount_name"].ToString(),
                        DiscountPercent = Convert.ToDecimal(row["discount_percent"]),
                        StartDate = Convert.ToDateTime(row["start_date"]),
                        EndDate = Convert.ToDateTime(row["end_date"]),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách giảm giá: {ex.Message}");
                throw;
            }

            return discounts;
        }

        // Lấy giảm giá theo ID
        public static Discount GetDiscountById(int discountId)
        {
            string query = "SELECT discount_id, discount_name, discount_percent, start_date, end_date, is_deleted FROM Discounts WHERE discount_id = @discount_id AND is_deleted = 0";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@discount_id", MySqlDbType.Int32) { Value = discountId }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new Discount
                {
                    DiscountId = Convert.ToInt32(row["discount_id"]),
                    DiscountName = row["discount_name"].ToString(),
                    DiscountPercent = Convert.ToDecimal(row["discount_percent"]),
                    StartDate = Convert.ToDateTime(row["start_date"]),
                    EndDate = Convert.ToDateTime(row["end_date"]),
                    IsDeleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        // Cập nhật giảm giá
        public static int UpdateDiscount(Discount discount)
        {
            string query = @"UPDATE Discounts
                            SET discount_name = @discount_name,
                                discount_percent = @discount_percent,
                                start_date = @start_date,
                                end_date = @end_date
                            WHERE discount_id = @discount_id AND is_deleted = 0";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@discount_id", MySqlDbType.Int32) { Value = discount.DiscountId },
                new MySqlParameter("@discount_name", MySqlDbType.VarChar) { Value = discount.DiscountName },
                new MySqlParameter("@discount_percent", MySqlDbType.Decimal) { Value = discount.DiscountPercent },
                new MySqlParameter("@start_date", MySqlDbType.DateTime) { Value = discount.StartDate },
                new MySqlParameter("@end_date", MySqlDbType.DateTime) { Value = discount.EndDate }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Xóa giảm giá (đánh dấu là đã xóa)
        public static int DeleteDiscount(int discountId)
        {
            string query = @"UPDATE Discounts
                            SET is_deleted = 1
                            WHERE discount_id = @discount_id AND is_deleted = 0";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@discount_id", MySqlDbType.Int32) { Value = discountId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
