'use strict';

app.factory('authService', ['$http', '$q','$window',
    function ($http, $q, $window) {


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
                 $window.sessionStorage.token = data.access_token;
                 deferred.resolve(data)
             }).
             error(function (data, status) {
                 console.log('Fehler bei Login', data);
                 deferred.reject(data);
             });

            return deferred.promise;
        }

        factory.logout = function () {
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
                delete $window.sessionStorage.token;
                deferred.resolve(data);
            }).
            error(function (data, status) {
                console.log('Fehler bei Logout', data);
                deferred.reject(data);
            });


            return deferred.promise;
        }

        factory.editProfile = function () {
        }

        factory.changePassword = function (oldPassword, newPassword) {
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
                console.log('Fehler bei Passwort vergessen', data)
                deferred.reject(data);
            });


            return deferred.promise;
        }

        factory.resetPassword = function (email, password, passwordconfirmation, code) {
            var deferred = $q.defer();
            //var data = {
            //    email: $scope.email
            //};
            $http({
                method: 'POST',
                url: '/api/account/ResetPassword',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                },
                data: 'email=' + email + '&password=' + password + '&confirmpassword=' + passwordconfirmation + '&code=' + code,
                //data: { Code: code, Email: email, Password: password, ConfirmPassword: passwordconfirmation },
            }).
            success(function (data, status, headers, config) {
                deferred.resolve(data)
            }).
            error(function (data, status) {
                console.log('Fehler bei Passwort zurücksetzen', data)
                deferred.reject(data);
            });


            return deferred.promise;
        }

        return factory;
    }
]);