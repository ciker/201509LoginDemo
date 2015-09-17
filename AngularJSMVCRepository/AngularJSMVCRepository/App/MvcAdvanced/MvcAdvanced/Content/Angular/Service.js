app.service("angularService", function ($http) {

        this.getEmp = function () {
            return $http.get("/Employee/GetAllEmployees");
        };

       

    //Save (Update)  
        this.update = function (employee) {
            var response = $http({
                method: "post",
                url: "/Employee/Update",
                data: JSON.stringify(employee),
                dataType: "json"
            });
            return response;
        }

    //Delete 
        this.Delete = function (empID) {
            var response = $http({
                method: "post",
                url: "/Employee/Delete",
                params: {
                    id: empID
                }
            });
            return response;
        }

    //Add 
        this.Add = function (employee) {
            var response = $http({
                method: "post",
                url: "/Employee/Add",
                data: JSON.stringify(employee),
                dataType: "json"
                
            });
            return response;
        }
   
});

app.service("angularServiceRole", function ($http) {
    this.getEmpRole = function () {
        return $http.get("/Permission/GetAllEmpRole");
    };

    this.getName = function () {
        return $http.get("/Permission/GetAllEmpNames");
    };
    this.getRole = function () {
        return $http.get("/Permission/GetAllRoles");
    };
        
    //Save Permission  
    this.updateRole = function (permission) {
        var response = $http({
            method: "post",
            url: "/Permission/UpdateRole",
            data: JSON.stringify(permission),
            dataType: "json"
        });
        return response;
    }
});