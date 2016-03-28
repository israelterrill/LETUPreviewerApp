using PreviewerAppAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PreviewerAppAPI.Controllers
{
    public class SchedulesController : ApiController
    {
        public ScheduleData[] schedules =
        {
            new  ScheduleData {
                ScheduleTitle = "Aviation Preview",
                ScheduleDates = "03/01/2016-03/05/2016",
                Events = new Event[]
                {
                    new Event { Title = "Check-in begins", DatesAndTimes = "Thursday", Location = "HHH 104", Description = "Get registered for cool free things!" },
                    new Event { Title = "Other Stuff Happens", DatesAndTimes = "Thursday, 1:00PM", Location = "HHH 139", Description = "Oh no!" },
                    new Event { Title = "People Get Excited", DatesAndTimes = "Thurday, 2:00PM", Location = "LH133", Description = "Rube Goldberg Happens" },
                    new Event { Title = "Everyone Goes to Bed", DatesAndTimes = "Thursday, 9:00PM", Location = "GLSK C105", Description = "Crazy stuff happens." },
                }
            },
            new  ScheduleData {
                ScheduleTitle = "Engineering Preview",
                ScheduleDates = "02/01/2016-03/19",
                Events = new Event[]
                {
                    new Event { Title = "Check-in begins", DatesAndTimes = "Thursday", Location = "HHH 104", Description = "Get registered for cool free things!" },
                    new Event { Title = "Everyone Decides to Switch Majors", DatesAndTimes = "Thursday, 1:00PM", Location = "HHH 139", Description = "Oh no!" },
                    new Event { Title = "CS Get More Majors", DatesAndTimes = "Thurday, 2:00PM", Location = "LH133", Description = "Rube Goldberg Happens" },
                    new Event { Title = "Everyone Comes to LETU", DatesAndTimes = "Thursday, 9:00PM", Location = "GLSK C105", Description = "Crazy stuff happens." },
                }
            }
        };

        public HttpResponseMessage Get()
        {
            var request = Request.CreateResponse(HttpStatusCode.OK, schedules);
            request.Headers.Add("Access-Control-Allow-Origin", "*");
            return request;
        }
    }
}
