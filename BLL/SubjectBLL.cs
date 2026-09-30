using System;
using System.Collections.Generic;
using Aarambha.Data_Access_Layer;
using Aarambha.Models;

namespace Aarambha.BLL
{
    public class SubjectBLL
    {
        private readonly SubjectDAL _dal = new SubjectDAL();

        public List<Subject> GetAll() => _dal.GetAll();

        public Subject GetById(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid subject id.");
            return _dal.GetById(id);
        }

        public int Create(Subject model)
        {
            if (model == null) throw new ValidationException("Subject is required.");
            if (string.IsNullOrWhiteSpace(model.SubjectName)) throw new ValidationException("Subject name is required.");
            if (model.SubjectName.Length > 100) throw new ValidationException("Subject name max length is 100.");

            return _dal.Insert(model);
        }

        public void Update(Subject model)
        {
            if (model == null) throw new ValidationException("Subject is required.");
            if (model.SubjectID <= 0) throw new ValidationException("Invalid subject id.");
            _dal.Update(model);
        }

        public void Delete(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid subject id.");
            _dal.Delete(id);
        }

        public void SetActive(int id, bool isActive)
        {
            if (id <= 0) throw new ValidationException("Invalid subject id.");
            _dal.SetActive(id, isActive);
        }

        public List<Subject> Search(string term)
        {
            return _dal.Search(term ?? string.Empty);
        }
    }
}
