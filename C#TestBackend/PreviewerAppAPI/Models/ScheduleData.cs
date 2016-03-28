using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreviewerAppAPI.Models
{
    public class Event
    {
        public String Title { get; set; }
        public String DatesAndTimes { get; set; }
        public String Location { get; set; }
        public String Description { get; set; }
    }
    public class ScheduleData
    {
        public String ScheduleTitle { get; set; }
        public String ScheduleDates {get; set;}
        public Event[] Events { get; set; }
    }
}