

app.factory('authService', function ($http, $q) {
    'use strict';

    var factory = [];
    factory.isAuthenticated = false;

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
             factory.userName = data.userName;
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

    factory.logout = function() {
        var deferred = $q.defer();
        $http({
            method: 'POST',
            url: '/api/account/logout',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
            },
            
        }).
        success(function (data, status, headers, config) {
            factory.isAuthenticated = false;
            factory.username = undefined;
            factory.bearerToken = undefined;
            factory.expirationDate = undefined;
            $http.defaults.headers.common.Authorization = 'Bearer ' + factory.bearerToken;
            deferred.resolve(data)
        }).
        error(function (data, status) {
            deferred.reject(data);
        });


        return deferred.promise;
    }

    factory.editProfile = function () {
    }

    factory.changePassword= function (oldPassword, newPassword) {
    }

    factory.forgotPassword = function (email) {
        var deferred = $q.defer();
        //var data = {
        //    email: $scope.email
        //};
        $http({
            method: 'POST',
            url: '/api/account/forgotPassword',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
            },
            data: 'email=' + email,
        }).
        success(function (data, status, headers, config) {
            deferred.resolve(data)
        }).
        error(function (data, status) {            
            deferred.reject(data);
        });


        return deferred.promise;
    }

    factory.resetPassword = function (email, code, password) {
    }


    return factory;
})