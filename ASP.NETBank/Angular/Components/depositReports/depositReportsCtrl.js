(function () {
    "use strict";

    // controller class definintion
    var depositReportsController = function ($scope, $rootScope, clientsService, $q) {
        $rootScope.loadingShow();
        $scope.Accounts = [];
        function getAllData() {
            $q.all([
                clientsService.getAllUsers(),
                clientsService.getSummBank()
            ]).then(function(results) {
                    $scope.clients = results[0];
                    $scope.getSummBank = results[1];
                },
                function(errorObject) {
                    $rootScope.toaster('error', errorObject.Message, 9000);
                    for (var i = 0; i < errorObject.ModelState.error.length; i++) {
                        $rootScope.toaster('error', errorObject.ModelState.error[i], 9000);
                    }
                }).finally(function() {
                $rootScope.loadingHide();
            });
        }

        $scope.$watch('deposit.ClientId', function (depositUserId) {
            if (depositUserId !== undefined) {
                $rootScope.loadingShow();
                $q.all([
                    clientsService.getDepositsUser(depositUserId)
                ]).then(function (results) {
                    $scope.Accounts = results[0];
                    },
                    function (errorObject) {
                        $rootScope.toaster('error', errorObject.Message, 9000);
                        for (var i = 0; i < errorObject.ModelState.error.length; i++) {
                            $rootScope.toaster('error', errorObject.ModelState.error[i], 9000);
                        }
                    }).finally(function () {
                    $rootScope.loadingHide();
                });
            }

        });

        $scope.$watch('deposit.AccountId', function (AccountId) {
            if (AccountId !== undefined) {
                var dateBegin = $scope.Accounts.find(function(value) {
                    return value.Id === AccountId;
                });
                $scope.deposit.DateBegin = dateBegin.DateBegin;
            }

        });


        $scope.addDeposit = function (deposit, depositForm)
        {
            if (!depositForm.$valid) {
                return;
            }
            $rootScope.loadingShow();
            $q.all([
                clientsService.addDeposit(deposit)
            ]).then(function (results) {
                    $scope.deposits = results;
                },
                function (errorObject) {
                    $rootScope.toaster('error', errorObject.Message, 9000);
                    for (var i = 0; i < errorObject.ModelState.error.length; i++) {
                        $rootScope.toaster('error', errorObject.ModelState.error[i], 9000);
                    }
                }).finally(function () {
                $rootScope.loadingHide();
            });

        }

        $scope.report = function(deposit, depositReportForm) {
            if (!depositReportForm.$valid) {
                return;
            }
            $rootScope.loadingShow();
            $q.all([
                clientsService.getreport(deposit)
            ]).then(function (data) {
                var file = new Blob([data[0]], { type: 'octet-stream' });
                saveAs(file, 'отчет по депозитам.xls');
                },
                function (errorObject) {
                    $rootScope.toaster('error', errorObject.Message, 9000);
                    for (var i = 0; i < errorObject.ModelState.error.length; i++) {
                        $rootScope.toaster('error', errorObject.ModelState.error[i], 9000);
                    }
                }).finally(function () {
                $rootScope.loadingHide();
            });
        }

        getAllData();
    };

    // register your controller into a dependent module 
    angular
        .module("Web.Controllers")
        .controller("depositReportsController", ["$scope", "$rootScope", "clientsService", "$q", depositReportsController]);

})();