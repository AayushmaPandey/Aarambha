using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Aarambha.Models;

namespace Aarambha.Data_Access_Layer
{
    public class SubjectDAL
    {
        public List<Subject> GetAll()
        {
            var list = new List<Subject>();
            const string sql = "SELECT SubjectID, SubjectName, Description, IconPath, IsActive FROM Subjects";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(ReadSubject(reader));
                    }
                }
            }
            return list;
        }

        public Subject GetById(int subjectId)
        {
            const string sql = "SELECT SubjectID, SubjectName, Description, IconPath, IsActive FROM Subjects WHERE SubjectID = @SubjectID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@SubjectID", subjectId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ReadSubject(reader);
                    }
                }
            }
            return null;
        }

        public int Insert(Subject model)
        {
            const string sql = @"INSERT INTO Subjects (SubjectName, Description, IconPath, IsActive) OUTPUT INSERTED.SubjectID VALUES (@SubjectName, @Description, @IconPath, @IsActive)";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@SubjectName", model.SubjectName);
                cmd.Parameters.AddWithValue("@Description", (object)model.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IconPath", (object)model.IconPath ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void Update(Subject model)
        {
            const string sql = @"UPDATE Subjects SET SubjectName=@SubjectName, Description=@Description, IconPath=@IconPath, IsActive=@IsActive WHERE SubjectID=@SubjectID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@SubjectName", model.SubjectName);
                cmd.Parameters.AddWithValue("@Description", (object)model.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IconPath", (object)model.IconPath ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                cmd.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int subjectId)
        {
            const string sql = "DELETE FROM Subjects WHERE SubjectID = @SubjectID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@SubjectID", subjectId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void SetActive(int subjectId, bool isActive)
        {
            const string sql = "UPDATE Subjects SET IsActive = @IsActive WHERE SubjectID = @SubjectID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@SubjectID", subjectId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Subject> Search(string term)
        {
            var list = new List<Subject>();
            const string sql = "SELECT SubjectID, SubjectName, Description, IconPath, IsActive FROM Subjects WHERE SubjectName LIKE @term OR Description LIKE @term";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@term", "%" + term + "%");
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(ReadSubject(reader));
                    }
                }
            }
            return list;
        }

        private Subject ReadSubject(SqlDataReader reader)
        {
            return new Subject
            {
                SubjectID = (int)reader["SubjectID"],
                SubjectName = reader["SubjectName"] as string,
                Description = reader["Description"] as string,
                IconPath = reader["IconPath"] as string,
                IsActive = (bool)reader["IsActive"]
            };
        }
    }
}
