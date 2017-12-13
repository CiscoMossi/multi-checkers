angular.module('app').controller('LoginCtrl', function ($scope, authService, usuarioService, $sessionStorage) {

  $scope.auth = authService;
  $scope.erroNoLogin = false;

  $scope.$on('isConnect', function(event, connect){
    $sessionStorage.connect = connect;
    $scope.$apply();
  });

  $scope.login = function (usuario) {
    if ($scope.formLogin.$invalid) {
      return;
    }
    authService.login(usuario)
      .then(
      function () {
        $scope.erroNoLogin = false;
      },
      function (response) {
        $scope.erroNoLogin = true;
      });
  };

  $scope.fecharCadastro = function(){
    $scope.mostrarCadastro = false;
  }

  $scope.cadastrar = function(usuario){
    if (usuario.email == undefined || usuario.login == undefined || usuario.senha == undefined) {
      $scope.mostrarCadastro = false;
      return;
    }
    usuarioService.cadastrar(usuario);
    $scope.mostrarCadastro = false;
  }

});