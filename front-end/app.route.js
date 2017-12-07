angular.module('app')
.config(function ($routeProvider) {
    $routeProvider
        .when('/home', {
            templateUrl: 'home/home.html',
            controller: 'HomeCtrl'
        })
        .when('/sala', {
            templateUrl: 'jogo/jogo.html',
            controller: 'JogoCtrl'
        })
        .otherwise('/home');
})