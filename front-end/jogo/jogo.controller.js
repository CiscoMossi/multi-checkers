angular.module('app')
.controller('JogoCtrl', function($scope, $location, $timeout, jogoService, $routeParams, $interval, pecaService, $rootScope, $sessionStorage) {
    if($routeParams.urlSala == null || $routeParams.urlSala == undefined){
        $location.path('/home');
    }
    if($sessionStorage.connect != undefined){
        rodarJogo();
    }
    $scope.$on('isConnect', function(event, connect){
        $sessionStorage.connect = connect;
        rodarJogo();
    });
    function rodarJogo(){   
        jogoService.insereUsuario('CheckersKing', $routeParams.urlSala);
        $rootScope.$on('infoJogador', function(event, jogador){

            if(jogador == 'BRANCAS'){
                $sessionStorage.usuarioCor = 0;
            }else if(jogador == 'PRETAS'){
                $sessionStorage.usuarioCor = 1;
            }

            $scope.corJogador = $sessionStorage.usuarioCor;
            jogoService.consultar($routeParams.urlSala);
            $scope.$apply();
        });
        $rootScope.$on('jogada', function(){
            var peca = pecaService.getPosicaoPecas();
            jogoService.atualizar(peca.pecaMovimento, peca.pecaCor, $routeParams.urlSala)
            $timeout( function(){
                jogoService.consultar($routeParams.urlSala);
            }, 500 );
        });
    
        $scope.$on('buscarJogo', function (event, partida) {
            $scope.pecas = partida.Tabuleiro.Pecas;
            $scope.corJogando = parseInt(partida.Tabuleiro.CorTurnoAtual);
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
    
        $scope.mostrarMovimentos = function(peca,cor){
            if($scope.corJogando == peca.Cor && 
                peca.PosicoesPossiveis.length != 0 &&
                $scope.corJogando == $sessionStorage.usuarioCor)
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

    $scope.alterarMostrar = function(){
        $scope.mostrar = !$scope.mostrar;
    }
});