angular.module('app')
    .controller('HomeCtrl', function ($scope, jogoService, $location, $sessionStorage, usuarioService, $localStorage, historicoService) {
        $scope.gerarPartida = function () {
            $scope.url = "";
            jogoService.gerarUrl();
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
            $location.path(`sala/${$scope.url}`);
            $scope.gerado = !!$scope.url;
            $scope.$apply();
        });
        $scope.$on('isConnect', function (event, connect) {
            $sessionStorage.connect = connect;
            $scope.$apply();
        });

        $scope.copiar = function () {
            let texto = document.getElementById("url");
            texto.select();
            document.execCommand("Copy");
        }

    });