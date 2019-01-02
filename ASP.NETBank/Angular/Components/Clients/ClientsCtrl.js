(function () {
    "use strict";

    // controller class definintion
    var clientsController = function ($scope, $rootScope, clientsService, $uibModal, $q) {
        $rootScope.loadingShow();
        $scope.selectedRow = "";
        $scope.gridClients = {
            enableColumnResizing: true,
            showGridFooter: true,
            enableHorizontalScrollbar: 0,
            enableVerticalScrollbar: 1,
            enableColumnMenus: false,
            showColumnFooter: false,
            gridColumnFooterHeight: 20,
            enableRowSelection: true,
            enableRowHeaderSelection: false,
            noUnselect: false,
            multiSelect: false,
            enableCellEdit: false,
            enableFiltering: false,
            enableSorting: false,
            showSortMenu: false,
            enableColumnMenu: false,
            rowHeight: 22,
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
                    displayName: "Идент.номер"
                },
                {
                    field: 'E_mail',
                    displayName: 'E_mail',
                },
                {
                    field: 'buttons_edit_del',
                    displayName: "",
                    visible: true,
                    cellTemplate: "<div class=\"ui-grid-cell-contents\" align=\"center\">" +
                        "<button type='button' class='btn btn-danger btn-xs' style='margin-left: 2px;margin-top: -10px; margin-right: 2px; height: 18px; width: 24px;padding: 0px 5px;font-size: 12px;' ng-click='grid.appScope.deleteUser(row.entity.Id)'tooltip-placement ='left' uib-tooltip='Удалить запись'><i style='font-size: 15px;' class='fa fa-trash'></i></button>" +
                        "</div>",
                    enableCellEdit: false,
                    enableFiltering: true,
                    enableSorting: false,
                    showSortMenu: false,
                    enableColumnMenu: false
                }
                //{
                //    field: 'PlaceOfWork',
                //    displayName: 'Место работы',
                //    cellTemplate: '<p style="margin-left:80px;" >{{row.entity.PlaceOfWork.NamePlaseOfWork}}</p>'
                //}
            ],
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                $scope.gridApi.selection.on.rowSelectionChanged($scope,
                    function (row) {
                        $scope.selectedRow = row.entity;
                    });
            }

        };

        function getAllListData() {
            $q.all([
                clientsService.getAll(),
                clientsService.getAllCity(),
                clientsService.getAllCitizenship(),
                clientsService.getAllPlaceOfWork(),
                clientsService.getAllDisability()
            ]).then(function (results) {
                    $scope.gridClients.data = results[0];
                    $scope.CityRegistration = results[1];
                    $scope.Citizenship = results[2];
                    $scope.PlaceOfWork = results[3];
                    $scope.Disability = results[4];
                },
                function (errorObject) {
                    $rootScope.toaster('error', errorObject.Message, 9000);
                    for (var i = 0; i < errorObject.ModelState.error.length; i++) {
                        $rootScope.toaster('error', errorObject.ModelState.error[i], 9000);
                    }
                }).finally(function () {
                $rootScope.loadingHide();
            });
        }
       

        // flag = true (add client) and flag = false (edit client)
        $scope.addEditClient = function (flag) { 
            $uibModal.open({
                templateUrl: function () {
                    return 'Angular/Components/Clients/modal/addClient.html';
                },
                size: 'lg',
                scope: $scope,
                controller: [
                    '$rootScope', '$scope', '$uibModalInstance', function ($rootScope, $scope, $uibModalInstance) {
                        $scope.title = "Добавление нового клиента";
                        $scope.btnName = "Добавить";

                        $scope.listSex = [{ Name: "Мужской", Id: 1 }, { Name: "Женский", Id: 2 }];
                        $scope.CityRegistration = $scope.$parent.CityRegistration;
                        $scope.FamilyStatus = [{ Name: "Женат", Id: 1 }, { Name: "Холост", Id: 2 }];
                        $scope.Citizenship = $scope.$parent.Citizenship;
                        $scope.PlaceOfWork = $scope.$parent.PlaceOfWork;
                        $scope.Bool = [{ Name: "Да", Id: true }, { Name: "Нет", Id: false }];


                        if (!flag) {
                            $scope.client = $scope.$parent.selectedRow;
                            $scope.title = "Редактирование данных клиента";
                            $scope.btnName = "Изменить";
                        }


                        $scope.cancel = function () {
                            $uibModalInstance.dismiss({ $value: 'cancel' });
                        };

                        $scope.AddEditClientInfo = function (client, addClientForm) {
                            if (!addClientForm.$valid) {
                                return;
                            }

                            if (status) {
                                clientsService.addClientDataBase(client).then(function (results) {
                                        $rootScope.toaster('success', "Клиент успешно добавлен", 9000);
                                        $scope.cancel();
                                        getAllListData();
                                    },
                                    function (errorObject) {
                                        $rootScope.toaster('error', errorObject.Message, 9000);
                                        for (var i = 0; i < errorObject.ModelState.error.length; i++) {
                                            $rootScope.toaster('error', errorObject.ModelState.error[i], 9000);
                                        }
                                    }).finally(function () {
                                    $rootScope.loadingHide();
                                });
                            } else {
                                clientsService.editClientDataBase(client).then(function (results) {
                                        $rootScope.toaster('success', "Данные клиента успешно изменены", 9000);
                                        $scope.cancel();
                                        getAllListData();
                                    },
                                    function (errorObject) {
                                        $rootScope.toaster('error', errorObject.Message, 9000);
                                        for (var i = 0; i < errorObject.ModelState.error.length; i++) {
                                            $rootScope.toaster('error', errorObject.ModelState.error[i], 9000);
                                        }
                                    }).finally(function () {
                                    $rootScope.loadingHide();
                                });
                            }
                            
                        }
                    }
                ]
            });
        }

        getAllListData();
    };

    // register your controller into a dependent module 
    angular
        .module("Web.Controllers")
        .controller("clientsController", ["$scope", "$rootScope", "clientsService", "$uibModal", "$q", clientsController]);

})();