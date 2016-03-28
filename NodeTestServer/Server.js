
//CREDIT: http://blog.modulus.io/build-your-first-http-server-in-nodejs
//Lets require/import the HTTP module
var http = require('http');
var dispatcher = require('httpdispatcher');

//Lets define a port we want to listen to
const PORT=44623;

//We need a function which handles requests and send response
function handleRequest(request, response){
	try {
		//log the request on console
		console.log(request.url);
		//Disptach
		dispatcher.dispatch(request, response);
	} catch(err) {
		console.log(err);
	}
}

//Create a server
var server = http.createServer(handleRequest);

//Lets start our server
server.listen(PORT, function(){
    //Callback triggered when server is successfully listening. Hurray!
    console.log("Server listening on: http://localhost:%s", PORT);
});

//For all your static (js/css/images/etc.) set the directory name (relative path).
dispatcher.setStatic('resources');

//A sample GET request
dispatcher.onGet("/api/schedules", function(req, res) {
    res.writeHead(200, {'Content-Type': 'application/json',
	    'Access-Control-Allow-Origin': '*'});

    var schedules =
    [
    	{
    		ScheduleTitle: 'Aviation Preview',
            ScheduleDates: '03/1/2016-03/5/2016',
    		Events:	[
    			{
    				Title: 'Check-in begins',
    				DatesAndTimes: 'Thursday (03/02/2016), 4:15-5:30 pm',
        			Location: 'HHH',
    				Description: 'Spend 1 hour finding out why students from all 50 states and over 20 countries choose LETU.',
    			},
    			{
    				Title: 'someTitle',
    				DatesAndTimes: 'Thursday (03/02/2016), 4:15-5:30 pm',
        			Location: 'someLocation',
    				Description: 'some description',
    			}
    		]
    	},
        {
            ScheduleTitle: 'Engineering Preview',
            ScheduleDates: '03/1/2016-03/5/2016',
            Events:	[
                {
                    Title: 'Stuff',
                    DatesAndTimes: 'Thursday (03/02/2016), 4:15-5:30 pm',
                    Location: 'HHH',
                    Description: 'Spend 1 hour finding out why students from all 50 states and over 20 countries choose LETU.',
                },
                {
                    Title: 'Cool Engineering Stuff',
                    DatesAndTimes: 'Thursday (03/02/2016), 4:15-5:30 pm',
                    Location: 'someLocation',
                    Description: 'some description',
                }
         ]
        }
    ];
    res.end(JSON.stringify(schedules));
});

dispatcher.onGet("/api/googlemapsdata", function(req, res) {
    res.writeHead(200, {'Content-Type': 'application/json',
	    'Access-Control-Allow-Origin': '*'});
   var buildings = [
   { Name: 'Heath Hardwick Hall', Code: 'HHH', Lat: '32.467455', Long: '-94.726533'},
   { Name: 'Thomas Hall', Code: 'THOM', Lat: '32.468040', Long: '-94.724982'},
   { Name: 'Longview Hall', Code: 'LH', Lat: '32.467663', Long: '-94.727333'},
   { Name: 'Glaske',  Code: 'GLSK', Lat: '32.464961', Long: '-94.727397'},
   { Name: 'Machine Tool and Design Lab', Code: 'MTDL', Lat: '32.465211', Long: '-94.725745'},
   { Name: 'Computer Science Projects Lab', Code: 'CSPL', Lat: '32.465236', Long: '-94.725321'},
 ];
    res.end(JSON.stringify(buildings));
});

dispatcher.onGet("/api/activities", function(req, res) {
    res.writeHead(200, {'Content-Type': 'application/json',
	    'Access-Control-Allow-Origin': '*'});
	var activities = [
        {
                Title: "Rube Goldberg Competition",
                Date: "3-5 pm",
                Location: "<a href=\"#\maps\">Belcher Gym (Solheim)</a>",
                Description: "Come watch our Electronics Lab III students as they showcase their Rube Goldberg contraptions!",
                ImageLink: 'https://childcareworldwide.org/blog-images/wp-content/upLoads/2014/04/gml2.jpg'
        },
        {
                Title: "East Texas Symphonic Band",
                Date: "7:30-9 pm",
                Location: "Belcher Auditorium",
                Description: "Join us in the Belcher center for the East Texas Symphonic Band's spring concert.",
                ImageLink: 'http://static.texascommunitymedia.com/cache/de/22/de227d73381fff61bf5bc16bb877df3f_29.jpg'

        },
        {
                Title: "Student Projects",
                Date: "Any Time",
                Location: "Glaske",
                Description: "Join us in the Belcher center for the East Texas Symphonic Band's spring concert.",
                ImageLink: 'https://c2.staticflickr.com/2/1182/534059168_88325d465a_z.jpg?zz=1'
        }
    ];
    res.end(JSON.stringify(activities));
});

dispatcher.onGet("/api/questions", function(req, res) {
    res.writeHead(200, {'Content-Type': 'application/json',
	    'Access-Control-Allow-Origin': '*'});
    var questions = [
        {
            Query: "How do I apply to become a student?",
            Answer: "You can apply to LeTourneau online <a target=\"_blank\" href=\"http://www.letu.edu/_Admissions/Trad_Resources/\">here.</a>",
        },
        {
            Query: "What should I expect from a class visit?",
            Answer: "The professor and students will welcome you, and you'll get to experience LeTourneau's small class sizes and interactive classes."
        },
        {
            Query: "Another question",
            Answer: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
        }
    ];

    res.end(JSON.stringify(questions));
});
