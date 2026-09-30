using System;
using System.Collections.Generic;
using Aarambha.Data_Access_Layer;
using Aarambha.Models;

namespace Aarambha.BLL
{
    public class QuestionBLL
    {
        private readonly QuestionDAL _dal = new QuestionDAL();
        private readonly string[] AllowedTypes = new[] { "SingleChoice", "MultipleChoice", "TrueFalse" };

        public List<Question> GetAll() => _dal.GetAll();

        public Question GetById(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid question id.");
            return _dal.GetById(id);
        }

        public List<Question> GetByQuizId(int quizId)
        {
            if (quizId <= 0) throw new ValidationException("Invalid quiz id.");
            return _dal.GetByQuizId(quizId);
        }

        public int Create(Question model)
        {
            if (model == null) throw new ValidationException("Question is required.");
            if (model.QuizID <= 0) throw new ValidationException("Invalid quiz id.");
            if (string.IsNullOrWhiteSpace(model.QuestionText)) throw new ValidationException("Question text is required.");
            if (Array.IndexOf(AllowedTypes, model.QuestionType) < 0) throw new ValidationException("Invalid question type.");
            if (model.Marks <= 0) throw new ValidationException("Marks must be greater than zero.");
            return _dal.Insert(model);
        }

        public void Update(Question model)
        {
            if (model == null) throw new ValidationException("Question is required.");
            if (model.QuestionID <= 0) throw new ValidationException("Invalid question id.");
            if (Array.IndexOf(AllowedTypes, model.QuestionType) < 0) throw new ValidationException("Invalid question type.");
            _dal.Update(model);
        }

        public void Delete(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid question id.");
            _dal.Delete(id);
        }
    }
}
