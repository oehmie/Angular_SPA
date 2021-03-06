﻿app.controller("loginController", ['$scope', '$http', '$q', '$stateParams', '$state', '$location', 'authService',
    function ($scope, $http, $q, $stateParams, $state, $location, authService) {

        $scope.title = "Benutzeranmeldung";

        $scope.hasErrors = false;
        $scope.errorText = '';

        $scope.username = "";
        $scope.password = "";

        $scope.login = function (form) {
            if (!form.$valid)
                return;
            //$el.checkAndTriggerAutoFillEvent();
            $scope.hasErrors = false;
            authService.login($scope.username, $scope.password)
            .then(function (data) {
                //Weiterleitung auf die gewünschte Seite
                var redirect = $location.search();
                if (redirect.returnTo)
                    $location.path(redirect.returnTo);
            }, function (error) {
                // error handling here
                $scope.hasErrors = true;
                if (error.error_description) {
                    $scope.errorText = error.error_description;
                }
                else {
                    $scope.errorText = angular.fromJson(error);
                }
            });
        };

    }
]);