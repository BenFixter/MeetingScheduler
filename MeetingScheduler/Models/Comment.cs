namespace MeetingScheduler.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public Meeting Meeting { get; set; }
        public User Author { get; set; }
        public string Message { get; set; }
    }
}