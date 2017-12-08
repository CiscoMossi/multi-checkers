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
    $scope.copiar = function(url){
        
    }
});