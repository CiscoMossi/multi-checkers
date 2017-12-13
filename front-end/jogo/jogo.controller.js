angular.module('app')
.controller('JogoCtrl', function($scope, authService, $location, $timeout, jogoService, $routeParams, $interval, pecaService, $rootScope, $sessionStorage) {
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
        jogoService.insereUsuario(authService.getUsuario().Login, $routeParams.urlSala);
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
        $rootScope.$on('partidaInexistente', function(event, mensagem){
            alert(mensagem);
            $location.path('/home');
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
            $scope.brancas = $scope.pecas.filter(p => p.Cor == 0);
            $scope.pretas = $scope.pecas.filter(p => p.Cor == 1);
            $scope.corJogando = parseInt(partida.Tabuleiro.CorTurnoAtual);
            
            if(partida.JogadorBrancas.Id == null || partida.JogadorBrancas.Id != $scope.jogadorBrancas.Id)
            {
                usuarioService.buscar(partida.JogadorBrancas.Id)
                    .then(function (response) {
                        if (response.data == 0) {
                            $scope.jogadorBrancas.pontos = response.data;
                        } else {
                            $scope.jogadorBrancas.pontos = response.data.pontos;
                        }
                    });
            }
            if(partida.JogadorBrancas.Id == null || partida.JogadorPretas.Id != $scope.jogadorPretas.Id)
            {
                usuarioService.buscar(partida.JogadorPretas.Id)
                    .then(function (response) {
                        if (response.data == 0) {
                            $scope.jogadorPretas.pontos = response.data;
                        } else {
                            $scope.jogadorPretas.pontos = response.data.pontos;
                        }
                    });
            }

            $scope.jogadorBrancas = partida.JogadorBrancas;  
            $scope.jogadorPretas = partida.JogadorPretas;

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
        

        var modal = document.getElementById('myModal'); 
        }
    }

    var modal = document.getElementById('myModal');

    $scope.mostrar = false;

    $scope.alterarMostrar = function(){
        $scope.mostrar = !$scope.mostrar;
    }
});