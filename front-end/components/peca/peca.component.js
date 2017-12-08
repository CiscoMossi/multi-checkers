angular.module('app').component('peca', {
    templateUrl: 'components/peca/peca.html',
    controller: 'PecaController',
    bindings: {
      peca: '='
    }
  });