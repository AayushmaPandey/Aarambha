using System;
using System.Linq;
using Aarambha.BLL;

namespace Aarambha.Member
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private readonly SubjectBLL _subjectBll = new SubjectBLL();
        private readonly NoteBLL _noteBll = new NoteBLL();
        private readonly QuizAttemptBLL _quizAttemptBll = new QuizAttemptBLL();
        private readonly AnnouncementBLL _annBll = new AnnouncementBLL();

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
                if (role != 2)
                {
                    // not a student
                    Response.Redirect("~/Account/Login.aspx");
                    return;
                }

                ltName.Text = Server.HtmlEncode(Convert.ToString(Session["FullName"]));
                LoadStats();
            }
        }

        private void LoadStats()
        {
            try
            {
                // Subjects available (active)
                var subjects = _subjectBll.GetAll();
                var subjectCount = subjects?.Count(s => s.IsActive) ?? 0;
                ltSubjects.Text = subjectCount.ToString();

                // Notes total
                var notes = _noteBll.GetAll();
                ltNotes.Text = (notes?.Count ?? 0).ToString();

                // Quizzes attempted by this student
                var userId = Convert.ToInt32(Session["UserID"]);
                var attempts = _quizAttemptBll.GetByUserId(userId);
                ltQuizzesAttempted.Text = (attempts?.Count ?? 0).ToString();

                // Latest announcement
                var anns = _annBll.GetActive();
                if (anns != null && anns.Count > 0)
                {
                    var latest = anns[0];
                    ltLatestTitle.Text = Server.HtmlEncode(latest.Title);
                    ltLatestDate.Text = latest.PostedAt.ToString("g");
                }
            }
            catch { }
        }
    }
}
