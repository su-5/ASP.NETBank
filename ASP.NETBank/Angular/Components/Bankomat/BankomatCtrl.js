(function () {
    "use strict";

    // controller class definintion
    var bankomatController = function ($scope, $rootScope) {
        $scope.dataInput = true; // первичное меню
        $scope.mainMenu = false;

        $scope.ok = function () {

            if ($scope.dataInput) {
                cardPin();
            }

        }

        function cardPin() {
            if ($scope.numberCard !== undefined) {
                if ($scope.numberCard.length < 16) {
                    $rootScope.toaster('warning', "Номер карты должен составлять 16 символов", 10000);
                    return;
                }
            }
            else {
                $rootScope.toaster('warning', "Введите номер карты", 10000);
                return;
            }
            if ($scope.pinCode !== undefined) {
                if ($scope.pinCode.length < 4) {
                    $rootScope.toaster('warning', "Пин код должен составлять 4 символа", 10000);
                    return;
                }

            } else {
                $rootScope.toaster('warning', "Введите пин код", 10000);
                return;
            }

            $scope.dataInput = false; // первичное меню
            $scope.mainMenu = true;
        }
    };

    // register your controller into a dependent module 
    angular
        .module("Web.Controllers")
        .controller("bankomatController", ["$scope", "$rootScope", bankomatController]);

})();