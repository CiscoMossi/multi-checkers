angular.module('app')
    .controller('HomeCtrl', function ($scope, jogoService, authService, $window, $sessionStorage, usuarioService, $localStorage, historicoService) {
        $scope.gerarPartida = function () {
            jogoService.gerarUrl();
        }

        $scope.logout = function(){
            authService.logout();
        } 

        $scope.pagina = 1;

        carregarLeaderBoard($scope.pagina);

        function carregarLeaderBoard(pagina){
            historicoService.listar(pagina).then(function(response) {
                if(response.data.length > 0){
                    $scope.leaderboard = response.data
                }else{
                    $scope.pagina--;
                }
            });
        }

        $scope.usuarioLogado = $localStorage.usuarioLogado;

        function buscar(id) {
            historicoService.buscar(id)
                .then(function (response) {
                    if (response.data == 0) {
                        $scope.usuarioLogado.pontos = response.data;
                        $scope.usuarioLogado.partidas = response.data;
                        $scope.usuarioLogado.vitorias = response.data;
                        $scope.usuarioLogado.pecasDestruidas = response.data;
                    } else {
                        $scope.usuarioLogado.pontos = response.data.pontos;
                        $scope.usuarioLogado.partidas = response.data.partidas;
                        $scope.usuarioLogado.vitorias = response.data.vitorias;
                        $scope.usuarioLogado.pecasDestruidas = response.data.pecasDestruidas;
                    }
                })
        }

        buscar($scope.usuarioLogado.Id);

        $scope.$on('criarSala', function (event, urlSala) {
            $scope.url = urlSala;
            $window.open(`/#!/sala/${$scope.url}`);
            $scope.gerado = !!$scope.url;
            $scope.$apply();
        });
        $scope.$on('isConnect', function (event, connect) {
            $sessionStorage.connect = connect;
            $scope.$apply();
        });
        
        $scope.addIndicePag = function(quant){
            if($scope.pagina + quant > 0){
                $scope.pagina = $scope.pagina + quant;
                carregarLeaderBoard($scope.pagina);
            }
        }
        
        $scope.buttonResponsivo = document.body.scrollWidth <= 750;
        $scope.statusLeaderboard = !$scope.buttonResponsivo;

        function setButtonResponsive() {
            $scope.buttonResponsivo = document.body.scrollWidth <= 750;
        }
        
        window.onresize = function(){
            $scope.buttonResponsivo = document.body.scrollWidth <= 750;
        };

        $scope.mostrarLeaderboard = function(){
            $scope.statusLeaderboard = !$scope.statusLeaderboard;
        }
    });