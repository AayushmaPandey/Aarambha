using System;
using System.Collections.Generic;
using Aarambha.Data_Access_Layer;
using Aarambha.Models;

namespace Aarambha.BLL
{
    public class ContactMessageBLL
    {
        private readonly ContactMessageDAL _dal = new ContactMessageDAL();

        public List<ContactMessage> GetAll() => _dal.GetAll();

        public ContactMessage GetById(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid message id.");
            return _dal.GetById(id);
        }

        public int Create(ContactMessage model)
        {
            if (model == null) throw new ValidationException("Message is required.");
            if (string.IsNullOrWhiteSpace(model.Name)) throw new ValidationException("Name is required.");
            if (string.IsNullOrWhiteSpace(model.Email)) throw new ValidationException("Email is required.");
            if (!model.Email.Contains("@")) throw new ValidationException("Invalid email.");
            if (string.IsNullOrWhiteSpace(model.Subject)) throw new ValidationException("Subject is required.");
            if (string.IsNullOrWhiteSpace(model.Message)) throw new ValidationException("Message is required.");
            model.SubmittedAt = DateTime.UtcNow;
            model.IsRead = false;
            return _dal.Insert(model);
        }

        public void MarkAsRead(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid message id.");
            _dal.MarkAsRead(id);
        }

        public void MarkAsUnread(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid message id.");
            _dal.MarkAsUnread(id);
        }
    }
}
