using System;
using System.Data;
using Aarambha.BLL;

namespace Aarambha.Member
{
    public partial class QuizResults : System.Web.UI.Page
    {
        private readonly QuizAttemptBLL _bll = new QuizAttemptBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null || Session["RoleID"] == null || (int)Session["RoleID"] != 2)
                {
                    Response.Redirect("~/Account/Login.aspx");
                    return;
                }

                LoadResults();
            }
        }

        private void LoadResults()
        {
            var userId = Convert.ToInt32(Session["UserID"]);
            var dt = _bll.GetByUserId(userId);
            // convert to DataTable-like structure
            var table = new DataTable();
            table.Columns.Add("QuizTitle");
            table.Columns.Add("Score");
            table.Columns.Add("TotalMarks");
            table.Columns.Add("IsPassed", typeof(bool));
            table.Columns.Add("AttemptedAt", typeof(DateTime));
            foreach (var a in dt)
            {
                var quiz = new QuizBLL().GetById(a.QuizID);
                var row = table.NewRow();
                row["QuizTitle"] = quiz?.Title ?? "-";
                row["Score"] = a.Score;
                row["TotalMarks"] = a.TotalMarks;
                row["IsPassed"] = a.IsPassed;
                row["AttemptedAt"] = a.AttemptedAt;
                table.Rows.Add(row);
            }
            gvResults.DataSource = table;
            gvResults.DataBind();
        }

        // helper for binding pass/fail in the markup
        public string RenderResult(object isPassedObj, object scoreObj, object totalObj)
        {
            try
            {
                bool? isPassed = isPassedObj != null ? (bool?)Convert.ToBoolean(isPassedObj) : null;
                double score = scoreObj != null ? Convert.ToDouble(scoreObj) : 0;
                double total = totalObj != null ? Convert.ToDouble(totalObj) : 0;

                if (isPassed.HasValue)
                {
                    return isPassed.Value ? "<span class='badge bg-success'>Passed</span>" : "<span class='badge bg-danger'>Failed</span>";
                }

                double pct = total == 0 ? 0 : (score / total) * 100.0;
                return pct >= 50 ? "<span class='badge bg-success'>Passed</span>" : "<span class='badge bg-danger'>Failed</span>";
            }
            catch
            {
                return "<span class='badge bg-secondary'>N/A</span>";
            }
        }
    }
}
