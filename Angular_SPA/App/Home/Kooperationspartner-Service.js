'use strict';

app.factory('kooperationspartnerService', function ($http, $q) {

    var factory = [];

    factory.getKooperationspartner = function (request) {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: 'api/kooperationspartner',
            //params : {page:1, pagesize:5}
            //params : {skip:1, take:5, sort:'test1',filter:'test2'}
            params : request
        }).
         success(function (data, status, headers, config) {
             deferred.resolve(data)
         }).
         error(function (data, status) {
             deferred.reject(data);
         });

        return deferred.promise;
    }


    
    return factory;
});