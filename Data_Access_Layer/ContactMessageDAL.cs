using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Aarambha.Models;

namespace Aarambha.Data_Access_Layer
{
    public class ContactMessageDAL
    {
        public List<ContactMessage> GetAll()
        {
            var list = new List<ContactMessage>();
            const string sql = "SELECT MessageID, Name, Email, Subject, Message, IsRead, SubmittedAt FROM ContactMessages ORDER BY SubmittedAt DESC";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(ReadMessage(reader));
                    }
                }
            }
            return list;
        }

        public ContactMessage GetById(int messageId)
        {
            const string sql = "SELECT MessageID, Name, Email, Subject, Message, IsRead, SubmittedAt FROM ContactMessages WHERE MessageID = @MessageID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MessageID", messageId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) return ReadMessage(reader);
                }
            }
            return null;
        }

        public int Insert(ContactMessage model)
        {
            const string sql = @"INSERT INTO ContactMessages (Name, Email, Subject, Message, IsRead, SubmittedAt) OUTPUT INSERTED.MessageID VALUES (@Name, @Email, @Subject, @Message, @IsRead, @SubmittedAt)";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Subject", model.Subject);
                cmd.Parameters.AddWithValue("@Message", model.Message);
                cmd.Parameters.AddWithValue("@IsRead", model.IsRead);
                cmd.Parameters.AddWithValue("@SubmittedAt", model.SubmittedAt);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void MarkAsRead(int messageId)
        {
            SetReadFlag(messageId, true);
        }

        public void MarkAsUnread(int messageId)
        {
            SetReadFlag(messageId, false);
        }

        private void SetReadFlag(int messageId, bool isRead)
        {
            const string sql = "UPDATE ContactMessages SET IsRead = @IsRead WHERE MessageID = @MessageID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IsRead", isRead);
                cmd.Parameters.AddWithValue("@MessageID", messageId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private ContactMessage ReadMessage(SqlDataReader reader)
        {
            return new ContactMessage
            {
                MessageID = (int)reader["MessageID"],
                Name = reader["Name"] as string,
                Email = reader["Email"] as string,
                Subject = reader["Subject"] as string,
                Message = reader["Message"] as string,
                IsRead = (bool)reader["IsRead"],
                SubmittedAt = (DateTime)reader["SubmittedAt"]
            };
        }
    }
}
