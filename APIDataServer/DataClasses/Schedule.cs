using System.Collections.Generic;
namespace DataClasses
{
    public class Schedule
    {
        public string ScheduleTitle { get; set; }
        public string ScheduleDates { get; set; }
        public List<Event> Events { get; set; }
    }
}
