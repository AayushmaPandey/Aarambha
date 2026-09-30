using System;

namespace Aarambha
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Default.aspx", true);
        }
    }
}
