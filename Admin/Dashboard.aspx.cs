using System;
using Aarambha.BLL;

namespace Aarambha.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private readonly UserBLL _userBll = new UserBLL();
        private readonly SubjectBLL _subjectBll = new SubjectBLL();
        private readonly QuizBLL _quizBll = new QuizBLL();
        private readonly ContactMessageBLL _msgBll = new ContactMessageBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null || Session["RoleID"] == null)
                {
                    Response.Redirect("~/Account/Login.aspx");
                    return;
                }

                var role = (int)Session["RoleID"];
                if (role != 1)
                {
                    Response.Redirect("~/Account/Login.aspx");
                    return;
                }

                try
                {
                    ltStudents.Text = _userBll.GetStudents().Count.ToString();
                    ltSubjects.Text = _subjectBll.GetAll().Count.ToString();
                    ltQuizzes.Text = _quizBll.GetAll().Count.ToString();
                    var unread = _msgBll.GetAll().FindAll(x => !x.IsRead).Count;
                    ltMessages.Text = unread.ToString();
                }
                catch
                {
                    // silently fail
                }
            }
        }
    }
}
