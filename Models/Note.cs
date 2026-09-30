using System;

namespace Aarambha.Models
{
    public class Note
    {
        public int NoteID { get; set; }
        public int SubjectID { get; set; }
        public string Title { get; set; }
        public string ContentHtml { get; set; }
        public int SortOrder { get; set; }
    }
}
