(function () {
    "use strict";

    // controller class definintion
    var bankomatController = function ($scope, $rootScope) {

    };

    // register your controller into a dependent module 
    angular
        .module("Web.Controllers")
        .controller("bankomatController", ["$scope", "$rootScope", bankomatController]);

})();