angular.module('app')
.controller('HomeCtrl', function($scope) {
    $scope.gerarPartida = function(){
        $scope.url = "aaaaaaaa";
        $scope.gerado = !!$scope.url;
    }

    $scope.copiar = function(url){
        
    }

    $scope.alterar = function(){
        $scope.gerado = !$scope.gerado;
    }
});