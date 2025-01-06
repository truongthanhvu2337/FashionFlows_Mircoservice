namespace PersonalScheduling.Services.Calendar.Domain.Entities
{
    public sealed class Calendars
    {
        public int calendarId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string colorCode { get; set; }
        public string location { get; set; }
        public DateTime createdAt { get; set; }
        //public int calendarType { get; set; }
        
    }
}
