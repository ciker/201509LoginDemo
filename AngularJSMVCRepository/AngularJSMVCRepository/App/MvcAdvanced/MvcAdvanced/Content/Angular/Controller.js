app.controller("AngularCtrl", function ($scope, angularService) {
    $scope.divEmpModification = false;
    GetAll();
    //To Get All Records  
    function GetAll() {
        var Data = angularService.getEmp();
            Data.then(function (emp) {
            $scope.employees = emp.data;
        }, function () {
            alert('Error');
        });
    }

    $scope.edit = function (employee) {
           
            $scope.ID = employee.ID;
            $scope.FirstName = employee.FirstName;
            $scope.LastName = employee.LastName;
            $scope.UserName = employee.UserName;
            $scope.Password = employee.Password;
            $scope.Operation = "Update";
            $scope.divEmpModification = true;     
    }

    $scope.add = function () {

        $scope.ID ="";
        $scope.FirstName = "";
        $scope.LastName = "";
        $scope.UserName = "";
        $scope.Password = "";
        $scope.Operation = "Add";
        $scope.divEmpModification = true;
    }

    $scope.Save = function () {
        var Employee = {
            FirstName: $scope.FirstName ,
            LastName: $scope.LastName ,
            UserName: $scope.UserName,
            Password: $scope.Password 
        };
        var Operation = $scope.Operation;

        if (Operation == "Update") {
            Employee.ID = $scope.ID;
            var getMSG = angularService.update(Employee);
            getMSG.then(function (messagefromController) {
                GetAll();
                alert(messagefromController.data);
                $scope.divEmpModification = false;
            }, function () {
                alert('Update Error');
            });
        }
        else {
            var getMSG = angularService.Add(Employee);
            getMSG.then(function (messagefromController) {
                GetAll();
                alert(messagefromController.data);
                $scope.divEmpModification = false;
            }, function () {
                alert('Insert Error');
            });
        }
    }

    $scope.delete = function (employee) {
        var getMSG = angularService.Delete(employee.ID);
        getMSG.then(function (messagefromController) {
            GetAll();
            alert(messagefromController.data);
        }, function () {
            alert('Delete Error');
        });
    }
});

app.controller("AngularCtrlRole", function ($scope, angularServiceRole) {
    GetAllNames();
    GetAllRoles();
    GetAllEmpRole();
    //To Get All Records  
    function GetAllEmpRole() {
        var Data = angularServiceRole.getEmpRole();
        Data.then(function (emp) {
            $scope.views = emp.data;
        }, function () {
            alert('Error');
        });
    }

    //To Get All Records  
    function GetAllNames() {
        var Data = angularServiceRole.getName();
        Data.then(function (emp) {
            $scope.items = emp.data;
        }, function () {
            alert('Error');
        });
    }

    function GetAllRoles() {
        var Data = angularServiceRole.getRole();
        Data.then(function (role) {
            $scope.roleitems = role.data;
        }, function () {
            alert('Error');
        });
    }

    $scope.SavePermission = function () {
        var Permission = {
            ID: $scope.selectedItem.ID,
            RoleID: $scope.selectedItemRole.ID
         };
           
        var getMSG = angularServiceRole.updateRole(Permission);
            getMSG.then(function (messagefromController) {
                GetAllNames();
                GetAllRoles();
                GetAllEmpRole();
                alert(messagefromController.data);
              }, function () {
                alert('Save Permission Error');
            });
       
       
    }

});