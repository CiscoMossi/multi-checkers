angular.module('app')
.factory('historicoService', function($http) {
    
    let urlBase = 'http://multicheckers.azurewebsites.net/api/historico/';

    function buscar(id) {
        return $http.get(urlBase + id);
    }

    function listar(page) {
        return $http.get(urlBase + "leaderboard/" + page);
    }

    return {
        buscar: buscar,
        listar: listar
    };
})