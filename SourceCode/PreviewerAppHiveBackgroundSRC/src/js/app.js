var previewerApp = angular.module('PreviewerApp', [
  'ngRoute',
  'mobile-angular-ui',
  'PreviewerApp.controllers.Main',
  'ngMap',
  'ngSanitize',
  'ui.select'
])

.config(function($routeProvider) {
  $routeProvider.when('/', {templateUrl:'home.html', controller: 'HomeController'})
  .when('/maps', {templateUrl:'maps.html', reloadOnSearch: false})
  .when('/schedule', {templateUrl:'schedule.html', reloadOnSearch: false})
  .when('/activities', {templateUrl:'activities.html', reloadOnSearch: false})
  .when('/dining', {templateUrl:'dining.html', reloadOnSearch: false})
  .when('/help', {templateUrl:'help.html', reloadOnSearch: false});
});

previewerApp.controller('MainController', function($scope, $location) {

  $scope.$on("$routeChangeSuccess", function($currentRoute, $previousRoute) {
    switch($location.path()) {
        case '/':
          $scope.location = 'Home';
          break;
        case '/maps':
          $scope.location = 'Maps';
          break;
        case '/schedule':
          $scope.location = 'Schedule';
          break;
        case '/activities':
          $scope.location = 'Activities';
          break;
        case '/dining':
          $scope.location = 'Dining';
          break;
        case '/help':
          $scope.location = 'Help';
          break;
      }
  });

  $scope.goBack = function () {
    $location.path('/');
  };

  $scope.notHome = function() {
    return $scope.location != 'Home';
  };
});

previewerApp.controller('HomeController', function($scope, $location) {
  $scope.goToMaps = function () {
    $location.path('/maps');
  }
  $scope.goToSchedule = function () {
    $location.path('/schedule');
  }
  $scope.goToActivities = function () {
    $location.path('/activities');
  }
  $scope.goToDining = function () {
    $location.path('/dining');
  }
  $scope.goToHelp = function () {
    $location.path('/help');
  }
});

previewerApp.controller('MapsController', function($scope, NgMap) {
  $scope.buildings = [
   { name: 'Heath Hardwick Hall', code: 'HHH', lat: '32.467455', long: '-94.726533'},
   { name: 'Thomas Hall', code: 'THOM', lat: '32.468040', long: '-94.724982'},
   { name: 'Longview Hall', code: 'LH', lat: '32.467663', long: '-94.727333'},
   { name: 'Glaske',  code: 'GLSK', lat: '32.464961', long: '-94.727397'},
   { name: 'Machine Tool and Design Lab', code: 'MTDL', lat: '32.465211', long: '-94.725745'},
   { name: 'Computer Science Projects Lab', code: 'CSPL', lat: '32.465236', long: '-94.725321'},
 ];
  $scope.building = {
    selected: $scope.buildings[0]
  };
  $scope.markers =[];
  NgMap.getMap().then(function(map) {
    map.getStreetView().setVisible(false);
    $scope.onSelected = function(building, $select) {
      while($scope.markers.length > 0) {
        $scope.markers.pop().setMap(null);
      }

      var infowindow = new google.maps.InfoWindow({
          content: '<h1 id="firstHeading" class="firstHeading">'+building.name+'</h1>'
      });

      var marker = new google.maps.Marker({
          map: map,
          position: new google.maps.LatLng(building.lat, building.long),
          title: building.name
      });
      marker.addListener('click', function() {
        infowindow.open(map, marker);
      });
      $scope.markers.push(marker);

        var ll = new google.maps.LatLng(building.lat, building.long);
        map.panTo(ll);

        document.activeElement.blur();
        $select.search="";
      };
  });

  $scope.$on('$locationChangeStart', function() {
      while($scope.markers.length > 0) {
        $scope.markers.pop().setMap(null);
      }
  });

  $scope.coordinates = function() {
    return {
	    'margin':'0px 0px',
    };
  };
  $scope.disabled = undefined;

  $scope.enable = function() {
   $scope.disabled = false;
 };

  $scope.disable = function() {
   $scope.disabled = true;
 };

});

previewerApp.controller('ScheduleController', function($scope, $location) {
  $scope.schedules = [
  //Schedule 1
  {
    scheduleTitle: 'Preview Day',
    scheduleDates: '03/1/2016-03/5/2016',
    titles: [
      "Check-in begins",
      "Campus tours",
      "Department overviews",
      "Parent/student chat with enrollment officers",
      "Welcome dinner",
      "Breakfast with admissions",
      "More events...",
      "Check-in begins",
      "Campus tours",
      "Department overviews",
      "Parent/student chat with enrollment officers",
      "Welcome dinner",
      "Breakfast with admissions",
      "More events..."
    ],
    dates: [
      "4 pm Thursday",
      "4:15-5:30 pm Thursday",
      "5:30 pm Thursday",
      "6:30 pm Thursday",
      "7 pm Thursday",
      "8 am Friday",
      "Date goes here",
      "4 pm Thursday",
      "4:15-5:30 pm Thursday",
      "5:30 pm Thursday",
      "6:30 pm Thursday",
      "7 pm Thursday",
      "8 am Friday",
      "Date goes here",
    ],
    locations: [
      "Student Center [AFSC]",
      "South Hall lobby",
      "MSC-3 Little Theater",
      "MSC-3 Little Theater",
      "Corner Cafe",
      "Corner Cafe",
      "Glaske C121",
      "Student Center [AFSC]",
      "South Hall lobby",
      "MSC-3 Little Theater",
      "MSC-3 Little Theater",
      "Corner Cafe",
      "Corner Cafe",
      "Glaske C121",
    ],
    descriptions: [
      "Check in, get settled in, and get ready for a great visit!",
      "Spend 1 hour finding out why students from all 50 states and over 20 countries choose LETU.",
      "Learn about the different departments and programs LETU offers.",
      "Find out how to apply to become a student at LETU and get answers to questions about student life, chapel, and academics at LETU.",
      "Enjoy dinner at our Corner Cafe with admissions officers and current students.",
      "Enjoy breakfast at the Corner Cafe with admissions officers and current students.",
      "The event description would go here if this were a real event.",
      "Check in, get settled in, and get ready for a great visit!",
      "Spend 1 hour finding out why students from all 50 states and over 20 countries choose LETU.",
      "Learn about the different departments and programs LETU offers.",
      "Find out how to apply to become a student at LETU and get answers to questions about student life, chapel, and academics at LETU.",
      "Enjoy dinner at our Corner Cafe with admissions officers and current students.",
      "Enjoy breakfast at the Corner Cafe with admissions officers and current students.",
      "The event description would go here if this were a real event."
    ]
  },
  //Schedule 2
  {
    scheduleTitle: 'LETU Fun and Relaxation Day',
    scheduleDates: '05/1/2016-05/5/2016',
    titles: [
      "Sit Down and Rest",
      "Do Something with Someone Special",
      "Impress Dr. Baas",
      "Make Coffee",
      "Think about Computers...",
      "Go Exercise",
      "More Events..."
    ],
    dates: [
      "Whenever you want",
      "4:15-5:30 pm Thursday",
      "5:30 pm Thursday",
      "6:30 pm Thursday",
      "7 pm Thursday",
      "8 am Friday",
      "Date goes here",
    ],
    locations: [
      "Wherever you want",
      "South Hall lobby",
      "MSC-3 Little Theater",
      "MSC-3 Little Theater",
      "Corner Cafe",
      "Corner Cafe",
      "Glaske C121",
    ],
    descriptions: [
      "Be happy!",
      "Spend 1 hour finding out why students from all 50 states and over 20 countries choose LETU.",
      "Learn about the different departments and programs LETU offers.",
      "Find out how to apply to become a student at LETU and get answers to questions about student life, chapel, and academics at LETU.",
      "Enjoy dinner at our Corner Cafe with admissions officers and current students.",
      "Enjoy breakfast at the Corner Cafe with admissions officers and current students.",
      "The event description would go here if this were a real event."
    ]
  }
];
  $scope.schedule = $scope.schedules[0];
  $scope.events = [];
  for (var i = 0; i < $scope.schedule.titles.length; i++) {
      $scope.events.push({
      title: $scope.schedule.titles[i],
      dates: $scope.schedule.dates[i],
      location: $scope.schedule.locations[i],
      description: $scope.schedule.descriptions[i],
      id: i
    });
  }

  $scope.onSelected = function(schedule, $select) {
    $scope.schedule = schedule;
    $scope.events = [];
    for (var i = 0; i < $scope.schedule.titles.length; i++) {
        $scope.events.push({
        title: $scope.schedule.titles[i],
        dates: $scope.schedule.dates[i],
        location: $scope.schedule.locations[i],
        description: $scope.schedule.descriptions[i],
        id: i
      });
    }
    document.activeElement.blur();
    $select.search="";
  };
  var numberOfPanels = 7; //can be set dynamically
  $scope.counts = Array.apply(1, {length: numberOfPanels}).map(Number.call, Number)
});

previewerApp.controller('ActivitiesController', function($scope, $location) {
  $scope.titles = [
    "Rube Goldberg Competition",
    "East Texas Symphonic Band",
    "Student Projects",
  ];
  $scope.dates = [
    "3-5 pm",
    "7:30-9 pm",
    "Any time",
  ];
  $scope.locations = [
    "<a href=\"#\maps\">Belcher Gym (Solheim)</a>",
    "Belcher Auditorium",
    "Glaske",
  ];
  $scope.descriptions = [
    "Come watch our Electronics Lab III students as they showcase their Rube Goldberg contraptions!",
    "Join us in the Belcher center for the East Texas Symphonic Band's spring concert.",
    "Walk down Glaske's halls and check out students' interactive class projects, from robots to model elevators.",
  ];

  var numberOfPanels = 3; //can be set dynamically
  $scope.counts = Array.apply(1, {length: numberOfPanels}).map(Number.call, Number)
});

previewerApp.controller('DiningController', function($scope, NgMap) {
  $scope.markers=[];
  NgMap.getMap().then(function(map) {
    map.getStreetView().setVisible(false);
    var infowindow1 = new google.maps.InfoWindow({
        content: '<h1 id="firstHeading" class="firstHeading">Corner Cafe</h1>'
    });
    var marker1 = new google.maps.Marker({
        map: map,
        position: new google.maps.LatLng(32.465237, -94.723749),
        title: 'Corner Cafe'
    });

    var infowindow2 = new google.maps.InfoWindow({
        content: '<h1 id="firstHeading" class="firstHeading">The Hive</h1>'
    });
    var marker2 = new google.maps.Marker({
        map: map,
        position: new google.maps.LatLng(32.466310, -94.726485),
        title: 'The Hive'
    });

    $scope.markers.push(marker1);
    marker1.addListener('click', function() {
      infowindow1.open(map, marker1);
    });
    $scope.markers.push(marker2);
    marker2.addListener('click', function() {
      infowindow2.open(map, marker2);
    });
  });

  $scope.$on('$locationChangeStart', function() {
      while($scope.markers.length > 0) {
        $scope.markers.pop().setMap(null);
      }
  });

  $scope.titles = [
    "Menu",
    "Hours",
  ];

  $scope.content = [
    "<a href=\"http:\/\/letu.cafebonappetit.com\" target=\"_blank\">Click here</a> to view today's menu.",
    "<b>Sunday</b><ul><li>Breakfast:<br>Cafe 8 - 9:15 am<br></li><li>Lunch:<br>Cafe 11:30 am - 1:30 pm</li></ul><b>Monday, Wednesday, Friday:</b><ul><li>Breakfast:<br>Cafe 6:30 - 9:30 am<br>Hive 6:30 - 11:30 am</li><li>Lunch:<br>Cafe 11:30 am - 1:30 pm<br>Hive 11:30 am - 3 pm</li><li>Midday:<br>Hive 3 - 6:30 pm</li><li>Dinner:<br>Cafe 4:45 - 7 pm<br>Hive 6:30 - 11 pm</li></ul><b>Tuesday, Thursday:</b><ul><li>Breakfast:<br>Cafe 6:30 - 9:30 am<br>Hive 6:30 - 10:50 am</li><li>Lunch:<br>Cafe 10:50 am - 1:30 pm<br>Hive 11:30 am - 3 pm</li><li>Midday:<br>Hive 3 - 6:30 pm</li><li>Dinner:<br>Cafe 4:45 - 7 pm<br>Hive 6:30 - 11 pm</li></ul><b>Saturday:</b><ul><li>Brunch:<br>Cafe 11:30 am - 1:30 pm</li><li>Dinner:<br>Cafe 4:45 - 6 pm<br>Hive 5:30 - 11 pm</li></ul>",
  ];

  var numberOfPanels = 2;
  $scope.counts = Array.apply(1, {length: numberOfPanels}).map(Number.call, Number);

});

previewerApp.controller('HelpController', function($scope, $location) {
  $scope.questionTitles = [
    "I have some extra time. What should I do?",
    "How do I apply to become a student?",
    "What should I expect from a class visit?",
    "When do I meet my host?",
    "Another question",
    "Yet another question",
  ];

  $scope.questionBodies = [
    "Check out our <a href=\"#/activities\">events listing</a>, or just ask some current students about their experience.",
    "You can apply to LeTourneau online <a href=\"http://www.letu.edu/_Admissions/Trad_Resources/\">here.</a>",
    "The professor and students will welcome you, and you'll get to experience LeTourneau's small class sizes and interactive classes.",
    "The number of the room where you're staying is on your nametag. Your host may have classes, but they will welcome you as soon as they can.",
    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
    "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
  ];

  var numberOfPanels = 6; //can be set dynamically
  $scope.counts = Array.apply(1, {length: numberOfPanels}).map(Number.call, Number);
});

previewerApp.filter('unsafe', function($sce) {
    return function(val) {
        return $sce.trustAsHtml(val);
    };
});
//From Code for ui-angular https://github.com/angular-ui/ui-select/blob/3fac88cfad0ad2369c567142eadba52bdb7998b1/examples/demo.js
previewerApp.filter('propsFilter', function() {
  return function(items, props) {
    var out = [];

    if (angular.isArray(items)) {
      items.forEach(function(item) {
        var itemMatches = false;

        var keys = Object.keys(props);
        for (var i = 0; i < keys.length; i++) {
          var prop = keys[i];
          var text = props[prop].toLowerCase();
          if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
            itemMatches = true;
            break;
          }
        }

        if (itemMatches) {
          out.push(item);
        }
      });
    } else {
      // Let the output be the input untouched
      out = items;
    }

    return out;
  }
});
