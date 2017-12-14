angular.module('app')
.factory('usuarioService', function($http) {
    
    let urlBase = 'http://multicheckers.azurewebsites.net/api/usuario/';

    function buscar(id) {
        return $http.get(urlBase + id);
    }
    function cadastrar(usuario) {
        return $http.post(urlBase, usuario)    
    }

    return {
        buscar: buscar,
        cadastrar: cadastrar
    };
})