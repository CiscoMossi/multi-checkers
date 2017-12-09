angular.module('app')
.factory('jogoService', function($http){
    urlBase = 'http://localhost:49938/api/Tabuleiro';
    function buscarJogo(){
        return $http.get(urlBase);
    }
    function alterarTabuleiro(movimento, cor){
        return $http.post(urlBase+`?cor=${cor}`, movimento);
    }
    return {
        buscarJogo : buscarJogo,
        alterarTabuleiro : alterarTabuleiro
    }
});
