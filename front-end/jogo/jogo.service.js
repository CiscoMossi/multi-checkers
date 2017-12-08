angular.module('app')
.factory('jogoService', function($http){
    urlBase = 'http://localhost:49938/api/tabuleiro?cor=0';
    function criarTabuleiro(){
        return  $http.get(urlBase);
    }
    function carregarMovimentos(tabuleiro, cor){
        return $http.post(urlBase, tabuleiro, cor);
    }
    return {
        criarTabuleiro : criarTabuleiro,
        carregarMovimentos : carregarMovimentos
    }
});