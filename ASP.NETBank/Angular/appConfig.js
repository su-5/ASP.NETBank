(function () {
    "use strict";

    angular
        .module("Web")
        .config(["$stateProvider", "$urlRouterProvider", "$locationProvider", function ($stateProvider, $urlRouterProvider) {

            $stateProvider.state("Index", {
                url: "/Index",
                templateUrl: "Angular/Components/Index/Index.html",
                controller: "indexController"
            });

            $stateProvider.state("Clients", {
                url: "/Clients",
                templateUrl: "Angular/Components/Clients/Clients.html",
                controller: "clientsController"
            });

            $stateProvider.state("Deposits", {
                url: "/Deposits",
                templateUrl: "Angular/Components/DepositOperations/DepositOperations.html",
                controller: "depositController"
            });

            $stateProvider.state("Bankomat", {
                url: "/Bankomat",
                templateUrl: "Angular/Components/Bankomat/Bankomat.html",
                controller: "bankomatController"
            });

            $stateProvider.state("Reports", {
                url: "/depositReports",
                templateUrl: "Angular/Components/depositReports/depositReports.html",
                controller: "depositReportsController"
            });

            $urlRouterProvider.otherwise("/Index");
        }]);
})();