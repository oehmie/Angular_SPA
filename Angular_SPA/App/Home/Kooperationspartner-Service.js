app.service('kooperationspartnerService', function ($http, $q) {
    this.getKooperationspartner = function () {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: 'api/kooperationspartner'
            //params : {page:1, pagesize:5}
            //params : {skip:1, take:5, sort:'test1',filter:'test2'}
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