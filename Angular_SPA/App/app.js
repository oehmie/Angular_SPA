
var app = angular.module('app', ['ui.router', 'ngGrid', 'angularFileUpload']);

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

      })
      .state('fileupload', {
        url: "/fileupload",
        templateUrl: "/app/home/fileupload.html",

    });


});

app.factory('httpInterceptor', function ($q, $rootScope, $log) {

    var numLoadings = 0;

    return {
        request: function (config) {

            numLoadings++;

            // Show loader
            $rootScope.$broadcast("loader_show");
            return config || $q.when(config)

        },
        response: function (response) {

            if ((--numLoadings) === 0) {
                // Hide loader
                $rootScope.$broadcast("loader_hide");
            }

            return response || $q.when(response);

        },
        responseError: function (response) {

            if (!(--numLoadings)) {
                // Hide loader
                $rootScope.$broadcast("loader_hide");
            }

            return $q.reject(response);
        }
    };
})
.config(function ($httpProvider) {
    $httpProvider.interceptors.push('httpInterceptor');
});

app.directive("loader", function ($rootScope) {
    return function ($scope, element, attrs) {
        $scope.$on("loader_show", function () {
            return element.show();
        });
        return $scope.$on("loader_hide", function () {
            return element.hide();
        });
    };
}
)

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



