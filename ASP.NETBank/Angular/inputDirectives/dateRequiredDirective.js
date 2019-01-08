// Account of transaction directive

(function () {
    "use strict";

    // account directive
    var dateRequiredDerective = function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function(scope, elem, attr, ngModel) {
                elem.bind('keypress', function (e) {
                    var code = String.fromCharCode(e.keyCode);
                    if (isNaN(code) && code != '.' && code != 'Enter') {
                        e.preventDefault();
                    }
                });

                var regex = /^(\d{2})[.](\d{2})[.](\d{4})$/;

                elem.bind('blur', function (e) {
                    var value = e.target.value;
                    if (regex.test(value)) {
                        var buffer = value.split('.');
                        var dateStr = buffer[1].concat('.',buffer[0],'.', buffer[2]);
                        var dateNuN = Date.parse(buffer[1].concat('.', buffer[0], '.', buffer[2]));
                        var today = new Date();
                        if (isNaN(dateNuN) || new Date(dateStr) > today)
                            e.target.value = '';
                    } else {
                        e.target.value = '';
                    }
                   
                });
                //For DOM -> model validation
                //ngModel.$parsers.unshift(function (value) {
                //    if(value)
                //        var valid = value.length > 0 && value.length < 11;
                   
                //        ngModel.$setValidity('dateRequired', valid);
                //    return value;
                //});
                //For model -> DOM validation
                //ngModel.$formatters.unshift(function(value) {
                //    var valid = value.length > 1;
                //    ngModel.$setValidity('dateRequired', valid);
                //    return value;
                //});
            }
        };
    };

    // register your directive into a dependent module.
    angular
        .module("Web.Directives")
        .directive("dateRequired", [dateRequiredDerective]);
})();
