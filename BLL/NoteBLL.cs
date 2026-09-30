using System;
using System.Collections.Generic;
using Aarambha.Data_Access_Layer;
using Aarambha.Models;

namespace Aarambha.BLL
{
    public class NoteBLL
    {
        private readonly NoteDAL _dal = new NoteDAL();

        public List<Note> GetAll() => _dal.GetAll();

        public Note GetById(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid note id.");
            return _dal.GetById(id);
        }

        public List<Note> GetBySubjectId(int subjectId)
        {
            if (subjectId <= 0) throw new ValidationException("Invalid subject id.");
            return _dal.GetBySubjectId(subjectId);
        }

        public int Create(Note model)
        {
            if (model == null) throw new ValidationException("Note is required.");
            if (string.IsNullOrWhiteSpace(model.Title)) throw new ValidationException("Title is required.");
            if (model.Title.Length > 200) throw new ValidationException("Title max length is 200.");
            return _dal.Insert(model);
        }

        public void Update(Note model)
        {
            if (model == null) throw new ValidationException("Note is required.");
            if (model.NoteID <= 0) throw new ValidationException("Invalid note id.");
            _dal.Update(model);
        }

        public void Delete(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid note id.");
            _dal.Delete(id);
        }
    }
}
