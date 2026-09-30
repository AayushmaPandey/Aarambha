using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Aarambha.Models;

namespace Aarambha.Data_Access_Layer
{
    public class NoteDAL
    {
        public List<Note> GetAll()
        {
            var list = new List<Note>();
            const string sql = "SELECT NoteID, SubjectID, Title, ContentHtml, SortOrder FROM Notes ORDER BY SortOrder";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(ReadNote(reader));
                    }
                }
            }
            return list;
        }

        public Note GetById(int noteId)
        {
            const string sql = "SELECT NoteID, SubjectID, Title, ContentHtml, SortOrder FROM Notes WHERE NoteID = @NoteID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@NoteID", noteId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) return ReadNote(reader);
                }
            }
            return null;
        }

        public List<Note> GetBySubjectId(int subjectId)
        {
            var list = new List<Note>();
            const string sql = "SELECT NoteID, SubjectID, Title, ContentHtml, SortOrder FROM Notes WHERE SubjectID = @SubjectID ORDER BY SortOrder";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@SubjectID", subjectId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) list.Add(ReadNote(reader));
                }
            }
            return list;
        }

        public int Insert(Note model)
        {
            const string sql = @"INSERT INTO Notes (SubjectID, Title, ContentHtml, SortOrder) OUTPUT INSERTED.NoteID VALUES (@SubjectID, @Title, @ContentHtml, @SortOrder)";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                cmd.Parameters.AddWithValue("@Title", model.Title);
                cmd.Parameters.AddWithValue("@ContentHtml", (object)model.ContentHtml ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SortOrder", model.SortOrder);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void Update(Note model)
        {
            const string sql = @"UPDATE Notes SET SubjectID=@SubjectID, Title=@Title, ContentHtml=@ContentHtml, SortOrder=@SortOrder WHERE NoteID=@NoteID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                cmd.Parameters.AddWithValue("@Title", model.Title);
                cmd.Parameters.AddWithValue("@ContentHtml", (object)model.ContentHtml ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SortOrder", model.SortOrder);
                cmd.Parameters.AddWithValue("@NoteID", model.NoteID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int noteId)
        {
            const string sql = "DELETE FROM Notes WHERE NoteID = @NoteID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@NoteID", noteId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private Note ReadNote(SqlDataReader reader)
        {
            return new Note
            {
                NoteID = (int)reader["NoteID"],
                SubjectID = (int)reader["SubjectID"],
                Title = reader["Title"] as string,
                ContentHtml = reader["ContentHtml"] as string,
                SortOrder = (int)reader["SortOrder"]
            };
        }
    }
}
