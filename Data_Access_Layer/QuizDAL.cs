using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Aarambha.Models;

namespace Aarambha.Data_Access_Layer
{
    public class QuizDAL
    {
        public List<Quiz> GetAll()
        {
            var list = new List<Quiz>();
            const string sql = "SELECT QuizID, SubjectID, Title, PassMark, IsActive FROM Quizzes";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) list.Add(ReadQuiz(reader));
                }
            }
            return list;
        }

        public Quiz GetById(int quizId)
        {
            const string sql = "SELECT QuizID, SubjectID, Title, PassMark, IsActive FROM Quizzes WHERE QuizID = @QuizID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@QuizID", quizId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) return ReadQuiz(reader);
                }
            }
            return null;
        }

        public List<Quiz> GetBySubjectId(int subjectId)
        {
            var list = new List<Quiz>();
            const string sql = "SELECT QuizID, SubjectID, Title, PassMark, IsActive FROM Quizzes WHERE SubjectID = @SubjectID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@SubjectID", subjectId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) list.Add(ReadQuiz(reader));
                }
            }
            return list;
        }

        public int Insert(Quiz model)
        {
            const string sql = @"INSERT INTO Quizzes (SubjectID, Title, PassMark, IsActive) OUTPUT INSERTED.QuizID VALUES (@SubjectID, @Title, @PassMark, @IsActive)";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                cmd.Parameters.AddWithValue("@Title", model.Title);
                cmd.Parameters.AddWithValue("@PassMark", model.PassMark);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void Update(Quiz model)
        {
            const string sql = @"UPDATE Quizzes SET SubjectID=@SubjectID, Title=@Title, PassMark=@PassMark, IsActive=@IsActive WHERE QuizID=@QuizID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                cmd.Parameters.AddWithValue("@Title", model.Title);
                cmd.Parameters.AddWithValue("@PassMark", model.PassMark);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                cmd.Parameters.AddWithValue("@QuizID", model.QuizID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int quizId)
        {
            // Delete dependent rows first to avoid FK constraint violations
            // Order: QuizAttempts -> QuestionOptions -> Questions -> Quizzes
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("DELETE FROM QuizAttempts WHERE QuizID = @QuizID", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@QuizID", quizId);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("DELETE FROM QuestionOptions WHERE QuestionID IN (SELECT QuestionID FROM Questions WHERE QuizID = @QuizID)", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@QuizID", quizId);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("DELETE FROM Questions WHERE QuizID = @QuizID", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@QuizID", quizId);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand("DELETE FROM Quizzes WHERE QuizID = @QuizID", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@QuizID", quizId);
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public void SetActive(int quizId, bool isActive)
        {
            const string sql = "UPDATE Quizzes SET IsActive = @IsActive WHERE QuizID = @QuizID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@QuizID", quizId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private Quiz ReadQuiz(SqlDataReader reader)
        {
            return new Quiz
            {
                QuizID = (int)reader["QuizID"],
                SubjectID = (int)reader["SubjectID"],
                Title = reader["Title"] as string,
                PassMark = (int)reader["PassMark"],
                IsActive = (bool)reader["IsActive"]
            };
        }
    }
}
