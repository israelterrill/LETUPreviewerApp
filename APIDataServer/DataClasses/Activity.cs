using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClasses
{
    public class Activity : Event
    {
        public string ImageLink { get; set; }
        public static Activity FromJson(string xmlStr)
        {
            return null;
        }
    }
}
