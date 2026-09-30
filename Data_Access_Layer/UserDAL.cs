using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Aarambha.Models;

namespace Aarambha.Data_Access_Layer
{
    public class UserDAL
    {
        public List<User> GetAll()
        {
            var list = new List<User>();
            const string sql = @"SELECT UserID, FullName, Username, Email, PasswordHash, PasswordSalt, RoleID, PhoneNumber, IsActive, CreatedAt, LastLoginAt FROM Users";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(ReadUser(reader));
                    }
                }
            }
            return list;
        }

        public List<User> GetStudents()
        {
            var list = new List<User>();
            const string sql = @"SELECT UserID, FullName, Username, Email, PasswordHash, PasswordSalt, RoleID, PhoneNumber, IsActive, CreatedAt, LastLoginAt FROM Users WHERE RoleID = @RoleID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@RoleID", 2);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(ReadUser(reader));
                    }
                }
            }
            return list;
        }

        public User GetByUsername(string username)
        {
            const string sql = @"SELECT UserID, FullName, Username, Email, PasswordHash, PasswordSalt, RoleID, PhoneNumber, IsActive, CreatedAt, LastLoginAt FROM Users WHERE Username = @Username";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ReadUser(reader);
                    }
                }
            }
            return null;
        }

        public User GetById(int userId)
        {
            const string sql = @"SELECT UserID, FullName, Username, Email, PasswordHash, PasswordSalt, RoleID, PhoneNumber, IsActive, CreatedAt, LastLoginAt FROM Users WHERE UserID = @UserID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ReadUser(reader);
                    }
                }
            }
            return null;
        }

        public int Insert(User model)
        {
            const string sql = @"INSERT INTO Users (FullName, Username, Email, PasswordHash, PasswordSalt, RoleID, PhoneNumber, IsActive, CreatedAt)
                               OUTPUT INSERTED.UserID VALUES (@FullName, @Username, @Email, @PasswordHash, @PasswordSalt, @RoleID, @PhoneNumber, @IsActive, @CreatedAt)";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@FullName", (object)model.FullName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Username", model.Username);
                cmd.Parameters.AddWithValue("@Email", (object)model.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PasswordHash", model.PasswordHash);
                cmd.Parameters.AddWithValue("@PasswordSalt", model.PasswordSalt);
                cmd.Parameters.AddWithValue("@RoleID", model.RoleID);
                cmd.Parameters.AddWithValue("@PhoneNumber", (object)model.PhoneNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                cmd.Parameters.AddWithValue("@CreatedAt", model.CreatedAt);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void Update(User model)
        {
            const string sql = @"UPDATE Users SET FullName=@FullName, Email=@Email, PasswordHash=@PasswordHash, PasswordSalt=@PasswordSalt, RoleID=@RoleID, PhoneNumber=@PhoneNumber, IsActive=@IsActive, LastLoginAt=@LastLoginAt WHERE UserID=@UserID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@FullName", (object)model.FullName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)model.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PasswordHash", (object)model.PasswordHash ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PasswordSalt", (object)model.PasswordSalt ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@RoleID", model.RoleID);
                cmd.Parameters.AddWithValue("@PhoneNumber", (object)model.PhoneNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                cmd.Parameters.AddWithValue("@LastLoginAt", (object)model.LastLoginAt ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UserID", model.UserID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EnableStudent(int userId)
        {
            SetActive(userId, true);
        }

        public void DisableStudent(int userId)
        {
            SetActive(userId, false);
        }

        private void SetActive(int userId, bool isActive)
        {
            const string sql = @"UPDATE Users SET IsActive = @IsActive WHERE UserID = @UserID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@UserID", userId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int userId)
        {
            // Keep for compatibility; soft delete
            SetActive(userId, false);
        }

        private User ReadUser(SqlDataReader reader)
        {
            return new User
            {
                UserID = (int)reader["UserID"],
                FullName = reader["FullName"] as string,
                Username = reader["Username"] as string,
                Email = reader["Email"] as string,
                PasswordHash = reader["PasswordHash"] as string,
                PasswordSalt = reader["PasswordSalt"] as string,
                RoleID = (int)reader["RoleID"],
                PhoneNumber = reader["PhoneNumber"] as string,
                IsActive = (bool)reader["IsActive"],
                CreatedAt = (DateTime)reader["CreatedAt"],
                LastLoginAt = reader["LastLoginAt"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["LastLoginAt"]
            };
        }
    }
}
