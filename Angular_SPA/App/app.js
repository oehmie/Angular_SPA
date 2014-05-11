'use strict';
var app = angular.module('app', ['ui.router', 'ngGrid', 'angularFileUpload' ]);

app.config(function ($stateProvider, $urlRouterProvider) {
    //
    // Alle unbekannten Route auf Home umleiten
    $urlRouterProvider.otherwise("/home");
    //
    // Die bekannten States zusammenbauen
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
        .state('login', {
            url: "/login",
            templateUrl: "/app/secure/login.html",

        })
      .state('fileupload', {
          url: "/fileupload",
          templateUrl: "/app/home/fileupload.html",

      });


});

//HTTP Interceptor für "busy" Spinner erstellen
app.factory('busyHttpInterceptor', function ($q, $rootScope, $log) {

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
    $httpProvider.interceptors.push('busyHttpInterceptor');
});

//Die Directiven für Loader_Show und Loader_hide erstellen
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

//Directive für das automatische ausfüllen von Eingabefeldern bspw Passworten
//Die Werte können nicht validiert werden, da OnChange nicht feuert.
//Lösung: polyfill: https://github.com/tbosch/autofill-event
app.directive('formAutofillFix', function ($timeout) {
    return function (scope, element, attrs) {
        element.prop('method', 'post');
        if (attrs.ngSubmit) {
            $timeout(function () {
                element
                  .unbind('submit')
                  .bind('submit', function (event) {
                      event.preventDefault();
                      element
                        .find('input, textarea, select')
                        .trigger('input')
                        .trigger('change')
                        .trigger('keydown');
                      scope.$apply(attrs.ngSubmit);
                  });
            });
        }
    };
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



