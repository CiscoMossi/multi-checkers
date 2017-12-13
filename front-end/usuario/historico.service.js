angular.module('app')
.factory('historicoService', function($http) {
    
    let urlBase = 'http://localhost:1431/api/historico/';

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