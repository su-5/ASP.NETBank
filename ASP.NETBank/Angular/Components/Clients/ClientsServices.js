(function () {
    "use strict";

    function clientsService($cookies, $http, $rootScope, $q) {
        this.getAll = function () {
            var deferred = $q.defer();
            $http.get('api/Clients/getAllClients')
                .then(function (response) {
                    deferred.resolve(response.data);
                }).catch(function onError(response) {
                    deferred.reject(response.data);
                });
            return deferred.promise;
        };

        this.getAllCity = function () {
            var deferred = $q.defer();
            $http.get('api/City/getAllCity')
                .then(function (response) {
                    deferred.resolve(response.data);
                }).catch(function onError(response) {
                    deferred.reject(response.data);
                });
            return deferred.promise;
        };

        this.getAllCitizenship = function () {
            var deferred = $q.defer();
            $http.get('api/Clients/getAllCitizenship')
                .then(function (response) {
                    deferred.resolve(response.data);
                }).catch(function onError(response) {
                    deferred.reject(response.data);
                });
            return deferred.promise;
        };

        this.getAllPlaceOfWork = function () {
            var deferred = $q.defer();
            $http.get('api/Clients/getAllPlaceOfWork')
                .then(function (response) {
                    deferred.resolve(response.data);
                }).catch(function onError(response) {
                    deferred.reject(response.data);
                });
            return deferred.promise;
        };

        this.getAllDisability = function () {
            var deferred = $q.defer();
            $http.get('api/Clients/getAllDisability')
                .then(function (response) {
                    deferred.resolve(response.data);
                }).catch(function onError(response) {
                    deferred.reject(response.data);
                });
            return deferred.promise;
        };


        this.addClientDataBase = function (client) {
            // var newCountry = { Name: country.Name, Description: country.Description };
            var deferred = $q.defer();
            $http.post("api/Clients/addClientDataBase", client)
                .then(function (response) {
                    deferred.resolve(response.data);
                }).catch(function onError(response) {
                    deferred.reject(response.data);
                });
            return deferred.promise;
        };

        this.editClientDataBase = function (client) {
            var deferred = $q.defer();
            $http.put("api/Clients/editClientDataBase", client)
                .then(function (response) {
                    deferred.resolve(response.data);
                }).catch(function onError(response) {
                    deferred.reject(response.data);
                });
            return deferred.promise;
        };

        this.deleteClientDataBase = function (id) {
            var deferred = $q.defer();
            $http.delete('api/Clients/deleteClientDataBase?id=' + id)
                .then(function (response) {
                    deferred.resolve(response.data);
                }).catch(function onError(response) {
                    deferred.reject(response.data);
                });
            return deferred.promise;
        }

        this.getAllUsers = function () {
            var deferred = $q.defer();
            $http.get('api/Clients/getAllUsers')
                .then(function (response) {
                    deferred.resolve(response.data);
                }).catch(function onError(response) {
                    deferred.reject(response.data);
                });
            return deferred.promise;
        }

        this.addDeposit = function(deposit) {
            var deferred = $q.defer();
            $http.post('api/Clients/addDeposit',deposit)
                .then(function (response) {
                    deferred.resolve(response.data);
                }).catch(function onError(response) {
                    deferred.reject(response.data);
                });
            return deferred.promise;
        }
    }


    angular
        .module("Web.Services")
        .service("clientsService", ["$cookies", "$http", "$rootScope", "$q", clientsService]);

})(); 