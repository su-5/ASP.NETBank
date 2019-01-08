//Only number directive
(function () {
    "use strict";
    
    var disabledDashDirective = function ($window) {
        return {
            restrict: 'A',
            link: function (scope, element) {
                element.bind('keypress',
                    function (e) {
                        var code = String.fromCharCode(e.keyCode);
                        if (code == '-') {
                            e.preventDefault();
                        }
                    });
            }
        };
    };

    // register your directive into a dependent module.
    angular
        .module("Web.Directives")
        .directive("disabledDash", ["$window", disabledDashDirective]);
})();