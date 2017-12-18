angular.module('app').controller('LoginCtrl', function ($scope, authService, usuarioService, $sessionStorage, toastr) {

  $scope.auth = authService;
  $scope.mostrar = false;

  $scope.$on('isConnect', function (event, connect) {
    $sessionStorage.connect = connect;
    $scope.$apply();
  });

  $scope.login = function (usuario) {
    if ($scope.formLogin.$invalid) {
      toastr.error('Um ou mais campos inválidos.', 'Ops...');
      return;
    }
    authService.login(usuario)
      .then(
      function () {
        toastr.success('Login efetuado com sucesso.', '');
      },
      function (response) {
        toastr.error('Email ou senha inválido.', 'Ops...');
      });
  };

  $scope.mostrarCadastro = function () {
    $scope.mostrar = !$scope.mostrar;
  }

  $scope.cadastrar = function (usuario) {
    if (usuario.email == undefined || usuario.login == undefined || usuario.senha == undefined) {
      toastr.error('Por favor, corrija as informações inválidas.', 'Ops...');
      return;
    }
    usuarioService.cadastrar(usuario).then(
      function(){
        toastr.success('Cadastro efetuado.', '');
        $scope.mostrarCadastro();
      },
      function(response){
        toastr.error(response.data.Message, 'Ops...');
      }
    );
  }

});