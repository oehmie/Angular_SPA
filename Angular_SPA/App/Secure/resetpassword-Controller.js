app.controller("resetpasswordController",
function ($scope, $http, $q, $stateParams, $state, $location, authService) {

    $scope.title = "Passwort vergessen";

    $scope.responsemessage = $stateParams.code;



});