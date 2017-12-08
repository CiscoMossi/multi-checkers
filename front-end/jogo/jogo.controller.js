angular.module('app')
.controller('JogoCtrl', function($scope, jogoService, $routeParams) {
    $scope.urlSala = $routeParams.urlSala;

    function carregarJogo(){
        jogoService.buscarTabuleiro().then(response => {
            $scope.pecas = response.data.dados.Pecas;
        });
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