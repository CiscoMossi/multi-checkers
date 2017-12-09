angular.module('app')
.controller('JogoCtrl', function($scope, jogoService, $routeParams, $interval) {
    carregarJogo();
    $scope.corDoJogador = 0;
    $scope.urlSala = $routeParams.urlSala;

    function carregarJogo(){
        jogoService.buscarJogo().then(response => {
            $scope.pecas = response.data.dados.tabuleiro.Pecas;
            $scope.corJogando = response.data.dados.cor;
        });
    }
    //$interval(polling, 1000);
    $scope.jogar = function(){
        if($scope.corDoJogador != $scope.corJogando){
            alert("não é sua vez");
            return
        }
        /*TO-DO: implementar jogo*/
    }
    function polling(){
        debugger;
        if($scope.corDoJogador != $scope.corJogando){
            debugger;
            carregarJogo();   
        }
    }
    /*
    $scope.gerarPartida = function(){
        $scope.url = "aaaaaaaa";

        $scope.gerado = !!$scope.url;
    }*/


    $scope.peca = {
        "PosicaoAtual": "5, 3",
        "Cor": 0,
        "PosicoesPossiveis": [
          "6, 4",
          "4, 4"
        ],
        "IsDama": false
      };

    $scope.copiar = function(url){

    }
});
