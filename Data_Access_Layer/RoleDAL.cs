using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Aarambha.Models;

namespace Aarambha.Data_Access_Layer
{
    public class RoleDAL
    {
        public List<Role> GetAll()
        {
            var list = new List<Role>();
            const string sql = "SELECT RoleID, RoleName, Description FROM Roles";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Role
                        {
                            RoleID = (int)reader["RoleID"],
                            RoleName = reader["RoleName"] as string,
                            Description = reader["Description"] as string
                        });
                    }
                }
            }
            return list;
        }

        public Role GetById(int roleId)
        {
            const string sql = "SELECT RoleID, RoleName, Description FROM Roles WHERE RoleID = @RoleID";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@RoleID", roleId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Role
                        {
                            RoleID = (int)reader["RoleID"],
                            RoleName = reader["RoleName"] as string,
                            Description = reader["Description"] as string
                        };
                    }
                }
            }
            return null;
        }
    }
}
