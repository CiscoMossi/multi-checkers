angular.module('app')
.controller('JogoCtrl', function($scope, $timeout, jogoService, $routeParams, $interval, pecaService, $rootScope, $sessionStorage) {
    if($sessionStorage.connect == true){
        debugger;
        rodarJogo();
    }
    $scope.$on('isConnect', function(event, connect){
        debugger;
        $sessionStorage.connect = connect;
        rodarJogo();
    });
    function rodarJogo(){   
        jogoService.insereUsuario('CheckersKing', $routeParams.urlSala);
        $rootScope.$on('jogada', function(){
            var peca = pecaService.getPosicaoPecas();
            jogoService.atualizar(peca.pecaMovimento, peca.pecaCor, $routeParams.urlSala)
            jogoService.consultar($routeParams.urlSala);
        });
    
        $scope.corDoJogador = 0;
        jogoService.consultar($routeParams.urlSala);
        $scope.$on('buscarJogo', function (event, partida) {
            $scope.pecas = partida.Tabuleiro.Pecas;
            $rootScope.corJogando = parseInt(partida.Tabuleiro.CorTurnoAtual);
            $scope.jogadorBrancas = partida.JogadorBrancas
            if(!!partida.JogadorPretas)
                $scope.jogadorPretas = partida.JogadorPretas
            $scope.$apply();
        });
        $scope.$on('fimJogo', function (event, mensagem) {
            modal.style.display = "flex";
            modal.style.justifyContent = "center";
            $scope.$apply();
        });
    
        $scope.mostrarMovimentos = function(peca){
            if($rootScope.corJogando == peca.Cor && peca.PosicoesPossiveis.length != 0)
                $scope.selecionada = peca;
        }
    
        $scope.copiar = function(){
            let texto = document.getElementById("url");
            texto.select();
            document.execCommand("Copy");
        }
    
        var modal = document.getElementById('myModal'); 
    }

    var modal = document.getElementById('myModal');

    $scope.mostrar = false;    

    $scope.mostrarUrl = function(){
        $scope.mostrar = true;
    }

    $scope.fechar = function(){
        $scope.mostrar = false;
    }
});
