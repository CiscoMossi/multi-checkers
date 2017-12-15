angular.module('app')
.factory('usuarioService', function($http) {
    
    let urlBase = 'http://multicheckers.azurewebsites.net/api/usuario/';

    function buscar(id) {
        return $http.get(urlBase + id);
    }
    function cadastrar(usuario) {
        return $http.post("http://multicheckers.azurewebsites.net/api/api/Usuario", usuario)    
    }

    return {
        buscar: buscar,
        cadastrar: cadastrar
    };
})