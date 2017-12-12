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
        debugger;
        $sessionStorage.connect = connect;
        $scope.$apply();
    });

    $scope.copiar = function(){
        let texto = document.getElementById("url");
        texto.select();
        document.execCommand("Copy");
    }

    //TODO: implementar metodo de login
    $scope.logar = function(usuario){

    }
});