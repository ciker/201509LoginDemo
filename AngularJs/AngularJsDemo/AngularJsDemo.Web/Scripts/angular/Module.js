var app = angular.module('myApp', ['ngRoute']);

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
    when('/User', {
        templateurl: '/Views/User/Index.cshtml',
        controller: 'userManageCtrl'
    }).
        otherwise({
            redirectto: '/Users'
        });
}]);