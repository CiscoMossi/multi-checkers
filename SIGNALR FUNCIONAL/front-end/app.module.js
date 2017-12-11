angular
.module('app', ['ngRoute', 'ngStorage'])
.value('$', $)
.run(function(jogoService) {
    jogoService.connect();
})
;