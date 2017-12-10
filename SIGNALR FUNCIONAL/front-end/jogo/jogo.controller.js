angular.module('app')
.controller('JogoCtrl', function($scope, $timeout, jogoService, $routeParams, $interval, pecaService, $rootScope) {

    $rootScope.$on('jogada', function(){
        var peca = pecaService.getPosicaoPecas();
        jogoService.atualizar(peca.pecaMovimento, peca.pecaCor)
        jogoService.consultar();
    });
    $scope.corDoJogador = 0;
    $scope.urlSala = $routeParams.urlSala;

    $scope.$on('buscarJogo', function (event, partida) {
        $scope.pecas = partida.Tabuleiro.Pecas;
        $scope.corJogando = parseInt(partida.Tabuleiro.CorTurnoAtual);
        $scope.jogadorBrancas = partida.JogadorBrancas
        $scope.$apply();
    });

    $scope.$on('fimJogo', function (event, mensagem) {
        alert(mensagem);
        $scope.$apply();
    });

    $scope.mostrarMovimentos = function(peca){
        if($scope.corJogando == peca.Cor && peca.PosicoesPossiveis.length != 0)
            $scope.selecionada = peca;
    }

    $scope.copiar = function(){
        let texto = document.getElementById("url");
        texto.select();
        document.execCommand("Copy");
    }

});
