angular.module('app').controller('LoginCtrl', function ($scope, authService, usuarioService) {

  $scope.auth = authService;
  $scope.erroNoLogin = false;

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
    if ($scope.formCadastro == undefined || $scope.formCadastro.$invalid) {
      $scope.mostrarCadastro = false;
      return;
    }
    usuarioService.cadastrar(usuario);
    $scope.mostrarCadastro = false;
  }

});