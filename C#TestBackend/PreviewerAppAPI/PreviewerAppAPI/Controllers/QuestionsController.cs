using PreviewerAppAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PreviewerAppAPI.Controllers
{
    public class QuestionsController : ApiController
    {
        public QuestionData[] questions =
        {
            new QuestionData {
                Query = "How do I apply to become a student?",
                Answer = "You can apply to LeTourneau online <a target=\"_blank\" href=\"http://www.letu.edu/_Admissions/Trad_Resources/\">here.</a>",
            },
            new QuestionData
            {
                Query = "What should I expect from a class visit?",
                Answer = "The professor and students will welcome you, and you'll get to experience LeTourneau's small class sizes and interactive classes."
            },
            new QuestionData
            {
                Query = "Another question",
                Answer = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            }
         };

        public HttpResponseMessage Get()
        {
            var request = Request.CreateResponse(HttpStatusCode.OK, questions);
            request.Headers.Add("Access-Control-Allow-Origin", "*");
            return request;
        }

    }
}
