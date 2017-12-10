angular.module('app')
.controller('JogoCtrl', function($scope, $timeout, jogoService, $routeParams, $interval, pecaService, $rootScope) {
    
    /* carregarJogo();
    $scope.corDoJogador = 0;
    $scope.urlSala = $routeParams.urlSala;

    function carregarJogo(){
        jogoService.buscarJogo().then(response => {
            $scope.pecas = response.data.dados.Pecas;
            $scope.corJogando = response.data.dados.CorTurnoAtual;
            if($scope.pecas == undefined){
                alert(response.data.dados);
            }
        });
    }
    $interval(polling, 3000);
    function polling(){
        if($scope.corDoJogador != $scope.corJogando){
            carregarJogo();   
        }
    }*/

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

    $scope.mostrarMovimentos = function(peca){
        $scope.selecionada = peca;
    }

    $scope.copiar = function(){
        let texto = document.getElementById("url");
        texto.select();
        document.execCommand("Copy");
    }
});
