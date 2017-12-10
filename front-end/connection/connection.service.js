angular.module('app')
.factory('connectionService', function($){
    $.connection.hub.url = "http://localhost:8082/signalr";
    function connection(){
        return $.connection;
    }
    return {
        connection : connection
    }
});