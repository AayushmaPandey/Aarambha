using System;
using Aarambha.BLL;
using Aarambha.Helpers;
using Aarambha.Models;

namespace Aarambha.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                var authBll = new AuthBLL();
                User user = authBll.Login(txtUsername.Text.Trim(), txtPassword.Text);

                Session["UserID"] = user.UserID;
                Session["RoleID"] = user.RoleID;
                Session["FullName"] = user.FullName;

                Response.Redirect(user.RoleID == 1
                    ? "~/Admin/Dashboard.aspx"
                    : "~/Member/Dashboard.aspx");
            }
            catch (ValidationException vex)
            {
                litError.Text = vex.Message;
                pnlError.Visible = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex);
                litError.Text = "Something went wrong. Please try again.";
                pnlError.Visible = true;
            }
        }
    }
}