angular.module('app')
.controller('HomeCtrl', function($scope, jogoService, $location, $sessionStorage) {
    $scope.gerarPartida = function(){
        $scope.url = "";
        jogoService.gerarUrl();
    }

    $scope.$on('criarSala', function(event, urlSala){
        $scope.url = urlSala;
        $location.path(`sala/${$scope.url}`);
        $scope.gerado = !!$scope.url;
        $scope.$apply();
    });
    $scope.$on('isConnect', function(event, connect){
        $sessionStorage.connect = connect;
    });

    $scope.copiar = function(){
        let texto = document.getElementById("url");
        texto.select();
        document.execCommand("Copy");
    }

    $scope.alterar = function(){
        $scope.gerado = !$scope.gerado;
    }
    $scope.fechar = function(){
        
    }
});