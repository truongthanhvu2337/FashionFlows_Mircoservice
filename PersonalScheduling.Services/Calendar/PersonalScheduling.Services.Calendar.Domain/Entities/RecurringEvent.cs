namespace PersonalScheduling.Services.Calendar.Domain.Entities
{
    public sealed class RecurringEvent
    {
        public int reccurringEventId { get; set; }
        public string frequency { get; set; }
        public int interval { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime createdAt { get; set; }
    }
}
