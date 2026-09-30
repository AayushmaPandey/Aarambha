using System;

namespace Aarambha.Models
{
    public class ContactMessage
    {
        public int MessageID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
