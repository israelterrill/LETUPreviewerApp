#API Format

##Map Format
```
[
   { Name: 'Building Name', Code: 'Initials', Lat: 'latitudeCoord', Long: 'longitudeCoord', ImageLink: 'link-to-image'},
   { Name: 'Heath Hardwick Hall', Code: 'HHH', Lat: '32.467455', Long: '-94.726533', ImageLink: 'images/heathhardwickhall.png'},
...
]
```

##Schedule Format
```
[
	//Schedule Object
	{
		ScheduleTitle: 'Aviation Preview',
		ScheduleDates: 'MM/DD/YYYY-MM/DD/YYYY',
		Events:	[
			{ 
				Title: 'someTitle',
				DatesAndTimes: 'Any format like "Thursday" or "MM/DD/YYYY"',
				Location: 'someLocation',
				Description: 'some description'
			}, 
			{ 
				Title: 'someTitle',
				DatesAndTimes: 'Any format like "Thursday" or "MM/DD/YYYY"',
				Location: 'someLocation',
				Description: 'some description'
			}
		]
	},
...
]
```

##Activities Format
```
[
	{
		Title: "EventTitle",
		DatesAndTimes: "date and time",
		Location: "location name and maybe link to maps",
		Description: "a short description of the event",
		ImageLink: "some image URL"
	},
	{
		Title: "Rube Goldberg Competition",
		Date: "3-5 pm",
		Location: "<a href=\"#\maps\">Belcher Gym (Solheim)</a>",
		Description: "Come watch our Electronics Lab III students as they showcase their Rube Goldberg contraptions!"
		ImageLink: "https://c2.staticflickr.com/2/1182/534059168_88325d465a_z.jpg?zz=1"	
}
]
```

##Question Format
```
[
        {
            Question: "question",
            Answer: "The answer to the question",
        },
        {
            Question: "How do I apply to become a student?",
            Answer: "You can apply to LeTourneau online <a target=\"_blank\" href=\"http://www.letu.edu/_Admissions/Trad_Resources/\">here.</a>",
        }
...
]
```
