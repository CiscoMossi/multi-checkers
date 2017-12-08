angular.module('app')
.controller('JogoCtrl', function($scope, jogoService) {
    $scope.gerarPartida = function(){
        $scope.url = "aaaaaaaa";

        $scope.gerado = !!$scope.url;
    }

    $scope.copiar = function(url){
        
    }
});