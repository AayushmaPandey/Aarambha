using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Aarambha.Models;

namespace Aarambha.Data_Access_Layer
{
    public class ResourceDAL
    {
        public Resource GetById(int resourceId)
        {
            const string sql = "SELECT ResourceID, NoteID, Title, FilePath, UploadedAt FROM Resources WHERE ResourceID = @ResourceID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ResourceID", resourceId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ReadResource(reader);
                    }
                }
            }
            return null;
        }

        public List<Resource> GetByNoteId(int noteId)
        {
            var list = new List<Resource>();
            const string sql = "SELECT ResourceID, NoteID, Title, FilePath, UploadedAt FROM Resources WHERE NoteID = @NoteID ORDER BY UploadedAt DESC";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@NoteID", noteId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) list.Add(ReadResource(reader));
                }
            }
            return list;
        }

        public int Insert(Resource model)
        {
            const string sql = @"INSERT INTO Resources (NoteID, Title, FilePath, UploadedAt) OUTPUT INSERTED.ResourceID VALUES (@NoteID, @Title, @FilePath, @UploadedAt)";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@NoteID", model.NoteID);
                cmd.Parameters.AddWithValue("@Title", model.Title);
                cmd.Parameters.AddWithValue("@FilePath", model.FilePath);
                cmd.Parameters.AddWithValue("@UploadedAt", model.UploadedAt);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void Delete(int resourceId)
        {
            const string sql = "DELETE FROM Resources WHERE ResourceID = @ResourceID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ResourceID", resourceId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private Resource ReadResource(SqlDataReader reader)
        {
            return new Resource
            {
                ResourceID = (int)reader["ResourceID"],
                NoteID = (int)reader["NoteID"],
                Title = reader["Title"] as string,
                FilePath = reader["FilePath"] as string,
                UploadedAt = (DateTime)reader["UploadedAt"]
            };
        }
    }
}
