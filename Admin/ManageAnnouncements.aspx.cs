using System;
using Aarambha.BLL;
using Aarambha.Models;

namespace Aarambha.Admin
{
    public partial class ManageAnnouncements : System.Web.UI.Page
    {
        private readonly AnnouncementBLL _annBll = new AnnouncementBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckRole();
                LoadAnnouncements();
            }
        }

        private void CheckRole()
        {
            if (Session["UserID"] == null || Session["RoleID"] == null || (int)Session["RoleID"] != 1)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var ann = new Announcement
                {
                    Title = txtTitle.Text.Trim(),
                    Message = txtContent.Text.Trim(),
                    PostedAt = DateTime.UtcNow,
                    PostedBy = (int)Session["UserID"],
                    IsActive = true
                };
                _annBll.Create(ann);
                ltAlert.Text = "<div class=\"alert alert-success\">Announcement posted.</div>";
                LoadAnnouncements();
                txtTitle.Text = txtContent.Text = string.Empty;
            }
            catch (ValidationException ex)
            {
                ltAlert.Text = $"<div class=\"alert alert-danger\">{Server.HtmlEncode(ex.Message)}</div>";
            }
            catch (Exception ex)
            {
                ltAlert.Text = $"<div class=\"alert alert-danger\">Error: {Server.HtmlEncode(ex.Message)}</div>";
            }
        }

        protected void gvAnnouncements_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int annId;
                if (int.TryParse(e.CommandArgument.ToString(), out annId))
                {
                    try
                    {
                        _annBll.Delete(annId);
                        LoadAnnouncements();
                    }
                    catch { }
                }
            }
        }

        private void LoadAnnouncements()
        {
            var anns = _annBll.GetAll();
            gvAnnouncements.DataSource = anns;
            gvAnnouncements.DataBind();
        }
    }
}
