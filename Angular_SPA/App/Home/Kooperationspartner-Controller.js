

app.controller('KooperationspartnerController', function ($scope, kooperationspartnerService) {
    //Perform the initialization

    $scope.kooperationspartner = [];
    kooperationspartnerService.getKooperationspartner()
        .then(function (data) {
            $scope.kooperationspartner = data;
        }, function (error) {
            // error handling here
            $scope.kooperationspartner = error;
        });
});