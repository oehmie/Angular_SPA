app.service('kooperationspartnerService', function ($http, $q) {
    this.getKooperationspartner = function () {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: 'api/kooperationspartner'
        }).
         success(function (data, status, headers, config) {
             deferred.resolve(data)
         }).
         error(function (data, status) {
             deferred.reject(data);
         });

        return deferred.promise;
    }
});