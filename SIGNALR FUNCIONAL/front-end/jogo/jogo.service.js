angular.module('app').factory('jogoService', ['$', '$rootScope',
function ($, $rootScope) {
    var proxy;
    var connection;
    return {
        connect: function () {
            var self = this;

            connection = $.hubConnection('http://192.168.0.101:8082/signalr');
            proxy = connection.createHubProxy('HubMessage');
            connection.start().done(function() {
                console.log("Conectado")
            });
            proxy.on('buscarJogo', function (tabuleiro) {
                $rootScope.$broadcast('buscarJogo', tabuleiro);
            });
            proxy.on('alterarTabuleiro', function (resposta) {
                $rootScope.$broadcast('alterarTabuleiro', resposta);
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
        consultar: function () {
            if(this.isConnected()) {
                proxy.invoke('Consultar');
            }
        },
        atualizar: function (jogada, cor){
            if(this.isConnected()){
                proxy.invoke('Atualizar', jogada, cor);
            }
        },
    }
}]);


