using System;
using System.Collections.Generic;
using Aarambha.Data_Access_Layer;
using Aarambha.Models;

namespace Aarambha.BLL
{
    public class RoleBLL
    {
        private readonly RoleDAL _dal = new RoleDAL();

        public List<Role> GetAll() => _dal.GetAll();

        public Role GetById(int id)
        {
            if (id <= 0) throw new ValidationException("Invalid role id.");
            return _dal.GetById(id);
        }
    }
}
