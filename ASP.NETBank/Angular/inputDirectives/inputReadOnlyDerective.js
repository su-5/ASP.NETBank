//Only number directive
(function () {
    "use strict";

    var inputReadOnlyDerective = function ($window) {
        return {
            restrict: 'A',
            link: function (scope, element) {
                element.bind('keydown keypress',
                    function (e) {
                        var code = String.fromCharCode(e.keyCode);
                        if (code != 'Tab')
                            e.preventDefault();
                    });
            }
        };
    };

    // register your directive into a dependent module.
    angular
        .module("Web.Directives")
        .directive("inputReadOnly", ["$window", inputReadOnlyDerective]);
})();