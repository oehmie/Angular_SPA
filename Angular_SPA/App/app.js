
var app = angular.module('app', ['ui.router']);

app.config(function ($stateProvider, $urlRouterProvider) {
    //
    // For any unmatched url, redirect to /home
    $urlRouterProvider.otherwise("/home");
    //
    // Now set up the states
    $stateProvider
      .state('home', {
          url: "/home",
          templateUrl: "/app/home/home.html"
      })
      .state('about', {
          url: "/about",
          templateUrl: "/app/home/about.html",
      })
      .state('contact', {
            url: "/contact",
            templateUrl: "/app/home/contact.html",
        })
      .state('state2', {
          url: "/state2",
          templateUrl: "partials/state2.html"
      })
      .state('state2.list', {
          url: "/list",
          templateUrl: "partials/state2.list.html",
          controller: function ($scope) {
              $scope.things = ["A", "Set", "Of", "Things"];
          }
      })
});

