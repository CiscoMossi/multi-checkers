angular.module('app')
.controller('JogoCtrl', function($scope, jogoService, $routeParams, $interval) {
    carregarJogo();
    $scope.corDoJogador = 0;
    $scope.urlSala = $routeParams.urlSala;

    function carregarJogo(){
        jogoService.buscarJogo().then(response => {
            $scope.pecas = response.data.dados.Pecas;
            $scope.corJogando = response.data.dados.CorTurnoAtual;
        });
    }
    $interval(polling, 5000);
    function polling(){
        carregarJogo();   
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
