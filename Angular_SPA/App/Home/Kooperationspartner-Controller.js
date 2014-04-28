

app.controller('kooperationspartnerController',
    function ($scope, $http, $q, $stateParams, $state, kooperationspartnerService) {
        //Perform the initialization


        $scope.totalServerItems = 0;
        $scope.success = true;

        $scope.pagedResult = {
            Success: false,
            ErrorMessage: 'Fehler',
            Result: {
                Data: [],
                TotalRows: 0
            }
        };

        $scope.filterOptions = {
            filterText: "",
            useExternalFilter: true
        };

        $scope.pagingOptions = {
            pageSizes: [5, 10, 20],
            pageSize: 10,
            currentPage: 1
        };

        $scope.sortInfo = {
            fields: ['name'],
            directions: ['asc']
        };

        $scope.gridOptions = {
            data: 'pagedResult.Result.Data',
            enablePaging: true,
            showFooter: true,
            totalServerItems: 'pagedResult.Result.TotalRows',
            pagingOptions: $scope.pagingOptions,
            filterOptions: $scope.filterOptions,
            sortInfo: $scope.sortInfo,
            enableCellSelection: false,
            enableRowSelection: false,
            enableCellEditOnFocus: false,
            enableSorting: true,
            showFilter: false,
            showColumnMenu: false,
            useExternalSorting: true,
            i18n: "de"
        };



        $scope.showData = function (data) {
            if (data.Success) {

            }
            $scope.pagedResult = data;

            if (!$scope.$$phase) {
                $scope.$apply();
            }
        };

        $scope.getPagedDataAsync = function (pageSize, page, searchText) {
            setTimeout(function () {

                var request = {
                    pageSize: $scope.pagingOptions.pageSize,
                    currentPage: $scope.pagingOptions.currentPage,
                    sortFields: $scope.sortInfo.fields,
                    sortDirections: $scope.sortInfo.directions,
                    filter: $scope.filterOptions.filterText
                };
                kooperationspartnerService.getKooperationspartner(request)
                .then(function (data) {
                    $scope.showData(data);
                }, function (error) {
                    // error handling here
                    $scope.showData(data);
                });

            }, 100);
        };

        //Daten das erste Mal holen
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

        $scope.$watch('sortInfo', function (newVal, oldVal) {
            if (newVal !== oldVal) {
                $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
            }
        }, true);



    });