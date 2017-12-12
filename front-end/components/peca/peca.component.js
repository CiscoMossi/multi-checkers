angular.module('app').component('peca', {
    templateUrl: 'components/peca/peca.html',
    controller: 'PecaCtrl',
    bindings: {
      peca: '=',
      selecionada: '=',
      corJogador: '='
    }
  });