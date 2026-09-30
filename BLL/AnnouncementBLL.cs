using System;
using System.Collections.Generic;
using Aarambha.Data_Access_Layer;
using Aarambha.Models;

namespace Aarambha.BLL
{
    public class AnnouncementBLL
    {
        private readonly AnnouncementDAL _dal = new AnnouncementDAL();

        public List<Announcement> GetAll() => _dal.GetAll();

        public List<Announcement> GetActive() => _dal.GetActive();

        public Announcement GetById(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid announcement id.");
            return _dal.GetById(id);
        }

        public int Create(Announcement model)
        {
            if (model == null) throw new ValidationException("Announcement is required.");
            if (string.IsNullOrWhiteSpace(model.Title)) throw new ValidationException("Title is required.");
            if (string.IsNullOrWhiteSpace(model.Message)) throw new ValidationException("Message is required.");
            if (model.Title.Length > 150) throw new ValidationException("Title max length is 150.");
            model.PostedAt = DateTime.UtcNow;
            return _dal.Insert(model);
        }

        public void Update(Announcement model)
        {
            if (model == null) throw new ValidationException("Announcement is required.");
            if (model.AnnouncementID <= 0) throw new ValidationException("Invalid announcement id.");
            _dal.Update(model);
        }

        public void Delete(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid announcement id.");
            _dal.Delete(id);
        }

        public void SetActive(int id, bool isActive)
        {
            if (id <= 0) throw new ValidationException("Invalid announcement id.");
            _dal.SetActive(id, isActive);
        }
    }
}
