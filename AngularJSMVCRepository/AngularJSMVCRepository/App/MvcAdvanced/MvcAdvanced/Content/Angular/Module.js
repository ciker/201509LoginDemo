var app = angular.module('MyApp', ['ngRoute']);

app.config(['$routeProvider',function ($routeprovider) {
      $routeprovider.
        when('/employee', {
            templateurl: '/Views/Employee/index.cshtml',
            controller: 'AngularCtrl'
        }).
        when('/permission', {
            templateurl: '/Views/Permission/index.cshtml',
            controller: 'AngularCtrlRole'
        }).
        otherwise({
            redirectto: '/employee'
        });
  }]);