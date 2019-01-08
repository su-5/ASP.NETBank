// Account of transaction directive

(function () {
    "use strict";

    // account directive
    var transactionSummDerective = function () {
        return {
            require: '?ngModel',
            link: function ($scope, elem, attrs, ctrl) {
                if (!ctrl) return;

                var format = {
                    prefix: '',
                    centsSeparator: ',',
                    thousandsSeparator: ' '
                };

                ctrl.$parsers.unshift(function (viewValue) {
                    elem.priceFormat(format);
                    return elem[0].value;
                });

                ctrl.$formatters.unshift(function (value) {
                    elem[0].value = ctrl.$modelValue * 100;
                    elem.priceFormat(format);
                    return elem[0].value;
                });
            }
        };
    };

    // register your directive into a dependent module.
    angular
        .module("Web.Directives")
        .directive("summInput", [transactionSummDerective]);
})();