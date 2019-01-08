// directive
(function () {
    "use strict";

    var cellEditGridDirective = function () {
        return {
            restrict: 'A',
            link: function(scope, element, attrs, $modelCtrl) {
                element.bind('keydown', function (e) {
                    if (e.keyCode == 13) {
                        var nextElement = document.getElementById('cell-account-' + attrs.id.substring(9));
                        if (nextElement)
                            nextElement.focus();
                    }
                });
            }
        };
    };

    // register your directive into a dependent module.
    angular
        .module("Web.Directives")
        .directive("cellEditGrid", [cellEditGridDirective]);
})();