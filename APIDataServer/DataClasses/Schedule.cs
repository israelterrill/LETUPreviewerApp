using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClasses
{
    public class Schedule
    {
        public string ScheduleTitle;
        public string ScheduleDates;
        public Event[] Events;

        public static Schedule FromJson(string xmlStr)
        {
            return null;
        }
    }
}
