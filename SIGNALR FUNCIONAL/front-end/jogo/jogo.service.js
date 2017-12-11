angular.module('app').factory('jogoService', ['$', '$rootScope',
function ($, $rootScope) {
    var proxy;
    var connection;
    return {
        connect: function () {
            var self = this;

            connection = $.hubConnection('http://localhost:9090/signalr');
            proxy = connection.createHubProxy('HubMessage');
            connection.start()/*.done(function() {
                console.log("Conectado")
            })*/;
            proxy.on('buscarJogo', function (tabuleiro) {
                $rootScope.$broadcast('buscarJogo', tabuleiro);
            });
            proxy.on('alterarTabuleiro', function (resposta) {
                $rootScope.$broadcast('alterarTabuleiro', resposta);
            });
            proxy.on('fimJogo', function(mensagem) {
                $rootScope.$broadcast('fimJogo', mensagem);
            });
            proxy.on('criarSala', function(urlSala) {
                $rootScope.$broadcast('criarSala', urlSala);
            });
        },
        isConnecting: function () {
            return connection.state === 0;
        },
        isConnected: function () {
            return connection.state === 1;
        },
        connectionState: function () {
            return connection.state;
        },
        consultar: function (salaHash) {
            if(this.isConnected()) {
                proxy.invoke('Consultar', salaHash);
            }
        },
        atualizar: function (jogada, cor) {
            if(this.isConnected()){
                proxy.invoke('Atualizar', jogada, cor);
            }
        },
        gerarUrl: function (login){
            if(this.isConnected()) {
                proxy.invoke('CriarSala', login);
            }
        },
    }
}]);


