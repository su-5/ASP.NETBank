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

            //$stateProvider.state("mainPage/AdminPanel", {
            //    url: "/adminPanel",
            //    templateUrl: "Angular/Views/AdminPanel.html",
            //    controller: "adminController"
            //});

            $urlRouterProvider.otherwise("/Index");
        }]);
})();