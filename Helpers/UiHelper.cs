namespace Aarambha.Helpers
{
    public static class UiHelper
    {
        // Picks a subject illustration from Content/images based on the subject name.
        public static string SubjectImage(object name)
        {
            var n = (name as string ?? "").ToLowerInvariant();
            string f = n.Contains("math") ? "math"
                     : n.Contains("sci") ? "science"
                     : n.Contains("engl") ? "english" : "default";
            return "~/Content/images/subject-" + f + ".svg";
        }
    }
}
