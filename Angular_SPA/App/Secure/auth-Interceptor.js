'use strict';

app.factory('authHttpResponseInterceptor', ['$q', '$location','$window',
    function ($q, $location, $window) {
        return {
            request: function (config) {
                config.headers = config.headers || {};
                if ($window.sessionStorage.token) {
                    config.headers.Authorization = 'Bearer ' + $window.sessionStorage.token;
                }
                return config || $q.when(config);
            },
            response: function (response) {
                if (response.status === 401) {
                    console.log("Response 401");
                }
                return response || $q.when(response);
            },
            responseError: function (rejection) {
                if (rejection.status === 401) {
                    console.log("Response Error 401", rejection);
                    var returnUrl = $location.url();
                    $location.path('/login').search('returnTo', returnUrl);
                }
                return $q.reject(rejection);
            }
        }
    }
])
.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authHttpResponseInterceptor');
});
