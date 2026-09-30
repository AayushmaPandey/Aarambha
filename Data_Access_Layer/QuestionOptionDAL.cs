using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Aarambha.Models;

namespace Aarambha.Data_Access_Layer
{
    public class QuestionOptionDAL
    {
        public List<QuestionOption> GetAll()
        {
            var list = new List<QuestionOption>();
            const string sql = "SELECT OptionID, QuestionID, OptionText, IsCorrect, SortOrder FROM QuestionOptions";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) list.Add(ReadOption(reader));
                }
            }
            return list;
        }

        public QuestionOption GetById(int optionId)
        {
            const string sql = "SELECT OptionID, QuestionID, OptionText, IsCorrect, SortOrder FROM QuestionOptions WHERE OptionID = @OptionID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@OptionID", optionId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) return ReadOption(reader);
                }
            }
            return null;
        }

        public List<QuestionOption> GetByQuestionId(int questionId)
        {
            var list = new List<QuestionOption>();
            const string sql = "SELECT OptionID, QuestionID, OptionText, IsCorrect, SortOrder FROM QuestionOptions WHERE QuestionID = @QuestionID ORDER BY SortOrder";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@QuestionID", questionId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) list.Add(ReadOption(reader));
                }
            }
            return list;
        }

        public int Insert(QuestionOption model)
        {
            const string sql = @"INSERT INTO QuestionOptions (QuestionID, OptionText, IsCorrect, SortOrder) OUTPUT INSERTED.OptionID VALUES (@QuestionID, @OptionText, @IsCorrect, @SortOrder)";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@QuestionID", model.QuestionID);
                cmd.Parameters.AddWithValue("@OptionText", model.OptionText);
                cmd.Parameters.AddWithValue("@IsCorrect", model.IsCorrect);
                cmd.Parameters.AddWithValue("@SortOrder", model.SortOrder);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void Update(QuestionOption model)
        {
            const string sql = @"UPDATE QuestionOptions SET QuestionID=@QuestionID, OptionText=@OptionText, IsCorrect=@IsCorrect, SortOrder=@SortOrder WHERE OptionID=@OptionID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@QuestionID", model.QuestionID);
                cmd.Parameters.AddWithValue("@OptionText", model.OptionText);
                cmd.Parameters.AddWithValue("@IsCorrect", model.IsCorrect);
                cmd.Parameters.AddWithValue("@SortOrder", model.SortOrder);
                cmd.Parameters.AddWithValue("@OptionID", model.OptionID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int optionId)
        {
            const string sql = "DELETE FROM QuestionOptions WHERE OptionID = @OptionID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@OptionID", optionId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private QuestionOption ReadOption(SqlDataReader reader)
        {
            return new QuestionOption
            {
                OptionID = (int)reader["OptionID"],
                QuestionID = (int)reader["QuestionID"],
                OptionText = reader["OptionText"] as string,
                IsCorrect = (bool)reader["IsCorrect"],
                SortOrder = (int)reader["SortOrder"]
            };
        }
    }
}
