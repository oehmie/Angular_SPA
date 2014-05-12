app.controller('navbarController', function ($scope, $location, authService) {
    'use strict';


    $scope.logout = function () {
        authService.logout()
        .then(function (data) {
            $location.path("home");
        }, function (error) {
            // error handling here
            $scope.errorText = error.error_description;
        });
        
    };

    $scope.isAuthenticated = function () {
        return authService.isAuthenticated;
    };

    $scope.getUserName = function () {
        return authService.userName;
    };



    }
);