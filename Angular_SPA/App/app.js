
var app = angular.module('app', ['ui.router', 'ngGrid']);

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
          templateUrl: "/app/home/about.html"
      })
      .state('contact', {
          url: "/contact",
          templateUrl: "/app/home/contact.html"
      })
      .state('kooperationspartner', {
          url: "/kooperationspartner",
          templateUrl: "/app/home/kooperationspartner.html",

      });


});

app.run(function ($rootScope, $state) {
    $rootScope.$on("$stateChangeStart", function (event, toState, toParams, fromState, fromParams) {
        //if (fromState.name == 'kooperationspartner') {
        //    angular.element('[ng-controller=kooperationspartnerController]').remove();
        //}
          //if (toState.authenticate && !AuthService.isAuthenticated()) {
          //    // User isn’t authenticated
          //    $state.transitionTo("login");
          //    event.preventDefault();
          //}
      });
  });



