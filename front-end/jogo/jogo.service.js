angular.module('app').factory('jogoService', ['$', '$rootScope',
function ($, $rootScope) {
    var proxy;
    var connection;
    return {
        connect: function () {
            var self = this;

            connection = $.hubConnection('http://multicheckers.azurewebsites.net/api/signalr');
            proxy = connection.createHubProxy('HubMessage');
            connection.start().done(function() {
                
            });
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
            proxy.on('isConnect', function(connect){
                $rootScope.$broadcast('isConnect', connect);
            });
            proxy.on('infoJogador', function(jogador){
                $rootScope.$broadcast('infoJogador', jogador);
            });
            proxy.on('partidaInexistente', function(mensagem){
                $rootScope.$broadcast('partidaInexistente', mensagem);
            });
            proxy.on('ativaSom', function(som){
                $rootScope.$broadcast('ativaSom', som);
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
        atualizar: function (jogada, cor, salaHash) {
            if(this.isConnected()){
                proxy.invoke('Atualizar', jogada, cor, salaHash);
            }
        },
        gerarUrl: function (){
            if(this.isConnected()) {
                proxy.invoke('CriarSala');
            }
        },
        insereUsuario: function (login, salaHash){
            if(this.isConnected()) {
                proxy.invoke('InserirUsuario', login, salaHash);
            }
        },
        finalizaJogo: function (historicoModel){
            debugger;
            if(this.isConnected()){
                proxy.invoke('FinalizarJogo', historicoModel);
            }
        },
    }
}]);


