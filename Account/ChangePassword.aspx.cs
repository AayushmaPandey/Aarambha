using System;
using Aarambha.BLL;
using Aarambha.Helpers;

namespace Aarambha.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        private readonly UserBLL _userBll = new UserBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Account/Login.aspx");
                    return;
                }
            }
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                var userId = Convert.ToInt32(Session["UserID"]);
                var current = txtCurrent.Text ?? string.Empty;
                var newPass = txtNew.Text ?? string.Empty;
                var confirm = txtConfirm.Text ?? string.Empty;

                if (string.IsNullOrWhiteSpace(newPass) || newPass.Length < 6)
                    throw new ValidationException("New password must be at least 6 characters.");
                if (newPass != confirm) throw new ValidationException("New password and confirmation do not match.");

                var user = _userBll.GetById(userId);
                if (user == null) throw new ValidationException("User not found.");

                if (!PasswordHelper.VerifyPassword(current, user.PasswordHash, user.PasswordSalt))
                    throw new ValidationException("Current password is incorrect.");

                var salt = PasswordHelper.GenerateSalt();
                var hash = PasswordHelper.HashPassword(newPass, salt);
                _userBll.ChangePassword(userId, hash, salt);

                ltAlert.Text = "<div class=\"alert alert-success\">Password changed successfully.</div>";
            }
            catch (ValidationException ex)
            {
                ltAlert.Text = $"<div class=\"alert alert-danger\">{Server.HtmlEncode(ex.Message)}</div>";
            }
            catch (Exception)
            {
                ltAlert.Text = "<div class=\"alert alert-danger\">An error occurred while changing password.</div>";
            }
        }
    }
}
