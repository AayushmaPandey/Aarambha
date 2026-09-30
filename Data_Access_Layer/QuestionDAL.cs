using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Aarambha.Models;

namespace Aarambha.Data_Access_Layer
{
    public class QuestionDAL
    {
        public List<Question> GetAll()
        {
            var list = new List<Question>();
            const string sql = "SELECT QuestionID, QuizID, QuestionText, QuestionType, Marks FROM Questions";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) list.Add(ReadQuestion(reader));
                }
            }
            return list;
        }

        public Question GetById(int questionId)
        {
            const string sql = "SELECT QuestionID, QuizID, QuestionText, QuestionType, Marks FROM Questions WHERE QuestionID = @QuestionID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@QuestionID", questionId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) return ReadQuestion(reader);
                }
            }
            return null;
        }

        public List<Question> GetByQuizId(int quizId)
        {
            var list = new List<Question>();
            const string sql = "SELECT QuestionID, QuizID, QuestionText, QuestionType, Marks FROM Questions WHERE QuizID = @QuizID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@QuizID", quizId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) list.Add(ReadQuestion(reader));
                }
            }
            return list;
        }

        public int Insert(Question model)
        {
            const string sql = @"INSERT INTO Questions (QuizID, QuestionText, QuestionType, Marks) OUTPUT INSERTED.QuestionID VALUES (@QuizID, @QuestionText, @QuestionType, @Marks)";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@QuizID", model.QuizID);
                cmd.Parameters.AddWithValue("@QuestionText", model.QuestionText);
                cmd.Parameters.AddWithValue("@QuestionType", model.QuestionType);
                cmd.Parameters.AddWithValue("@Marks", model.Marks);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void Update(Question model)
        {
            const string sql = @"UPDATE Questions SET QuizID=@QuizID, QuestionText=@QuestionText, QuestionType=@QuestionType, Marks=@Marks WHERE QuestionID=@QuestionID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@QuizID", model.QuizID);
                cmd.Parameters.AddWithValue("@QuestionText", model.QuestionText);
                cmd.Parameters.AddWithValue("@QuestionType", model.QuestionType);
                cmd.Parameters.AddWithValue("@Marks", model.Marks);
                cmd.Parameters.AddWithValue("@QuestionID", model.QuestionID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int questionId)
        {
            const string sql = "DELETE FROM Questions WHERE QuestionID = @QuestionID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@QuestionID", questionId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private Question ReadQuestion(SqlDataReader reader)
        {
            return new Question
            {
                QuestionID = (int)reader["QuestionID"],
                QuizID = (int)reader["QuizID"],
                QuestionText = reader["QuestionText"] as string,
                QuestionType = reader["QuestionType"] as string,
                Marks = (int)reader["Marks"]
            };
        }
    }
}
