(function () {
    "use strict";

    // controller class definintion
    var indexController = function ($scope, $rootScope) {
       
    };

    // register your controller into a dependent module 
    angular
        .module("Web.Controllers")
        .controller("indexController", ["$scope", "$rootScope", indexController]);

})();