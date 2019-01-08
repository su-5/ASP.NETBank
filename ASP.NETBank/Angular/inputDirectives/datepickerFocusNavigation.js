(function () {
    "use strict";

    var datepickerFocusNavigationDirective = function () {
        return {
            restrict: 'A',
            link: function(scope, element, attrs) {
                scope.$watch(attrs.ngModel,
                    function(nVal, oldVal) {
                        if (element[0].attributes.getNamedItem('nofirstcall') &&
                            element[0].attributes.getNamedItem('nofirstcall').value != 'false') {
                            var formElements = document.body.getElementsByTagName("*");
                            for (var i = 0; i < formElements.length; i++) { //loop through each element
                                if (formElements[i].tabIndex == attrs.nextTab) {
                                    var nextElement = formElements[i];
                                    nextElement.focus(); //if it's the one we want, focus it and exit the loop
                                    break;
                                }
                            }
                        }
                        element[0].setAttribute("nofirstcall", true);
                    });

            }
        };
    };

    // register your directive into a dependent module.
    angular
        .module("Web.Directives")
        .directive("dateFocusNavigation", [datepickerFocusNavigationDirective]);
})();