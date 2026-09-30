using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Aarambha.Data_Access_Layer
{
    public static class DbHelper
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["AarambhaDB"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
