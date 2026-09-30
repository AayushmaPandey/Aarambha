<%@ Page Language="C#" %>
<%@ Import Namespace="System.Security.Principal" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Configuration" %>
<!DOCTYPE html>
<html>
<head><title>DB Check</title></head>
<body style="font-family:consolas,monospace;font-size:14px;">
<h2>Runtime Diagnostic</h2>
<pre><%
    Response.Write("WindowsIdentity.GetCurrent().Name : " + WindowsIdentity.GetCurrent().Name + "\n");
    Response.Write("Environment.UserName              : " + Environment.UserName + "\n");
    Response.Write("Environment.UserDomainName         : " + Environment.UserDomainName + "\n");
    Response.Write("Page.User.Identity.Name (if any)   : " + (Page.User != null ? Page.User.Identity.Name : "(none)") + "\n\n");

    var connStr = ConfigurationManager.ConnectionStrings["AarambhaDB"].ConnectionString;
    Response.Write("ConnectionString from Web.config   : " + connStr + "\n\n");

    try
    {
        using (var conn = new SqlConnection(connStr))
        {
            conn.Open();
            Response.Write("CONNECTION OPENED SUCCESSFULLY.\n");
            using (var cmd = new SqlCommand("SELECT SUSER_SNAME(), DB_NAME()", conn))
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Response.Write("SQL Server sees login as   : " + reader.GetString(0) + "\n");
                    Response.Write("Connected to database       : " + reader.GetString(1) + "\n");
                }
            }
        }
    }
    catch (Exception ex)
    {
        Response.Write("CONNECTION FAILED:\n");
        Response.Write(ex.GetType().FullName + "\n");
        Response.Write(ex.Message + "\n");
        if (ex.InnerException != null)
        {
            Response.Write("\nInner: " + ex.InnerException.Message + "\n");
        }
    }
%></pre>
</body>
</html>
