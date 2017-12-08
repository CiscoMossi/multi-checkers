angular.module('app')
    .controller('PecaController', function ($scope) {
        $scope.setPeca = function(peca){
            $scope.peca = peca;
            $scope.selecionada = false;
            var posicao = $scope.peca.PosicaoAtual.split(", ");
            $scope.peca.x = 12.5 * (parseInt(posicao[0]) - 1) + 1.2;
            $scope.peca.y = 12.5 * (parseInt(posicao[1]) - 1) + 1.2;
            
            let movimentosString = [];
            $scope.peca.PosicoesPossiveis
                                .forEach(
                                    n => movimentosString.push(n.split(", "))
                                );

            $scope.movimentos = [];
            movimentosString.forEach(
                n => $scope.movimentos.push({"x": parseInt(n[0]), 
                             "y": parseInt(n[1])}
                            ));
        }

        $scope.mostrarMovimentos = function(){
            $scope.selecionada = true;
        }

        $scope.moverPeca = function(posicao){
            $scope.peca.x = 12.5 * posicao.x + 1.2;
            $scope.peca.y = 12.5 * posicao.y + 1.2;
            $scope.selecionada = false;
        }
    }
);    