using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Aarambha.Models;

namespace Aarambha.Data_Access_Layer
{
    public class QuizAttemptDAL
    {
        public QuizAttempt GetById(int attemptId)
        {
            const string sql = "SELECT AttemptID, UserID, QuizID, Score, TotalMarks, IsPassed, AttemptedAt FROM QuizAttempts WHERE AttemptID = @AttemptID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@AttemptID", attemptId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) return ReadAttempt(reader);
                }
            }
            return null;
        }

        public List<QuizAttempt> GetByUserId(int userId)
        {
            var list = new List<QuizAttempt>();
            const string sql = "SELECT AttemptID, UserID, QuizID, Score, TotalMarks, IsPassed, AttemptedAt FROM QuizAttempts WHERE UserID = @UserID ORDER BY AttemptedAt DESC";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) list.Add(ReadAttempt(reader));
                }
            }
            return list;
        }

        public List<QuizAttempt> GetAll()
        {
            var list = new List<QuizAttempt>();
            const string sql = "SELECT AttemptID, UserID, QuizID, Score, TotalMarks, IsPassed, AttemptedAt FROM QuizAttempts ORDER BY AttemptedAt DESC";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) list.Add(ReadAttempt(reader));
                }
            }
            return list;
        }

        public int Insert(QuizAttempt model)
        {
            const string sql = @"INSERT INTO QuizAttempts (UserID, QuizID, Score, TotalMarks, IsPassed, AttemptedAt) OUTPUT INSERTED.AttemptID VALUES (@UserID, @QuizID, @Score, @TotalMarks, @IsPassed, @AttemptedAt)";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", model.UserID);
                cmd.Parameters.AddWithValue("@QuizID", model.QuizID);
                cmd.Parameters.AddWithValue("@Score", model.Score);
                cmd.Parameters.AddWithValue("@TotalMarks", model.TotalMarks);
                cmd.Parameters.AddWithValue("@IsPassed", model.IsPassed);
                cmd.Parameters.AddWithValue("@AttemptedAt", model.AttemptedAt);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public DataTable GetAllResultsWithUserAndQuiz()
        {
            const string sql = @"SELECT qa.AttemptID, qa.UserID, qa.QuizID, u.FullName, u.Username, q.Title AS QuizTitle, qa.Score, qa.TotalMarks, qa.IsPassed, qa.AttemptedAt
                                 FROM QuizAttempts qa
                                 INNER JOIN Users u ON qa.UserID = u.UserID
                                 INNER JOIN Quizzes q ON qa.QuizID = q.QuizID
                                 ORDER BY qa.AttemptedAt DESC";
            var dt = new DataTable();
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                conn.Open();
                da.Fill(dt);
            }
            return dt;
        }

        private QuizAttempt ReadAttempt(SqlDataReader reader)
        {
            return new QuizAttempt
            {
                AttemptID = (int)reader["AttemptID"],
                UserID = (int)reader["UserID"],
                QuizID = (int)reader["QuizID"],
                Score = (int)reader["Score"],
                TotalMarks = (int)reader["TotalMarks"],
                IsPassed = (bool)reader["IsPassed"],
                AttemptedAt = (DateTime)reader["AttemptedAt"]
            };
        }
    }
}
