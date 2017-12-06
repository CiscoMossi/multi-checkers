angular.module('app')
.config(function ($routeProvider) {
    $routeProvider
        .when('/home', {
            templateUrl: 'home/home.html',
            controller: 'HomeCtrl'
        })
        .otherwise('/home');
})