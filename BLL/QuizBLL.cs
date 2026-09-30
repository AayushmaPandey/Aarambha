using System;
using System.Collections.Generic;
using Aarambha.Data_Access_Layer;
using Aarambha.Models;

namespace Aarambha.BLL
{
    public class QuizBLL
    {
        private readonly QuizDAL _dal = new QuizDAL();

        public List<Quiz> GetAll() => _dal.GetAll();

        public Quiz GetById(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid quiz id.");
            return _dal.GetById(id);
        }

        public List<Quiz> GetBySubjectId(int subjectId)
        {
            if (subjectId <= 0) throw new ValidationException("Invalid subject id.");
            return _dal.GetBySubjectId(subjectId);
        }

        public int Create(Quiz model)
        {
            if (model == null) throw new ValidationException("Quiz is required.");
            if (string.IsNullOrWhiteSpace(model.Title)) throw new ValidationException("Quiz title is required.");
            if (model.PassMark < 0 || model.PassMark > 100) throw new ValidationException("Pass mark must be between 0 and 100.");
            return _dal.Insert(model);
        }

        public void Update(Quiz model)
        {
            if (model == null) throw new ValidationException("Quiz is required.");
            if (model.QuizID <= 0) throw new ValidationException("Invalid quiz id.");
            if (model.PassMark < 0 || model.PassMark > 100) throw new ValidationException("Pass mark must be between 0 and 100.");
            _dal.Update(model);
        }

        public void Delete(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid quiz id.");
            _dal.Delete(id);
        }

        public void SetActive(int id, bool isActive)
        {
            if (id <= 0) throw new ValidationException("Invalid quiz id.");
            _dal.SetActive(id, isActive);
        }
    }
}
