(function () {
    "use strict";

    // controller class definintion
    var indexController = function ($scope, $rootScope, $location) {
        var url = $location.path();
        if (url != "AnaliticReport") {
            const text = document.querySelector("h1");
            text.innerHTML = "Asp.NetBank";   
        }
    };

    // register your controller into a dependent module 
    angular
        .module("Web.Controllers")
        .controller("indexController", ["$scope", "$rootScope","$location", indexController]);

})();