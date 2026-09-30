using System;
using System.Web;
using System.Web.Routing;

namespace Aarambha
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Register routes or bundles if needed
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        // Only Teachers (RoleID 1) may open /Admin pages (add / edit / delete);
        // /Member pages need any logged-in user.
        protected void Application_PostAcquireRequestState(object sender, EventArgs e)
        {
            var ctx = HttpContext.Current;
            if (ctx == null || ctx.Session == null) return;
            var path = ctx.Request.AppRelativeCurrentExecutionFilePath ?? "";
            bool admin = path.StartsWith("~/Admin/", StringComparison.OrdinalIgnoreCase);
            bool member = path.StartsWith("~/Member/", StringComparison.OrdinalIgnoreCase);
            if (!admin && !member) return;
            var role = ctx.Session["RoleID"] == null ? null : ctx.Session["RoleID"].ToString();
            if (ctx.Session["UserID"] == null || (admin && role != "1"))
                ctx.Response.Redirect("~/Account/Login.aspx", true);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Simple error logging could be added here
        }
    }
}
