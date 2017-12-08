angular.module('app')
    .controller('PecaController', function ($scope) {
        var x = 2;
        var y = 2;
        
        $scope.setPeca = function(peca){
            $scope.peca = peca;
            $scope.top = 12.5 *  $scope.peca.PosicaoAtual.Y;
            $scope.left = 12.5 *  $scope.peca.PosicaoAtual.X;
        }


        $scope.moverPeca = function(posicao){
            $scope.peca.PosicaoAtual.X = posicao
        }
    }
);    