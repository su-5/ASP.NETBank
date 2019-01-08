    (function () {
    "use strict";

    var focusNavigationDirective = function () {
        return {
            // restrict: 'A',
            link: function(scope, element, attrs) {
                element.bind("keydown keypress",
                    function(event) {
                        if (event.which === 13 || event.which === 39 || event.which === 37) {

                            event.preventDefault();
                            var formElements = document.body.getElementsByTagName("*");
                            for (var i = 0; i < formElements.length; i++) { //loop through each element
                                if (formElements[i].tabIndex > -1) {
                                    var nextElement = formElements[i];
                                    var currentElementTabIndex = element[0].tabIndex;
                                    if (event.which === 13 || event.which === 39) {
                                        if (nextElement
                                            .tabIndex >
                                            (currentElementTabIndex)) {
                                            //check the tabindex to see if it's the element we want
                                            nextElement.focus(); //if it's the one we want, focus it and exit the loop
                                            break;
                                        }
                                    } else {
                                        if (nextElement.tabIndex == (currentElementTabIndex - 1)) {
                                            //check the tabindex to see if it's the element we want
                                            nextElement.focus(); //if it's the one we want, focus it and exit the loop
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                    });
            }
        };
    };

    // register your directive into a dependent module.
    angular
        .module("Web.Directives")
        .directive("focusNavigation", [focusNavigationDirective]);
})();