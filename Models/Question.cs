using System;

namespace Aarambha.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public int QuizID { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public int Marks { get; set; }
    }
}
