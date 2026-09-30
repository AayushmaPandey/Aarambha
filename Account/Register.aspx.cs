using System;
using Aarambha.BLL;
using Aarambha.Helpers;
using Aarambha.Models;

namespace Aarambha.Account
{
    public partial class Register : System.Web.UI.Page
    {
        private const int StudentRoleId = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

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
                    RoleID = StudentRoleId,
                    IsActive = true
                };

                var bll = new UserBLL();
                int newUserId = bll.Create(user);

                // Auto-login after successful registration
                Session["UserID"] = newUserId;
                Session["RoleID"] = StudentRoleId;
                Session["FullName"] = user.FullName;

                Response.Redirect("~/Member/Dashboard.aspx");
            }
            catch (ValidationException vex)
            {
                litError.Text = vex.Message;
                pnlError.Visible = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex);
                litError.Text = "Something went wrong while creating your account. Please try again.";
                pnlError.Visible = true;
            }
        }
    }
}
