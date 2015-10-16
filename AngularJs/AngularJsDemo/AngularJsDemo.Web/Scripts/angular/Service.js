var options = {
    getUserlistUrl: "/User/GetUserList",
    editUserUrl: "/User/EditUser",
    addUserUrl: "/User/AddUser"
}

app.service("userManageService", function ($http) {
    this.getUserList = function () {
        return $http.get(options.getUserlistUrl);
    }

    this.saveUser = function (user) {
        return $http({
            method: 'post',
            url: options.editUserUrl,
            data: JSON.stringify(user),
            dataType: 'json'
        });
    }

    this.addUser = function (user) {
        return $http({
            method: "post",
            url: options.addUserUrl,
            data: JSON.stringify(user),
            dataType: 'json'
        });
    }
})