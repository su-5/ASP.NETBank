(function () {
    "use strict";

    // controller class definintion
    var depositController = function ($scope, $rootScope, clientsService, $q) {
        $rootScope.loadingShow();

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

        $scope.deposits = [
            {
                Id: 1,
                Name: ' "Выше.net" Ставки до  - 8.0 % годовых (отзывной)'
            },
            {
                 Id: 2, Name: '"Выше.net (безотзывный)" Ставка - 12,5% годовых'
            }
        ];

        $scope.currensys = [
            {
                Id: 1,
                Name: 'BYN'
            },
            {
                Id: 2, Name: 'USD'
            }, {
                Id: 3, Name: 'EUR'
            }];


        $scope.addDeposit = function (deposit, depositForm)
        {
            if (!depositForm.$valid) {
                return;
            }
            $rootScope.loadingShow();
            $q.all([
                clientsService.addDeposit(deposit)
            ]).then(function (results) {
                $rootScope.toaster('success', "Счета успешно добавлены", 5000);
                getAllData();
                $scope.deposit = {};
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
        .controller("depositController", ["$scope", "$rootScope", "clientsService", "$q", depositController]);

})();