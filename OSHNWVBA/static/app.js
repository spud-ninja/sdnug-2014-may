var app = angular.module('main', ['ngRoute']);

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

    $locationProvider.html5Mode(true);

    $routeProvider
        .when('/', {
            templateUrl: '/index',
            controller: 'IndexController'
        })
        .when('/external', {
            templateUrl: '/external',
            controller: 'ExternalController'
        })
        .otherwise({
            redirectTo: '/'
        });
}]);

app.controller('NavController', ['$scope', '$location', function ($scope, $location) {
    $scope.getClass = function (path) {
        if ($location.path().substr(0, path.length) == path) {
            if (path == "/" && $location.path() == "/") {
                return "active";
            } else if (path == "/") {
                return "";
            }
            return "active";
        } else {
            return "";
        }
    };
}]);

app.controller('IndexController', ['$scope', function ($scope) {
    $scope.Header = "Inline Header";
}]);

app.controller('ExternalController', ['$scope', function ($scope) {
    $scope.Header = "External Header";
}]);
