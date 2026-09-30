using System;

namespace Aarambha.Models
{
    public class Resource
    {
        public int ResourceID { get; set; }
        public int NoteID { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
