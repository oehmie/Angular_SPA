﻿'use strict';
var app = angular.module('app', ['ui.router', 'ui.bootstrap', 'ngGrid', 'angularFileUpload'])

.config(['$stateProvider', '$urlRouterProvider',
   function ($stateProvider, $urlRouterProvider) {

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
         .state('passwortvergessen', {
             url: "/passwortvergessen",
             templateUrl: "/app/secure/passwortvergessen.html",
         })
         .state('resetpassword', {
             url: "/resetpassword?code",
             templateUrl: "/app/secure/resetpassword.html",
         })
         .state('fileupload', {
             url: "/fileupload",
             templateUrl: "/app/home/fileupload.html",
         });

       // Alle unbekannten Route auf Home umleiten
       $urlRouterProvider.otherwise("/home");


   }
])



//HTTP Interceptor für "busy" Spinner erstellen
.factory('busyHttpInterceptor', ['$q', '$rootScope', '$log',
    function ($q, $rootScope, $log) {

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
    }
])
.config(['$httpProvider',
    function ($httpProvider) {
        $httpProvider.interceptors.push('busyHttpInterceptor');
    }
])

//Die Directiven für Loader_Show und Loader_hide erstellen
.directive("loader", ['$rootScope',
    function ($rootScope) {
        return function ($scope, element, attrs) {
            $scope.$on("loader_show", function () {
                return element.show();
            });
            return $scope.$on("loader_hide", function () {
                return element.hide();
            });
        };
    }
])

//Directive für das automatische ausfüllen von Eingabefeldern bspw Passworten
//Die Werte können nicht validiert werden, da OnChange nicht feuert.
//Lösung: polyfill: https://github.com/tbosch/autofill-event
.directive('formAutofillFix', ['$timeout',
    function ($timeout) {
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
    }
])






.run(['$rootScope', '$state', '$stateParams',
    function ($rootScope, $state, $stateParams) {

        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;

        $rootScope.$on('$stateChangeStart',
                   function (event, toState, toParams, fromState, fromParams) {
                       console.log('changing state from', fromState.name, 'to', toState.name);
                   });
        $rootScope.$on('$stateChangeSuccess',
        function (event, toState, toParams, fromState, fromParams) {
            console.log('changed state from', fromState.name, 'to', toState.name);
        });
        $rootScope.$on('$stateNotFound', function (unfoundState) {
            console.log('tried to change to state', unfoundState.to,
            'but that state is not known');
        });
        $rootScope.$on('$stateChangeError',
        function (event, toState, toParams, fromState, fromParams, error) {
            console.log(error, 'changing state from',
            fromState.name, 'to', toState.name);
        });



    }
]);



