﻿(function () {
    "use strict";

    angular.module("Web.Services", []);
    angular.module("Web.Controllers", []);
    angular.module("Web.Directives", []);
    angular.module("Web.Externals", ["ui.router", "ngCookies", "ngTouch", "ngAnimate", "ui.grid", "ui.grid.selection", 'ui.grid.resizeColumns', 'ui.grid.moveColumns', 'ui.grid.cellNav', 'ui.grid.autoResize', 'ui.grid.pagination', 'ui.grid.grouping', 'ui.grid.expandable', 'ui.grid.edit', 'ui.grid.rowEdit', 'toaster']);

    let app = angular.module("Web", ["Web.Services", "Web.Directives", "Web.Externals", "Web.Controllers", "ui.bootstrap"]);

    app.run(["$rootScope", "$location", "$http", "$state", "$stateParams", "$sce", "loadingService", "toaster",
        function ($rootScope, _$location, _$http, _$state, _$stateParams, _$sce, loadingService, toaster) {
            $rootScope.loadingShow = function () {
                $rootScope.loadingIsShow = loadingService.show(); // loading
            };

            $rootScope.loadingHide = function () {
                $rootScope.loadingIsShow = loadingService.hide();
            };

            $rootScope.toaster = function (type, message, timeout, clickHandler) {
                toaster.pop(type, null, message, timeout, null, clickHandler);
            };
        }
    ]);

})();