namespace MeetingScheduler.Models
{
    public class MeetingUser
    {
        public int Id { get; set; }

        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string equipmentNotes { get; set; }
        public string importantNotes { get; set; }
    }
}