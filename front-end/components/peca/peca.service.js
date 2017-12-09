angular.module('app')
.factory('pecaService', function($rootScope){
    var peca = {};
    function setPosicaoPecas(pecaMovimento, pecaCor){
        peca = {pecaMovimento , pecaCor};
        $rootScope.$broadcast('jogada');
    }
    function getPosicaoPecas(){
        return peca;
    }
    return {
        setPosicaoPecas : setPosicaoPecas,
        getPosicaoPecas : getPosicaoPecas
    }
});