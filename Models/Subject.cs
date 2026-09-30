using System;

namespace Aarambha.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        public bool IsActive { get; set; }
    }
}
