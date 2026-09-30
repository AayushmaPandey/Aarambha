using System;
using System.Collections.Generic;
using Aarambha.BLL;
using Aarambha.Helpers;
using Aarambha.Models;

namespace Aarambha.Pages
{
    public partial class Subjects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSubjects(null);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindSubjects(txtSearch.Text.Trim());
        }

        private void BindSubjects(string keyword)
        {
            try
            {
                var bll = new SubjectBLL();

                List<Subject> subjects = string.IsNullOrEmpty(keyword)
                    ? bll.GetAll()
                    : bll.Search(keyword);

                rptSubjects.DataSource = subjects;
                rptSubjects.DataBind();

                phEmpty.Visible = subjects.Count == 0;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex);
                pnlError.Visible = true;
                litError.Text = "We couldn't load subjects right now. Please try again in a moment.";
                phEmpty.Visible = false;
            }
        }
    }
}