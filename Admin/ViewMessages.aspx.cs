using System;
using Aarambha.BLL;

namespace Aarambha.Admin
{
    public partial class ViewMessages : System.Web.UI.Page
    {
        private readonly ContactMessageBLL _msgBll = new ContactMessageBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckRole();
                LoadMessages();
            }
        }

        private void CheckRole()
        {
            if (Session["UserID"] == null || Session["RoleID"] == null || (int)Session["RoleID"] != 1)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void gvMessages_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Toggle")
            {
                int msgId;
                if (int.TryParse(e.CommandArgument.ToString(), out msgId))
                {
                    try
                    {
                        var msg = _msgBll.GetById(msgId);
                        if (msg.IsRead)
                            _msgBll.MarkAsUnread(msgId);
                        else
                            _msgBll.MarkAsRead(msgId);
                        LoadMessages();
                    }
                    catch { }
                }
            }
            else if (e.CommandName == "Delete")
            {
                int msgId;
                if (int.TryParse(e.CommandArgument.ToString(), out msgId))
                {
                    try
                    {
                        // Add a Delete method to ContactMessageDAL if deleting is required
                        // For now, just refresh
                        LoadMessages();
                    }
                    catch { }
                }
            }
        }

        private void LoadMessages()
        {
            var messages = _msgBll.GetAll();
            gvMessages.DataSource = messages;
            gvMessages.DataBind();
        }
    }
}
