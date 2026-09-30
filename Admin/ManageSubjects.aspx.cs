using System;
using Aarambha.BLL;
using Aarambha.Models;

namespace Aarambha.Admin
{
    public partial class ManageSubjects : System.Web.UI.Page
    {
        private readonly SubjectBLL _subjectBll = new SubjectBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckRole();
                LoadSubjects();
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
                var subject = new Subject
                {
                    SubjectName = txtSubjectName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    IconPath = "",
                    IsActive = true
                };
                _subjectBll.Create(subject);
                ltAlert.Text = "<div class=\"alert alert-success\">Subject added.</div>";
                LoadSubjects();
                txtSubjectName.Text = txtDescription.Text = string.Empty;
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

        protected void gvSubjects_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Toggle")
            {
                int subjectId;
                if (int.TryParse(e.CommandArgument.ToString(), out subjectId))
                {
                    try
                    {
                        var subject = _subjectBll.GetById(subjectId);
                        subject.IsActive = !subject.IsActive;
                        _subjectBll.Update(subject);
                        LoadSubjects();
                    }
                    catch { }
                }
            }
            else if (e.CommandName == "Delete")
            {
                int subjectId;
                if (int.TryParse(e.CommandArgument.ToString(), out subjectId))
                {
                    try
                    {
                        _subjectBll.Delete(subjectId);
                        LoadSubjects();
                    }
                    catch { }
                }
            }
        }

        private void LoadSubjects()
        {
            var subjects = _subjectBll.GetAll();
            gvSubjects.DataSource = subjects;
            gvSubjects.DataBind();
        }
    }
}
