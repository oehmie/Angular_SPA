﻿app.controller("passwortvergessenController", ['$scope', '$http', '$q', '$stateParams', '$state', '$location', '$modal', 'authService',
    function ($scope, $http, $q, $stateParams, $state, $location, $modal, authService) {

        $scope.title = "Passwort vergessen";

        $scope.hasErrors = false;
        $scope.errorText = '';

        $scope.email = '';

        $scope.dialogmessage = '';

        $scope.passwortvergessen = function (form) {
            if (!form.$valid)
                return;
            $scope.hasErrors = false;
            authService.forgotPassword($scope.email)
            .then(function (data) {
                $scope.dialogmessage = data.message;

                $scope.openDialog();

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

        //Dialog anzeigen
        $scope.openDialog = function () {
            var modalInstance = $modal.open({
                templateUrl: 'passwordvergessenModal.html',
                controller: passwordvergessenModalInstanceCtrl,
                windowClass: 'modal fade in',
                size: 'sm',
                resolve: {
                    text: function () {
                        return $scope.dialogmessage;
                    }
                }
            });

            modalInstance.result.then(
                function (result) {
                    $state.go('home');
                },
                function () {
                    //$log.info('Modal dismissed at: ' + new Date());
                }
            );
        };

    }
]);


//Controller für das Dialogfenster
var passwordvergessenModalInstanceCtrl = function ($scope, $modalInstance, text) {

    $scope.title = 'Passwort vergessen';
    $scope.text = text;

    $scope.ok = function () {
        $modalInstance.close('ok');
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};