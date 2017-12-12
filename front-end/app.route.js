angular.module('app')
.config(function ($routeProvider) {
    $routeProvider
        .when('/login', {
            templateUrl: 'login/login.html',
            controller: 'LoginCtrl'
        })
        .when('/home', {
            templateUrl: 'home/home.html',
            controller: 'HomeCtrl',
            resolve: {
                autenticado: function (authService) {
                return authService.possuiPermissaoPromise("Jogador");
                }
            }
        })
        .when('/sala/:urlSala?', {
            templateUrl: 'jogo/jogo.html',
            controller: 'JogoCtrl',
            resolve: {
                autenticado: function (authService) {
                return authService.possuiPermissaoPromise("Jogador");
                }
            }
        })

        .otherwise('/home');
})