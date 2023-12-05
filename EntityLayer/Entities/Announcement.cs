
namespace EntityLayer.Entities
{
    public class Announcement
    {
        public int AnnouncementID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date {  get; set; }
        public bool Status {  get; set; }
    }
}