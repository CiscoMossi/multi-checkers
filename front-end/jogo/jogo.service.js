angular.module('app')
.factory('jogoService', function($http){
    urlBase = 'http://localhost:49938/api/Tabuleiro';
    function buscarJogo(){
        return $http.get(urlBase);
    }
    function alterarTabuleiro(tabuleiro, cor){
        return $http.put(urlBase+`?cor=${cor}`, tabuleiro);
    }
    return {
        buscarJogo : buscarJogo,
        alterarTabuleiro : alterarTabuleiro
    }
});
