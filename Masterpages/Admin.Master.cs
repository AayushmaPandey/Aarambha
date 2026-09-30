using System;

namespace Aarambha.Masterpages
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Central guard: only logged-in Teachers (RoleID = 1) may see any Admin/ page
            if (Session["UserID"] == null || Session["RoleID"]?.ToString() != "1")
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            litTeacherName.Text = Server.HtmlEncode(
                Session["FullName"] as string ?? "Teacher");
        }
    }
}