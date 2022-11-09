(function () {
    "use strict";

    // controller class definintion
    let analiticController = function ($scope, $rootScope) {
        const text = document.querySelector("h1");
        text.innerHTML = "Аналитический отчет.";   
    };

    // register your controller into a dependent module 
    angular
        .module("Web.Controllers")
        .controller("analiticController", ["$scope", "$rootScope", analiticController]);

})();