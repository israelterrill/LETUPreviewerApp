
//CREDIT: http://blog.modulus.io/build-your-first-http-server-in-nodejs
//Lets require/import the HTTP module
var http = require('http');
var dispatcher = require('httpdispatcher');

//Lets define a port we want to listen to
const PORT=8080; 

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
dispatcher.onGet("/schedules", function(req, res) {
    res.writeHead(200, {'Content-Type': 'application/json',
	    'Access-Control-Allow-Origin': '*'});

    var schedules = 
    [
    	{
    		scheduleTitle: 'Aviation Preview',
            scheduleDates: '03/1/2016-03/5/2016',
    		events:	[
    			{
    				title: 'Check-in begins',
    				datesAndTimes: 'Thursday (03/02/2016), 4:15-5:30 pm',
        			location: 'HHH',
    				description: 'Spend 1 hour finding out why students from all 50 states and over 20 countries choose LETU.',
    			},
    			{
    				title: 'someTitle',
    				datesAndTimes: 'Thursday (03/02/2016), 4:15-5:30 pm',
        			location: 'someLocation',
    				description: 'some description',
    			}
    		]
    	},
        {
            scheduleTitle: 'Engineering Preview',
            scheduleDates: '03/1/2016-03/5/2016',
            events:	[
                {
                    title: 'Stuff',
                    datesAndTimes: 'Thursday (03/02/2016), 4:15-5:30 pm',
                    location: 'HHH',
                    description: 'Spend 1 hour finding out why students from all 50 states and over 20 countries choose LETU.',
                },
                {
                    title: 'Cool Engineering Stuff',
                    datesAndTimes: 'Thursday (03/02/2016), 4:15-5:30 pm',
                    location: 'someLocation',
                    description: 'some description',
                }
         ]
        }
    ];
    res.end(JSON.stringify(schedules));
});    

dispatcher.onGet("/maps", function(req, res) {
    res.writeHead(200, {'Content-Type': 'application/json',
	    'Access-Control-Allow-Origin': '*'});
   var buildings = [
   { name: 'Heath Hardwick Hall', code: 'HHH', lat: '32.467455', long: '-94.726533'},
   { name: 'Thomas Hall', code: 'THOM', lat: '32.468040', long: '-94.724982'},
   { name: 'Longview Hall', code: 'LH', lat: '32.467663', long: '-94.727333'},
   { name: 'Glaske',  code: 'GLSK', lat: '32.464961', long: '-94.727397'},
   { name: 'Machine Tool and Design Lab', code: 'MTDL', lat: '32.465211', long: '-94.725745'},
   { name: 'Computer Science Projects Lab', code: 'CSPL', lat: '32.465236', long: '-94.725321'},
 ];
    res.end(JSON.stringify(buildings));
});    

dispatcher.onGet("/activities", function(req, res) {
    res.writeHead(200, {'Content-Type': 'application/json',
	    'Access-Control-Allow-Origin': '*'});
	var activities = [
        {
                title: "Rube Goldberg Competition",
                date: "3-5 pm",
                location: "<a href=\"#\maps\">Belcher Gym (Solheim)</a>",
                description: "Come watch our Electronics Lab III students as they showcase their Rube Goldberg contraptions!",
                imageLink: 'https://childcareworldwide.org/blog-images/wp-content/upLoads/2014/04/gml2.jpg'
        },
        {
                title: "East Texas Symphonic Band",
                date: "7:30-9 pm",
                location: "Belcher Auditorium",
                description: "Join us in the Belcher center for the East Texas Symphonic Band's spring concert.",
                imageLink: 'http://static.texascommunitymedia.com/cache/de/22/de227d73381fff61bf5bc16bb877df3f_29.jpg'

        },
        {
                title: "Student Projects",
                date: "Any Time",
                location: "Glaske",
                description: "Join us in the Belcher center for the East Texas Symphonic Band's spring concert.",
                imageLink: 'https://c2.staticflickr.com/2/1182/534059168_88325d465a_z.jpg?zz=1'
        }
    ];
    res.end(JSON.stringify(activities));
});    

dispatcher.onGet("/help", function(req, res) {
    res.writeHead(200, {'Content-Type': 'application/json',
	    'Access-Control-Allow-Origin': '*'});
    var questions = [
        {
            query: "How do I apply to become a student?",
            answer: "You can apply to LeTourneau online <a target=\"_blank\" href=\"http://www.letu.edu/_Admissions/Trad_Resources/\">here.</a>",
        },
        {
            query: "What should I expect from a class visit?",
            answer: "The professor and students will welcome you, and you'll get to experience LeTourneau's small class sizes and interactive classes."
        },
        {
            query: "Another question",
            answer: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
        }
    ];

    res.end(JSON.stringify(questions));
});    
