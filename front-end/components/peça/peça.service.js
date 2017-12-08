angular.module('app')
.factory('pecaService', function(){
    var pecas = [];
    function setPosicaoPecas(pecasLista){
        pecas = pecasLista;
    }
    function getPosicaoPecas(){
        return pecas;
    }
    return {
        setPosicaoPecas : setPosicaoPecas,
        getPosicaoPecas : getPosicaoPecas
    }
});