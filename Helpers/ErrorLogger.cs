using System;
using System.IO;
using System.Web;

namespace Aarambha.Helpers
{
    public static class ErrorLogger
    {
        private static readonly object _lock = new object();
        public static void Log(Exception ex)
        {
            try
            {
                var msg = $"[{DateTime.UtcNow:u}] {ex.GetType()}: {ex.Message}\n{ex.StackTrace}\n";
                var path = HttpContext.Current == null ? "App_Data/error.log" : HttpContext.Current.Server.MapPath("~/App_Data/error.log");
                lock (_lock)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    File.AppendAllText(path, msg);
                }
            }
            catch
            {
                // swallow - logging must not throw
            }
        }
    }
}
