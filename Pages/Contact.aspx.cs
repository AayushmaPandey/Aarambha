using System;
using Aarambha.BLL;
using Aarambha.Helpers;
using Aarambha.Models;

namespace Aarambha.Pages
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                var message = new ContactMessage
                {
                    Name = txtName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Subject = txtSubject.Text.Trim(),
                    Message = txtMessage.Text.Trim(),
                    IsRead = false,
                    SubmittedAt = DateTime.UtcNow
                };

                var bll = new ContactMessageBLL();
                bll.Create(message);

                pnlSuccess.Visible = true;
                pnlError.Visible = false;

                txtName.Text = txtEmail.Text = txtSubject.Text = txtMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex);
                pnlSuccess.Visible = false;
                pnlError.Visible = true;
            }
        }
    }
}