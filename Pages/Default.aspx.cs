using System;
using System.Collections.Generic;
using System.Linq;
using Aarambha.BLL;
using Aarambha.Helpers;
using Aarambha.Models;

namespace Aarambha.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFeaturedSubjects();
            }
        }

        private void BindFeaturedSubjects()
        {
            List<Subject> subjects;

            try
            {
                subjects = new SubjectBLL().GetAll().Take(3).ToList();
                if (subjects.Count == 0) subjects = FallbackSubjects();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex);
                subjects = FallbackSubjects();
            }

            rptFeaturedSubjects.DataSource = subjects;
            rptFeaturedSubjects.DataBind();
        }

        private List<Subject> FallbackSubjects()
        {
            // Shown only if the database can't be reached yet, so the homepage
            // still looks complete while that gets sorted out.
            return new List<Subject>
            {
                new Subject { SubjectName = "English", Description = "Grade 8 English language and literature" },
                new Subject { SubjectName = "Mathematics", Description = "Grade 8 Mathematics" },
                new Subject { SubjectName = "Science", Description = "Grade 8 General Science" }
            };
        }
    }
}