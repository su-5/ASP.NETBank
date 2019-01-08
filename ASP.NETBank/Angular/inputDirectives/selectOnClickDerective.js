// Account of transaction directive

(function () {
    "use strict";

    // account directive
    var selectonClickDerective = function ($window) {
        return {
            restrict: 'A',
            link: function (scope, element) {
                element.on('click focus',
                    function () {
                        if (!$window.getSelection().toString()) {
                            // Required for mobile Safari
                            this.setSelectionRange(0, this.value.length);
                        }
                    });

            }
        };
    };

    // register your directive into a dependent module.
    angular
        .module("Web.Directives")
        .directive("selectOnClick", ["$window",selectonClickDerective]);
})();