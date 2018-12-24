(function () {
    "use strict";

    // controller class definintion
    var clientsController = function ($scope, $rootScope) {
       
    };

    // register your controller into a dependent module 
    angular
        .module("Web.Controllers")
        .controller("clientsController", ["$scope", "$rootScope", clientsController]);

})();