'use strict';

app.factory('authService', function ($http, $q) {

    var factory = [];

    factory.login = function (username, password) {
        var deferred = $q.defer();
        
        $http({
            method: 'POST',
            url: '/token',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
            },
            data: 'grant_type=password&username=' + username + '&password=' + password,
        }).
         success(function (data, status, headers, config) {
             factory.isAuthenticated = true;
             factory.username = data.userName;
             factory.bearerToken = data.access_token;
             factory.expirationDate = new Date(data['.expires']);
             $http.defaults.headers.common.Authorization = 'Bearer ' + factory.bearerToken;
             deferred.resolve(data)
         }).
         error(function (data, status) {
             deferred.reject(data);
         });

        return deferred.promise;
    }



    return factory;
})