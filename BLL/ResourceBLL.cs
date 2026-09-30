using System;
using System.Collections.Generic;
using Aarambha.Data_Access_Layer;
using Aarambha.Models;

namespace Aarambha.BLL
{
    public class ResourceBLL
    {
        private readonly ResourceDAL _dal = new ResourceDAL();

        public Resource GetById(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid resource id.");
            return _dal.GetById(id);
        }

        public List<Resource> GetByNoteId(int noteId)
        {
            if (noteId <= 0) throw new ValidationException("Invalid note id.");
            return _dal.GetByNoteId(noteId);
        }

        public int Create(Resource model)
        {
            if (model == null) throw new ValidationException("Resource is required.");
            if (string.IsNullOrWhiteSpace(model.Title)) throw new ValidationException("Title is required.");
            if (string.IsNullOrWhiteSpace(model.FilePath)) throw new ValidationException("File path is required.");
            if (model.Title.Length > 150) throw new ValidationException("Title max length is 150.");
            return _dal.Insert(model);
        }

        public void Delete(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid resource id.");
            _dal.Delete(id);
        }
    }
}
