app.controller("passwortvergessenController",
    function ($scope, $http, $q, $stateParams, $state, $location, authService) {

        $scope.title = "Passwort vergessen";

        $scope.hasErrors = false;
        $scope.errorText = '';
        
        $scope.email = '';

        $scope.passwortvergessen = function (form) {
            if (!form.$valid)
                return;
            $scope.hasErrors = false;
            authService.forgotPassword($scope.email)
            .then(function (data) {
                $scope.responsemessage = data.message;
                $state.go('passwortvergessen.response');
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

        }
    });