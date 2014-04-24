

app.controller('KooperationspartnerController', function ($scope, $http, kooperationspartnerService) {
    //Perform the initialization

    $scope.filterOptions = {
        filterText: "",
        useExternalFilter: true
    };

    $scope.totalServerItems = 0;

    $scope.pagingOptions = {
        pageSizes: [5, 10, 20],
        pageSize: 5,
        currentPage: 1
    };  

    $scope.setPagingData = function (data) {
        if (data.Success) {
            var pagedData = data.Result.Data;//.slice((page - 1) * pageSize, page * pageSize);
            $scope.myData = pagedData;
            $scope.totalServerItems = data.Result.TotalRows;
        }
        $scope.success = data.Success;
        $scope.errorMessage = data.ErrorMessage;

        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };

    $scope.getPagedDataAsync = function (pageSize, page, searchText) {
        setTimeout(function () {
            var data;
            if (searchText) {
                var ft = searchText.toLowerCase();
                var request = {
                    pageSize : $scope.pagingOptions.pageSize,
                    currentPage : $scope.pagingOptions.currentPage
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
        }, 100);
    };
	
    $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage);
	
    $scope.$watch('pagingOptions', function (newVal, oldVal) {
        if (newVal !== oldVal && newVal.currentPage !== oldVal.currentPage) {
            $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
        }
    }, true);

    $scope.$watch('filterOptions', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
        }
    }, true);
	
    $scope.gridOptions = {
        data: 'myData',
        enablePaging: true,
        showFooter: true,
        totalServerItems:'totalServerItems',
        pagingOptions: $scope.pagingOptions,
        filterOptions: $scope.filterOptions
    };

    $scope.success = true;
    $scope.totalServerItems = 10;
});