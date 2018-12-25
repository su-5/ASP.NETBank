(function () {
    "use strict";

    // controller class definintion
    var clientsController = function ($scope, $rootScope, clientsService, $uibModal) {
        $rootScope.loadingShow();
        $scope.gridClients = {
            enableColumnResizing: true,
            showGridFooter: true,
            enableHorizontalScrollbar: 0,
            enableVerticalScrollbar: 1,
            enableColumnMenus: false,
            showColumnFooter: false,
            enableFiltering: true,
            gridColumnFooterHeight: 20,
            enableRowSelection: true,
            enableRowHeaderSelection: false,
            enableSelectAll: false,
            noUnselect: false,
            multiSelect: false,
            columnDefs: [
                {
                    field: 'Surname',
                    width: "10%",
                    displayName: 'Фамилия',
                },
                {
                    field: 'Name',
                    width: "10%",
                    displayName: "Имя",
                },
                {
                    field: 'MiddleName',
                    width: "10%",
                    displayName: 'Отчество'
                },
                {
                    field: 'DateBirth',
                    width: "10%",
                    displayName: "Дата рождения"
                },
                {
                    field: 'PassportSeries',
                    filter: true,
                    width: "8%",
                    displayName: "Серия паспорта",
                },
                {
                    field: 'PassportNo',
                    width: "10%",
                    displayName: '№ паспорта',
                },
                {
                    field: 'PassportIssuedBy',
                    width: "10%",
                    displayName: 'Кем выдан',
                },

                {
                    field: 'DateIssuePassport',
                    width: "10%",
                    displayName: 'Дата выдачи'
                },
                {
                    field: 'IDNumber',
                    width: "10%",
                    displayName: 'Идент.номер'
                },
                {
                    field: 'E_mail',
                    displayName: 'E_mail',
                }
                //{
                //    field: 'PlaceOfWork',
                //    displayName: 'Место работы',
                //    cellTemplate: '<p style="margin-left:80px;" >{{row.entity.PlaceOfWork.NamePlaseOfWork}}</p>'
                //}
            ]
            //onRegisterApi: function (gridApi) {
            //    $scope.gridApi = gridApi;
            //    $scope.gridApi.selection.on.rowSelectionChanged($scope,
            //        function (row) {
            //        });
            //}

        };

        clientsService.getAll().then(function (value) {
            $scope.gridClients.data = value;
        },
            function (errorObject) {
                $rootScope.toaster('error', errorObject.Message, 9000);
                for (var i = 0; i < errorObject.ModelState.error.length; i++) {
                    $rootScope.toaster('error', errorObject.ModelState.error[i], 9000);
                }
            }).finally(function () {
                $rootScope.loadingHide();
            });


        $scope.addClient = function() {
            $uibModal.open({
                templateUrl: function () {
                    return 'Angular/Components/Clients/modal/addClient.html';
                },
                size: 'lg',
                scope:$scope,
                controller: [
                    '$rootScope', '$scope', '$uibModalInstance', function($rootScope, $scope, $uibModalInstance) {
                        $scope.listSex = [{ Name: "Мужской", Id: 1 }, { Name: "Женский", Id: 2 }];

                        $scope.cancel = function () {
                            $uibModalInstance.dismiss({ $value: 'cancel' });
                        };
                    }
                ]
            });
        }
    };

    // register your controller into a dependent module 
    angular
        .module("Web.Controllers")
        .controller("clientsController", ["$scope", "$rootScope", "clientsService","$uibModal", clientsController]);

})();