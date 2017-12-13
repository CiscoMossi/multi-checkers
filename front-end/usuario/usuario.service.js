angular.module('app')
.factory('usuarioService', function($http) {
    
    let urlBase = 'http://localhost:1431/api/usuario/';

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