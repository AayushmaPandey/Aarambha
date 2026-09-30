using System;
using System.Text;
using Aarambha.BLL;

namespace Aarambha.Member
{
    public partial class Announcements : System.Web.UI.Page
    {
        private readonly AnnouncementBLL _annBll = new AnnouncementBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null || Session["RoleID"] == null || (int)Session["RoleID"] != 2)
                {
                    Response.Redirect("~/Account/Login.aspx");
                    return;
                }

                LoadAnnouncements();
            }
        }

        private void LoadAnnouncements()
        {
            var announcements = _annBll.GetAll();
            var sb = new StringBuilder();

            if (announcements.Count == 0)
            {
                ltAnnouncements.Text = "<p class=\"alert alert-info\">No announcements at this time.</p>";
                return;
            }

            foreach (var ann in announcements)
            {
                sb.Append("<div class=\"card mb-3\">");
                sb.Append($"<div class=\"card-header bg-primary text-white\"><h5>{Server.HtmlEncode(ann.Title)}</h5></div>");
                sb.Append("<div class=\"card-body\">");
                sb.Append($"<p>{Server.HtmlEncode(ann.Message)}</p>");
                sb.Append($"<small class=\"text-muted\">Posted on {ann.PostedAt:g}</small>");
                sb.Append("</div>");
                sb.Append("</div>");
            }

            ltAnnouncements.Text = sb.ToString();
        }
    }
}
