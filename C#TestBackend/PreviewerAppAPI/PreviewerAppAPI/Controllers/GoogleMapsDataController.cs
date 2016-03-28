using PreviewerAppAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace PreviewerAppAPI.Controllers
{
    public class GoogleMapsDataController : ApiController
    {
        public GoogleMapsData[] googleMapsLocations =
        {
            new GoogleMapsData { Name = "Heath Hardwick Hall", Code = "HHH", Lat = "32.467455", Long = "-94.726533" }
        };

        public HttpResponseMessage Get()
        {
            var request = Request.CreateResponse(HttpStatusCode.OK, googleMapsLocations);
            request.Headers.Add("Access-Control-Allow-Origin", "*");
            return request;
        }
    }
}
