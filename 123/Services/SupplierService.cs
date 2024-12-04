using System;
using System.Collections.Generic;
using System.Data;
using _123.Helpers;
using MySql.Data.MySqlClient;
using _123.Models;

namespace _123.Services
{
    public static class SupplierService
    {
        // Tạo nhà cung cấp mới
        public static int CreateSupplier(Supplier supplier)
        {
            string query = @"INSERT INTO Suppliers (supplier_name, contact_name, phone_number, address, email, is_deleted)
                            VALUES (@supplier_name, @contact_name, @phone_number, @address, @email, 0)";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@supplier_name", MySqlDbType.VarChar) { Value = supplier.SupplierName },
                new MySqlParameter("@contact_name", MySqlDbType.VarChar) { Value = supplier.ContactName },
                new MySqlParameter("@phone_number", MySqlDbType.VarChar) { Value = supplier.PhoneNumber },
                new MySqlParameter("@address", MySqlDbType.VarChar) { Value = supplier.Address },
                new MySqlParameter("@email", MySqlDbType.VarChar) { Value = supplier.Email }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Lấy danh sách nhà cung cấp
        public static List<Supplier> GetSuppliers()
        {
            string query = "SELECT supplier_id, supplier_name, contact_name, phone_number, address, email, is_deleted FROM Suppliers WHERE is_deleted = 0";
            
            var suppliers = new List<Supplier>();

            try
            {
                DataTable dataTable = DatabaseHelper.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    suppliers.Add(new Supplier
                    {
                        SupplierId = Convert.ToInt32(row["supplier_id"]),
                        SupplierName = row["supplier_name"].ToString(),
                        ContactName = row["contact_name"]?.ToString(),
                        PhoneNumber = row["phone_number"]?.ToString(),
                        Address = row["address"]?.ToString(),
                        Email = row["email"]?.ToString(),
                        IsDeleted = Convert.ToBoolean(row["is_deleted"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách nhà cung cấp: {ex.Message}");
                throw;
            }

            return suppliers;
        }

        // Lấy nhà cung cấp theo ID
        public static Supplier GetSupplierById(int supplierId)
        {
            string query = "SELECT supplier_id, supplier_name, contact_name, phone_number, address, email, is_deleted FROM Suppliers WHERE supplier_id = @supplierId AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@supplierId", MySqlDbType.Int32) { Value = supplierId }
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new Supplier
                {
                    SupplierId = Convert.ToInt32(row["supplier_id"]),
                    SupplierName = row["supplier_name"].ToString(),
                    ContactName = row["contact_name"]?.ToString(),
                    PhoneNumber = row["phone_number"]?.ToString(),
                    Address = row["address"]?.ToString(),
                    Email = row["email"]?.ToString(),
                    IsDeleted = Convert.ToBoolean(row["is_deleted"])
                };
            }

            return null;
        }

        // Cập nhật thông tin nhà cung cấp
        public static int UpdateSupplier(Supplier supplier)
        {
            string query = @"UPDATE Suppliers
                             SET supplier_name = @supplier_name,
                                 contact_name = @contact_name,
                                 phone_number = @phone_number,
                                 address = @address,
                                 email = @email
                             WHERE supplier_id = @supplier_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@supplier_id", MySqlDbType.Int32) { Value = supplier.SupplierId },
                new MySqlParameter("@supplier_name", MySqlDbType.VarChar) { Value = supplier.SupplierName },
                new MySqlParameter("@contact_name", MySqlDbType.VarChar) { Value = supplier.ContactName },
                new MySqlParameter("@phone_number", MySqlDbType.VarChar) { Value = supplier.PhoneNumber },
                new MySqlParameter("@address", MySqlDbType.VarChar) { Value = supplier.Address },
                new MySqlParameter("@email", MySqlDbType.VarChar) { Value = supplier.Email }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        // Xóa nhà cung cấp (đánh dấu là đã xóa)
        public static int DeleteSupplier(int supplierId)
        {
            string query = @"UPDATE Suppliers
                             SET is_deleted = 1
                             WHERE supplier_id = @supplier_id AND is_deleted = 0";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@supplier_id", MySqlDbType.Int32) { Value = supplierId }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }
    }
}
