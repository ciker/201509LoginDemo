app.controller("userManageCtrl",
    function ($scope, userManageService) {
        $scope.divUserModification = false;
        getUserList();

        function getUserList() {
            var Data = userManageService.getUserList();
            Data.then(function (resp) {
                $scope.Users = resp.data;
            });
        }

        $scope.edit = function (user) {
            $scope.ID = user.ID;
            $scope.Name = user.Name;
            $scope.Email = user.Email;
            $scope.Mobile = user.Mobile;
            $scope.CryptoOperation = " Edit ";
            $scope.divUserModification = true;
        }

        $scope.add = function () {

            $scope.ID = "";
            $scope.Name = "";
            $scope.Email = "";
            $scope.Mobile = "";
            $scope.Operation = "Add";
            $scope.divUserModification = true;
        }


        $scope.saveUser = function () {
            var UserInfo = {
                ID: $scope.ID,
                Name: $scope.Name,
                Email: $scope.Email,
                Mobile: $scope.Mobile
            }
            var data = userManageService.saveUser(UserInfo);
            console.log(data);
            data.then(function (resp) {
                $scope.Users = resp.data;
            });
        }

    })