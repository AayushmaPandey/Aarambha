using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Aarambha.Models;

namespace Aarambha.Data_Access_Layer
{
    public class AnnouncementDAL
    {
        public List<Announcement> GetAll()
        {
            var list = new List<Announcement>();
            const string sql = "SELECT AnnouncementID, Title, Message, PostedBy, PostedAt, IsActive FROM Announcements ORDER BY PostedAt DESC";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) list.Add(ReadAnnouncement(reader));
                }
            }
            return list;
        }

        public List<Announcement> GetActive()
        {
            var list = new List<Announcement>();
            const string sql = "SELECT AnnouncementID, Title, Message, PostedBy, PostedAt, IsActive FROM Announcements WHERE IsActive = 1 ORDER BY PostedAt DESC";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) list.Add(ReadAnnouncement(reader));
                }
            }
            return list;
        }

        public Announcement GetById(int announcementId)
        {
            const string sql = "SELECT AnnouncementID, Title, Message, PostedBy, PostedAt, IsActive FROM Announcements WHERE AnnouncementID = @AnnouncementID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@AnnouncementID", announcementId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) return ReadAnnouncement(reader);
                }
            }
            return null;
        }

        public int Insert(Announcement model)
        {
            const string sql = @"INSERT INTO Announcements (Title, Message, PostedBy, PostedAt, IsActive) OUTPUT INSERTED.AnnouncementID VALUES (@Title, @Message, @PostedBy, @PostedAt, @IsActive)";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Title", model.Title);
                cmd.Parameters.AddWithValue("@Message", model.Message);
                cmd.Parameters.AddWithValue("@PostedBy", model.PostedBy);
                cmd.Parameters.AddWithValue("@PostedAt", model.PostedAt);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void Update(Announcement model)
        {
            const string sql = @"UPDATE Announcements SET Title=@Title, Message=@Message, PostedBy=@PostedBy, PostedAt=@PostedAt, IsActive=@IsActive WHERE AnnouncementID=@AnnouncementID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Title", model.Title);
                cmd.Parameters.AddWithValue("@Message", model.Message);
                cmd.Parameters.AddWithValue("@PostedBy", model.PostedBy);
                cmd.Parameters.AddWithValue("@PostedAt", model.PostedAt);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                cmd.Parameters.AddWithValue("@AnnouncementID", model.AnnouncementID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int announcementId)
        {
            const string sql = "DELETE FROM Announcements WHERE AnnouncementID = @AnnouncementID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@AnnouncementID", announcementId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void SetActive(int announcementId, bool isActive)
        {
            const string sql = "UPDATE Announcements SET IsActive = @IsActive WHERE AnnouncementID = @AnnouncementID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@AnnouncementID", announcementId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private Announcement ReadAnnouncement(SqlDataReader reader)
        {
            return new Announcement
            {
                AnnouncementID = (int)reader["AnnouncementID"],
                Title = reader["Title"] as string,
                Message = reader["Message"] as string,
                PostedBy = (int)reader["PostedBy"],
                PostedAt = (DateTime)reader["PostedAt"],
                IsActive = (bool)reader["IsActive"]
            };
        }
    }
}
