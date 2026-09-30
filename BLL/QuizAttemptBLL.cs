using System;
using System.Collections.Generic;
using System.Data;
using Aarambha.Data_Access_Layer;
using Aarambha.Models;

namespace Aarambha.BLL
{
    public class QuizAttemptBLL
    {
        private readonly QuizAttemptDAL _dal = new QuizAttemptDAL();

        public QuizAttempt GetById(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid attempt id.");
            return _dal.GetById(id);
        }

        public List<QuizAttempt> GetByUserId(int userId)
        {
            if (userId <= 0) throw new ValidationException("Invalid user id.");
            return _dal.GetByUserId(userId);
        }

        public List<QuizAttempt> GetAll() => _dal.GetAll();

        public int Create(QuizAttempt model)
        {
            if (model == null) throw new ValidationException("Attempt is required.");
            if (model.UserID <= 0) throw new ValidationException("Invalid user id.");
            if (model.QuizID <= 0) throw new ValidationException("Invalid quiz id.");
            if (model.TotalMarks <= 0) throw new ValidationException("Total marks must be greater than zero.");
            return _dal.Insert(model);
        }

        public DataTable GetAllResultsWithUserAndQuiz() => _dal.GetAllResultsWithUserAndQuiz();
    }
}
