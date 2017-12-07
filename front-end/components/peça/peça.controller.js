angular.module('app')
    .controller('PecaController', function ($scope) {
        var x = 2;
        var y = 2;
        
        $scope.top = 12.5 * y;
        $scope.left = 12.5 * x;
    }
);    