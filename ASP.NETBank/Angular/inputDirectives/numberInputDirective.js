//Only number directive
(function () {
    "use strict";
    
    var numberInputDirective = function ($window) {
        return {
            // restrict: 'A',
            link: function ($scope, element, attrs) {
                element.bind('keypress',
                    function (e) {
                        var code = String.fromCharCode(e.keyCode);
                        if (isNaN(code) && code != ',' && code != '.' && code != '-' && code != 'Enter') {
                            e.preventDefault();
                        }
                        if ((code == '.' || code == ',') && e.target.value.indexOf('.') != -1) {
                            e.preventDefault();
                        } else if (code == ',') {
                            e.target.value += '.';
                            e.preventDefault();
                        }
                    });
                element.bind('keyup',
                    function (e) {
                        if (e.target.value == '') {
                            e.target.value += '0.00';
                            this.setSelectionRange(0, this.value.length);
                            e.preventDefault();
                        }
                    });
            }
        };
    };

    // register your directive into a dependent module.
    angular
        .module("Web.Directives")
        .directive("inputNumber", ["$window", numberInputDirective]);
})();