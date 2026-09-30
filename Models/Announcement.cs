using System;

namespace Aarambha.Models
{
    public class Announcement
    {
        public int AnnouncementID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int PostedBy { get; set; }
        public DateTime PostedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
