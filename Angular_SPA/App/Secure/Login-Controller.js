app.controller("loginController",
    function ($scope, $http, $q, $stateParams, $state, $location, authService) {

        $scope.text = "Benutzeranmeldung";

        $scope.userName = "";
        $scope.password = "";

        $scope.hasErrors = false;
        $scope.errorText = '';

        $scope.login = function (data) {
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
                $scope.errorText = error.error_description;
            });
        };

    });