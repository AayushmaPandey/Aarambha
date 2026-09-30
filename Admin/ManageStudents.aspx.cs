using System;
using Aarambha.BLL;
using Aarambha.Helpers;
using Aarambha.Models;

namespace Aarambha.Admin
{
    public partial class ManageStudents : System.Web.UI.Page
    {
        private readonly UserBLL _userBll = new UserBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckRole();
                LoadStudents();
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
                var salt = PasswordHelper.GenerateSalt();
                var hash = PasswordHelper.HashPassword(txtPassword.Text, salt);
                var user = new User
                {
                    FullName = txtFullName.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    PhoneNumber = txtPhone.Text.Trim(),
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    RoleID = 2,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };
                _userBll.Create(user);
                ltAlert.Text = "<div class=\"alert alert-success\">Student added.</div>";
                LoadStudents();
                txtFullName.Text = txtUsername.Text = txtPassword.Text = txtEmail.Text = txtPhone.Text = string.Empty;
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

        protected void gvStudents_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Toggle")
            {
                int userId;
                if (int.TryParse(e.CommandArgument.ToString(), out userId))
                {
                    try
                    {
                        var user = _userBll.GetById(userId);
                        if (user.IsActive)
                            _userBll.DisableStudent(userId);
                        else
                            _userBll.EnableStudent(userId);
                        LoadStudents();
                    }
                    catch { }
                }
            }
            else if (e.CommandName == "DeleteStudent")
            {
                int userId;
                if (int.TryParse(e.CommandArgument.ToString(), out userId))
                {
                    try
                    {
                        _userBll.Delete(userId);

                        ltAlert.Text = "<div class=\"alert alert-success\">Student deleted.</div>";
                        LoadStudents();
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
            }
        }

        protected void gvStudents_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvStudents.EditIndex = e.NewEditIndex;
            LoadStudents();
        }

        protected void gvStudents_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvStudents.EditIndex = -1;
            LoadStudents();
        }

        protected void gvStudents_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            try
            {
                var rowIndex = e.RowIndex;
                var userId = (int)gvStudents.DataKeys[rowIndex].Value;
                var user = _userBll.GetById(userId);
                var row = gvStudents.Rows[rowIndex];
                var txtFull = row.FindControl("txtEditFullName") as System.Web.UI.WebControls.TextBox;
                var txtEmail = row.FindControl("txtEditEmail") as System.Web.UI.WebControls.TextBox;
                var txtPhone = row.FindControl("txtEditPhone") as System.Web.UI.WebControls.TextBox;
                if (txtFull != null) user.FullName = txtFull.Text.Trim();
                if (txtEmail != null) user.Email = txtEmail.Text.Trim();
                if (txtPhone != null) user.PhoneNumber = txtPhone.Text.Trim();
                _userBll.Update(user);
                ltAlert.Text = "<div class=\"alert alert-success\">Student updated.</div>";
                gvStudents.EditIndex = -1;
                LoadStudents();
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

        private void LoadStudents()
        {
            var students = _userBll.GetStudents();
            gvStudents.DataSource = students;
            gvStudents.DataBind();
        }
    }
}
