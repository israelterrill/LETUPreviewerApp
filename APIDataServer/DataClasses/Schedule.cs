using System.Collections.Generic;
using System.ComponentModel;
namespace DataClasses
{
    public class Schedule
    {
        public string ScheduleTitle { get; set; }
        public string ScheduleDates { get; set; }
        public BindingList<Event> Events { get; set; }
    }
}
