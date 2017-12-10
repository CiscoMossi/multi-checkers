angular.module('app')
.controller('JogoCtrl', function($scope, connectionService, jogoService, $routeParams, $interval, pecaService, $rootScope) {
    carregarJogo();
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
    }
    $rootScope.$on('jogada', function(){
        var peca = pecaService.getPosicaoPecas();
        jogoService.alterarTabuleiro(peca.pecaMovimento, peca.pecaCor).then(
            response => {
                $scope.corJogando = response.data;
            }
        );
    });

    $scope.copiar = function(){
        let texto = document.getElementById("url");
        texto.select();
        document.execCommand("Copy");
    }

    $scope.mostrarMovimentos = function(peca){
        $scope.selecionada = peca;
    }
});
