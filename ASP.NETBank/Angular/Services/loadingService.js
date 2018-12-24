(function () {
    "use strict";

    function loadingService() {
        var service = {
                show: show,
                hide: hide,
                get: get
            },
            loadingShow = false;

        function show() {
            loadingShow = true;
            return loadingShow;
        }

        function hide() {
            loadingShow = false;
            return loadingShow;
        }

        function get() {
            return loadingShow;
        }

        return service;
    }

    angular
        .module("Web.Services")
        .factory("loadingService", loadingService);

})();