(function () {
    "use strict";

    // controller class definintion
    var depositController = function ($scope, $rootScope, clientsService, $q) {
        $rootScope.loadingShow();

        $q.all([
            clientsService.getAllUsers(),
        ]).then(function (results) {
            $scope.clients = results[0];
            },
            function (errorObject) {
                $rootScope.toaster('error', errorObject.Message, 9000);
                for (var i = 0; i < errorObject.ModelState.error.length; i++) {
                    $rootScope.toaster('error', errorObject.ModelState.error[i], 9000);
                }
            }).finally(function () {
            $rootScope.loadingHide();
        });

        $scope.deposits = [
            {
                Id: 1,
                Name: ' "Выше.net" Ставки до  - 8.0 % годовых (отзывной)'
            }, { Id: 2, Name: '"Выше.net (безотзывный)" Ставка - 12,5% годовых' }
        ];
    };

    // register your controller into a dependent module 
    angular
        .module("Web.Controllers")
        .controller("depositController", ["$scope", "$rootScope", "clientsService", "$q", depositController]);

})();