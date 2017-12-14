angular.module('app')
    .controller('JogoCtrl', function ($scope, authService, $location, $timeout, jogoService, $routeParams, $interval, pecaService, $rootScope, $sessionStorage, historicoService) {
        var acabouPartida = false;
        if ($routeParams.urlSala == null || $routeParams.urlSala == undefined) {
            $location.path('/home');
        }
        if ($sessionStorage.connect != undefined) {
            rodarJogo();
        }
        $scope.$on('isConnect', function (event, connect) {
            $sessionStorage.connect = connect;
            rodarJogo();
        });
        function rodarJogo() {
            jogoService.insereUsuario(authService.getUsuario().Login, $routeParams.urlSala);
            $rootScope.$on('infoJogador', function (event, jogador) {

                if (jogador == 'BRANCAS') {
                    $sessionStorage.usuarioCor = 0;
                } else if (jogador == 'PRETAS') {
                    $sessionStorage.usuarioCor = 1;
                }

                $scope.corJogador = $sessionStorage.usuarioCor;
                jogoService.consultar($routeParams.urlSala);
                $scope.$apply();
            });
            $rootScope.$on('partidaInexistente', function (event, mensagem) {
                alert(mensagem);
                $location.path('/home');
                $scope.$apply();
            });
            $rootScope.$on('jogada', function () {
                var peca = pecaService.getPosicaoPecas();
                jogoService.atualizar(peca.pecaMovimento, peca.pecaCor, $routeParams.urlSala)
                $timeout(function () {
                    jogoService.consultar($routeParams.urlSala);
                }, 500);
            });


            $scope.$on('buscarJogo', function (event, partida) {
                $scope.pecas = partida.Tabuleiro.Pecas;
                $scope.brancas = $scope.pecas.filter(p => p.Cor == 0);
                $scope.pretas = $scope.pecas.filter(p => p.Cor == 1);
                $scope.corJogando = parseInt(partida.Tabuleiro.CorTurnoAtual);
                $scope.jogadorBrancas = partida.JogadorBrancas
                $scope.jogadorPretas = partida.JogadorPretas

                buscarPontos = function (jogadorId) {
                    if (jogadorId != null || jogadorId != undefined) {
                        historicoService.buscar(jogadorId)
                            .then(function (response) {
                                if (response.data == 0) {
                                    return response.data;
                                } else {
                                    return response.data.pontos;
                                }
                            })
                    }
                }
                //$scope.pontosBrancas = buscarPontos($scope.jogadorBrancas.Id);
                //$scope.pontosPretas = buscarPontos($scope.jogadorPretas.Id);

            if($sessionStorage.usuarioCor == 0){
                $scope.quantidadeDePecasUsuario = $scope.brancas.length;
                $scope.quantidadeDePecasOponente = $scope.pretas.length;
            }else{
                $scope.quantidadeDePecasUsuario = $scope.pretas.length;
                $scope.quantidadeDePecasOponente = $scope.brancas.length;
            }
            $scope.$apply();
        });
        $scope.$on('fimJogo', function (event, mensagem) {
            if(mensagem == "BRANCA"){
                $scope.corGanhadora = 0;
            }else{
                $scope.corGanhadora = 1;
            }
            if(acabouPartida == false){
                if($scope.corGanhadora == $sessionStorage.usuarioCor){
                    jogoService.finalizaJogo({"LoginUsuario": authService.getUsuario().Login, "Venceu": true, "PecasRestantes":  $scope.quantidadeDePecasUsuario, "PecasEliminadas": 12 - $scope.quantidadeDePecasOponente});
                }else if($scope.corGanhadora != $sessionStorage.usuarioCor){
                    jogoService.finalizaJogo({"LoginUsuario": authService.getUsuario().Login, "Venceu": false, "PecasRestantes":  $scope.quantidadeDePecasUsuario, "PecasEliminadas": 12 - $scope.quantidadeDePecasOponente});
                }
                acabouPartida = true;
            }
            modal.style.display = "flex";
            modal.style.justifyContent = "center";
            $scope.corGanhadora;
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

        $scope.mostrarUrl = function () {
            $scope.mostrar = !$scope.mostrar;
            $scope.url = $routeParams.urlSala;
        }
    });