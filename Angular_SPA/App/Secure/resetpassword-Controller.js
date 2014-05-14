app.controller("resetpasswordController", ['$scope', '$http', '$q', '$stateParams', '$state', '$location', '$modal', 'authService',
   function ($scope, $http, $q, $stateParams, $state, $location, $modal, authService) {

       $scope.title = "Passwort zurücksetzen";

       $scope.dialogmessage = '';

       $scope.email = "";
       $scope.password = "";
       $scope.passwordconfirmation = "";

       $scope.hasErrors = false;
       $scope.errorText = '';

       //Password zurücksetzen
       $scope.resetpassword = function (form) {

           $scope.hasErrors = false;

           if (!form.$valid)
               return;

           if ($scope.password != $scope.passwordconfirmation) {
               $scope.hasErrors = true;
               $scope.errorText = 'Das angegebene Kennwort stimmt nicht mit dem Bestätigungskennwort überein.'
               return;
           }

           //$el.checkAndTriggerAutoFillEvent();
           $scope.hasErrors = false;
           authService.resetPassword($scope.email, $scope.password, $scope.passwordconfirmation, $stateParams.code)
           .then(function (data) {
               //Dialog anzeigen
               $scope.dialogmessage = data.message;

               $scope.openDialog();

           }, function (error) {
               // error handling here
               $scope.hasErrors = true;
               if (error.error_description) {
                   $scope.errorText = error.error_description;
               }
               else {
                   $scope.errorText = JSON.stringify(error);
               }
           });
       };

       //Dialog anzeigen
       $scope.openDialog = function () {
           var modalInstance = $modal.open({
               templateUrl: 'resetpasswordModal.html',
               controller: resetpasswordModalInstanceCtrl,
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
var resetpasswordModalInstanceCtrl = function ($scope, $modalInstance, text) {

    $scope.title = 'Passwort zurücksetzen';
    $scope.text = text;

    $scope.ok = function () {
        $modalInstance.close('ok');
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};