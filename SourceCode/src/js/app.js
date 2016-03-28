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

//CHANGE THIS TO CORRECT BASE!
var httpRequestBase = 'http://localhost:44623/api';

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

previewerApp.controller('MapsController', function($scope, $http, NgMap) {

 $http({
   method: 'GET',
   url: httpRequestBase + '/googlemapsdata',
 }).then(function successCallback(response) {
     $scope.buildings = response.data;
     $scope.building = {
       selected: $scope.buildings[0]
     };
     $scope.markers =[];
     $scope.not_checked = true;
     $scope.geoEnabled = true;
     NgMap.getMap().then(function(map) {
       map.getStreetView().setVisible(false);
           var options = {
               enableHighAccuracy: true
            };
           navigator.geolocation.getCurrentPosition(function(pos) {
               var currMarker = new google.maps.Marker({
                   map: map,
                   position: new google.maps.LatLng(pos.coords.latitude, pos.coords.longitude),
                   title: "You Are Here"
               });
               var currentWindow = new google.maps.InfoWindow({
                   content: '<h4 id="firstHeading" class="firstHeading">You Are Here</h4>'
               });

               currMarker.addListener('click', function() {
                 currentWindow.open(map, currMarker);
               });
               currentWindow.open(map, currMarker);
               $scope.markers.push(currMarker);
               $scope.currentPosition = currMarker.position;
               map.panTo(currMarker.position);
               $scope.geoEnabled = true;
           },
           function(error) {
                   $scope.geoEnabled = false;
                   alert('Unable to get location: ' + error.message
                   + '\nPlease select a location from the dropdown to get started');
                   var defaultBuilding = $scope.buildings[0];
                   window.console.log(map);
                   window.console.log(defaultBuilding);
                   var pos = new google.maps.LatLng(defaultBuilding.Lat, defaultBuilding.Long);
                   map.panTo(pos);
           }, options);

       $scope.onSelected = function(building, $select) {
         while($scope.markers.length > 0) {
             $scope.markers.pop().setMap(null);
         }

         var infowindow = new google.maps.InfoWindow({
             content: '<h4 id="firstHeading" class="firstHeading">'+building.Name+'</h4>'
         });

         var marker = new google.maps.Marker({
             map: map,
             position: new google.maps.LatLng(building.Lat, building.Long),
             title: building.Name
         });
         marker.addListener('click', function() {
           infowindow.open(map, marker);
         });
         $scope.markers.push(marker);
         //In case no geolocation
           var ll = new google.maps.LatLng(building.Lat, building.Long);
           map.panTo(ll);
       if ($scope.geoEnabled === true) {
           var options = {
                           enableHighAccuracy: true
                       };

           navigator.geolocation.getCurrentPosition(function(pos) {
                           var currMarker = new google.maps.Marker({
                               map: map,
                               position: new google.maps.LatLng(pos.coords.latitude, pos.coords.longitude),
                               title: "You Are Here"
                           });
                           var currentWindow = new google.maps.InfoWindow({
                               content: '<h4 id="firstHeading" class="firstHeading">You Are Here</h4>'
                           });

                           currMarker.addListener('click', function() {
                             currentWindow.open(map, currMarker);
                           });
                           currentWindow.open(map, currMarker);
                           $scope.markers.push(currMarker);
                           $scope.currentPosition = currMarker.position;
                           map.panTo(currMarker.position);
                       },
                       function(error) {
                           alert('Unable to get location: ' + error.message);
                       }, options);
                   }
           document.activeElement.blur();
           $select.search="";
         };
     });

     $scope.$on('$locationChangeStart', function() {
         while($scope.markers.length > 0) {
           $scope.markers.pop().setMap(null);
         }
     });


     }, function errorCallback(response) {
         window.console.log(response);
         window.console.log("ERROR");
     });

     $scope.disabled = undefined;

     $scope.disable = function() {
      $scope.disabled = true;
     };
     $scope.enable = function() {
      $scope.disabled = false;
     };

     $scope.coordinates = function() {
       return {
        'margin':'0px 0px',
       };
     };
});

previewerApp.controller('ScheduleController', function($scope, $location, $http) {
    $http({
      method: 'GET',
      url: httpRequestBase + '/schedules',
    }).then(function successCallback(response) {
        $scope.schedules = response.data;
        $scope.schedule = $scope.schedules[0];
        for (var i = 0; i < $scope.schedule.Events.length; i++) {
            $scope.schedule.Events[i].id = i;
    }
      $scope.onSelected = function(schedule, $select) {
        $scope.schedule = schedule;
        for (var i = 0; i < $scope.schedule.Events.length; i++) {
            $scope.schedule.Events[i].id = i;
        }
        document.activeElement.blur();
        $select.search="";
      };
      var numberOfPanels = $scope.schedule.Events.length;
      $scope.counts = Array.apply(1, {length: numberOfPanels}).map(Number.call, Number);
    }, function errorCallback(response) {
        window.console.log(response);
    window.console.log("ERROR");
    });
});

previewerApp.controller('ActivitiesController', function($scope, $http, $location) {
  $http({
    method: 'GET',
    url: httpRequestBase + '/activities',
  }).then(function successCallback(response) {
      $scope.activities = response.data;
      for(var i = 0; i < $scope.activities.length; i++) {
          $scope.activities[i].id = i;
      }
    var numberOfPanels = $scope.activities.length; //can be set dynamically
    $scope.counts = Array.apply(1, {length: numberOfPanels}).map(Number.call, Number);
  }, function errorCallback(response) {
      window.console.log(response);
      window.console.log("ERROR");
  });
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

previewerApp.controller('HelpController', function($scope, $http, $location) {
    $http({
      method: 'GET',
      url: httpRequestBase + '/questions',
    }).then(function successCallback(response) {
        $scope.questions = response.data;
        var numberOfPanels = $scope.questions.length;
        for(var i = 0; i < numberOfPanels; i++) {
          $scope.questions[i].id = i;
        }
        $scope.counts = Array.apply(1, {length: numberOfPanels}).map(Number.call, Number);
    }, function errorCallback(response) {
        window.console.log(response);
        window.console.log("ERROR");
    });
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
