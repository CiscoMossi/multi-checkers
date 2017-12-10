angular
.module('app', ['ngRoute'])
.value('$', $)
.run(function(jogoService) {
    jogoService.connect();
})
;