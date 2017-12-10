angular.module('app')
.controller('HomeCtrl', function($scope) {
    $scope.gerarPartida = function(){
        $scope.url = "aaaaaaaa";
        $scope.gerado = !!$scope.url;
    }

    $scope.copiar = function(){
        let texto = document.getElementById("url");
        texto.select();
        document.execCommand("Copy");
    }

    $scope.alterar = function(){
        $scope.gerado = !$scope.gerado;
    }
});