var app = angular.module('main', ['ngRoute', 'ui.bootstrap', 'ngAnimate']);

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

    $locationProvider.html5Mode(true);

    $routeProvider
        .when('/', {
            templateUrl: '/index',
            controller: 'IndexController'
        })
        .when('/chat', {
            templateUrl: '/chat.html',
            controller: 'ChatController'
        })
        .otherwise({
            redirectTo: '/'
        });
}]);

app.factory('chatService', ['$rootScope', function ($rootScope) {
    var connection = $.hubConnection();
    $.connection.hub.logging = true;

    var chatProxy = connection.createHubProxy('ChatHub');

    chatProxy.on('addMessage', function (name, msg) {
        $rootScope.$emit('chat_message', { Name: name, Message: msg, Time: new Date() });
    });

    var send = function (name, msg) {
        chatProxy.invoke('Send', name, msg);
    };

    var start = function () { connection.start(); };
    var stop = function () { connection.stop(); };

    return {
        start: start,
        stop: stop,
        send: send
    };
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
}]);

app.controller('ChatController', ['$scope', '$location', 'chatService', function ($scope, $location, chatService) {
    chatService.start();
    
    $scope.$parent.$on('chat_message', function (e, msg) {
        $scope.$apply(function () {
            if (!$scope.Chat) { $scope.Chat = []; }
            if ($scope.Chat.length >= 5) {
                $scope.Chat.shift();
            }

            $scope.Chat.push(msg);
            $scope.Users = updateUsers($scope);
        });
    });

    $scope.send = function () {
        chatService.send($scope.name, $scope.message);
        $scope.message = '';
    };

    $scope.$on('$locationChangeStart', function () {
        chatService.stop();
    });

    function updateUsers(scope) {
        var testList = [];
        return scope.Chat.filter(function(value) {
            if (testList.indexOf(value.Name) != -1) {
                return false;
            }
            testList.push(value.Name);
            return true;
        });
    }
}]);
