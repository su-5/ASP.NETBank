// Account of transaction directive

(function () {
    "use strict";

    // account directive
    var inputBlurValidDerective = function () {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                element.bind('blur',
                    function (e) {
                        if (typeof this.value == "string") {
                            this.value = "";
                            element.css('width', '90px');
                        }
                    });
            }
        };
    };

    // register your directive into a dependent module.
    angular
        .module("Web.Directives")
        .directive("blurValid", [inputBlurValidDerective]);
})();