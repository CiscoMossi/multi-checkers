angular.module('app')
.factory('jogoService', function($http){
    urlBase = 'http://localhost:49938/';
    function buscarTabuleiro(){
        return $http.get(urlBase+"tabuleiro");
    }
    function alterarTabuleiro(tabuleiro, cor){
        return $http.put(urlBase+`api/Tabuleiro?cor=${cor}`, tabuleiro);
    }
    function buscarCorAtual(){
        return $http.get(urlBase+"cor");
    }
    return {
        buscarTabuleiro : buscarTabuleiro,
        alterarTabuleiro : alterarTabuleiro,
        buscarCorAtual : buscarCorAtual
    }
});