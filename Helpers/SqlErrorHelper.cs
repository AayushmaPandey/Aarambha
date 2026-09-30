using System;

namespace Aarambha.Helpers
{
    public static class SqlErrorHelper
    {
        public static string GetFriendlyMessage(Exception ex)
        {
            if (ex == null) return "An unknown database error occurred.";
            // Avoid exposing SQL internals
            if (ex is System.Data.SqlClient.SqlException sqlEx)
            {
                // Basic mapping for common errors
                switch (sqlEx.Number)
                {
                    case 2627: // unique constraint
                    case 2601:
                        return "A record with the same key already exists.";
                    case 547: // FK violation
                        return "The operation failed due to related data. Please remove dependent records first.";
                    default:
                        return "A database error occurred. Please contact the administrator.";
                }
            }
            return "A data error occurred. Please contact the administrator.";
        }
    }
}
