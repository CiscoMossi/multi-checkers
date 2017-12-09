angular.module('app')
.factory('pecaService', function(){
    var peca = {};
    function setPosicaoPecas(pecaMovimento){
        peca = pecaMovimento;
    }
    function getPosicaoPecas(){
        return pecas;
    }
    return {
        setPosicaoPecas : setPosicaoPecas,
        getPosicaoPecas : getPosicaoPecas
    }
});