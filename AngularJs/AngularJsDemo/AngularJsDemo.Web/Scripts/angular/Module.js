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

app.directive("mydctv",function() {
    return function(scope, ele, attrs) {
        ele.bind("mouseenter", function() {
            ele.css("background", "yellow");
        });
        ele.bind("mouseleave", function () {
            ele.css("background", "none");
        });
    }
})