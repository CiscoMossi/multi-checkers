angular.module('app')
.controller('HomeCtrl', function($scope, jogoService, $location) {
    $scope.gerarPartida = function(){
        $scope.url = "";
        var login = "CheckersKing";
        jogoService.gerarUrl(login);
    }
    $scope.$on('criarSala', function(event, urlSala){
        $scope.url = urlSala;
        $location.path(`sala/${$scope.url}`);
        $scope.gerado = !!$scope.url;
        $scope.$apply();
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