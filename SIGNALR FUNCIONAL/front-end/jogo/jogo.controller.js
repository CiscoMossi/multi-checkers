angular.module('app')
.controller('JogoCtrl', function($scope, $timeout, jogoService, $routeParams, $interval, pecaService, $rootScope) {

    $rootScope.$on('jogada', function(){
        var peca = pecaService.getPosicaoPecas();
        jogoService.atualizar(peca.pecaMovimento, peca.pecaCor)
        jogoService.consultar();
    });
    $scope.corDoJogador = 0;
    $scope.urlSala = $routeParams.urlSala;

    $scope.$on('buscarJogo', function (event, tabuleiro) {
        $scope.pecas = tabuleiro.Pecas;
        $scope.corJogando = parseInt(tabuleiro.CorTurnoAtual);
        $scope.$apply();
    });

    $scope.$on('fimJogo', function (event, mensagem) {
        alert(mensagem);
        $scope.$apply();
    });

    $scope.mostrarMovimentos = function(peca){
        $scope.selecionada = peca;
    }

    $scope.copiar = function(){
        let texto = document.getElementById("url");
        texto.select();
        document.execCommand("Copy");
    }

});
