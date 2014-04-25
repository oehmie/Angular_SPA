

app.controller('kooperationspartnerController',
    function ($scope, $http, $q, $stateParams, $state, kooperationspartnerService) {
        //Perform the initialization

        
        $scope.totalServerItems = 0;
        $scope.success = true;

        $scope.pagedResult = {
            Success: false,
            ErrorMessage : 'Fehler',
            Result : {
                Data : [],
                TotalRows : 0
            }                 
        };

        $scope.filterOptions = {
            filterText: "",
            useExternalFilter: true
        };

        $scope.pagingOptions = {
            pageSizes: [5, 10, 20],
            pageSize: 5,
            currentPage: 1
        };

        $scope.gridOptions = {
            data: 'pagedResult.Result.Data',
            enablePaging: true,
            showFooter: true,
            totalServerItems: 'pagedResult.Result.TotalRows',
            pagingOptions: $scope.pagingOptions,
            filterOptions: $scope.filterOptions
        };

        

        $scope.setPagingData = function (data) {
            if (data.Success) {
                
            }
            $scope.pagedResult = data;

            if (!$scope.$$phase) {
                $scope.$apply();
            }
        };

        $scope.getPagedDataAsync = function (pageSize, page, searchText) {
            //setTimeout(function () {
                if (searchText) {
                    var ft = searchText.toLowerCase();
                    var request = {
                        pageSize: $scope.pagingOptions.pageSize,
                        currentPage: $scope.pagingOptions.currentPage
                    };
                    kooperationspartnerService.getKooperationspartner(request)
                    .then(function (data) {
                        $scope.setPagingData(data);
                    }, function (error) {
                        // error handling here
                        $scope.setPagingData(data);
                    });
                } else {
                    var request = {
                        pageSize: $scope.pagingOptions.pageSize,
                        currentPage: $scope.pagingOptions.currentPage
                    };
                    kooperationspartnerService.getKooperationspartner(request)
                    .then(function (data) {
                        $scope.setPagingData(data);
                    }, function (error) {
                        // error handling here
                        $scope.setPagingData(data);
                    });
                }
            //}, 100);
        };

        $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage);

        $scope.$watch('pagingOptions', function (newVal, oldVal) {
            if (newVal !== oldVal && (newVal.currentPage !== oldVal.currentPage || newVal.pageSize !== oldVal.pageSize)) {
                if (newVal.pageSize !== oldVal.pageSize)
                    $scope.pagingOptions.currentPage = 1;
                $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
            }
        }, true);

        $scope.$watch('filterOptions', function (newVal, oldVal) {
            if (newVal !== oldVal) {
                $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
            }
        }, true);

        
        

    });