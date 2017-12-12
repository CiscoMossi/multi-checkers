angular.module('app')
.factory('historicoService', function($http) {
    
    let urlBase = 'http://localhost:1431/api/Historico/';

    function buscar(id) {
        return $http.get(urlBase + id);
    }

    return {
        buscar: buscar
    };
})