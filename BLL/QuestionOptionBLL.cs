using System;
using System.Collections.Generic;
using Aarambha.Data_Access_Layer;
using Aarambha.Models;

namespace Aarambha.BLL
{
    public class QuestionOptionBLL
    {
        private readonly QuestionOptionDAL _dal = new QuestionOptionDAL();

        public List<QuestionOption> GetAll() => _dal.GetAll();

        public QuestionOption GetById(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid option id.");
            return _dal.GetById(id);
        }

        public List<QuestionOption> GetByQuestionId(int questionId)
        {
            if (questionId <= 0) throw new ValidationException("Invalid question id.");
            return _dal.GetByQuestionId(questionId);
        }

        public int Create(QuestionOption model)
        {
            if (model == null) throw new ValidationException("Option is required.");
            if (model.QuestionID <= 0) throw new ValidationException("Invalid question id.");
            if (string.IsNullOrWhiteSpace(model.OptionText)) throw new ValidationException("Option text is required.");
            if (model.OptionText.Length > 250) throw new ValidationException("Option text max length is 250.");
            return _dal.Insert(model);
        }

        public void Update(QuestionOption model)
        {
            if (model == null) throw new ValidationException("Option is required.");
            if (model.OptionID <= 0) throw new ValidationException("Invalid option id.");
            _dal.Update(model);
        }

        public void Delete(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid option id.");
            _dal.Delete(id);
        }
    }
}
