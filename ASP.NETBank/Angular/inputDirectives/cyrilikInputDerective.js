//Only number directive
(function () {
    "use strict";

    var cyrilicInputDerective = function () {
        return {
            restrict: 'A',
            link: function(scope, element, attrs, $modelCtrl) {
                element.bind('keypress',
                    function(e) {
                        var code = String.fromCharCode(e.keyCode);
                        var res = /[^а-яА-Я]/g.exec(code);
                        if (res != null) {
                            e.preventDefault();
                        }
                        element.css("text-transform", "uppercase");
                    });
            }
        };
    };

    // register your directive into a dependent module.
    angular
        .module("Web.Directives")
        .directive("cyrilicInput", [cyrilicInputDerective]);
})();