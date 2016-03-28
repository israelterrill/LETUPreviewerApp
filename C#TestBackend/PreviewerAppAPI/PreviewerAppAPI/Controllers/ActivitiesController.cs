using PreviewerAppAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PreviewerAppAPI.Controllers
{
    public class ActivitiesController : ApiController
    {
        public ActivityData[] activities =
        {
            new ActivityData { 
                Title = "Rube Goldberg Competition",
                Date = "3-5 pm",
                Location = "<a href=\"#\\maps\">Belcher Gym (Solheim)</a>",
                Description = "Come watch our Electronics Lab III students as they showcase their Rube Goldberg contraptions!",
                ImageLink = "https://childcareworldwide.org/blog-images/wp-content/upLoads/2014/04/gml2.jpg"
            },
            new ActivityData
            {
                Title = "East Texas Symphonic Band",
                Date = "7:30-9 pm",
                Location = "Belcher Auditorium",
                Description = "Join us in the Belcher center for the East Texas Symphonic Band's spring concert.",
                ImageLink = "http://static.texascommunitymedia.com/cache/de/22/de227d73381fff61bf5bc16bb877df3f_29.jpg"
            },
            new ActivityData
            {
                Title = "Student Projects",
                Date = "Any Time",
                Location = "Glaske",
                Description = "Join us in the Belcher center for the East Texas Symphonic Band's spring concert.",
                ImageLink = "https://c2.staticflickr.com/2/1182/534059168_88325d465a_z.jpg?zz=1"
            }
        };

        public HttpResponseMessage Get()
        {
            var request = Request.CreateResponse(HttpStatusCode.OK, activities);
            request.Headers.Add("Access-Control-Allow-Origin", "*");
            return request;
        }

    }
}
