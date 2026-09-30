using System;

namespace Aarambha.Models
{
    public class Quiz
    {
        public int QuizID { get; set; }
        public int SubjectID { get; set; }
        public string Title { get; set; }
        public int PassMark { get; set; }
        public bool IsActive { get; set; }
    }
}
