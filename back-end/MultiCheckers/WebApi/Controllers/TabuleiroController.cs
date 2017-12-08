using Dominio;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class TabuleiroController : BaseController
    {
        private static Cor COR_ATUAL = Cor.BRANCA;
        private static Tabuleiro TABULEIRO_ATUAL = new Tabuleiro();

        [HttpGet]
        [Route("cor")]
        public HttpResponseMessage ConsultarCorAtual()
        {
            return ResponderOK(COR_ATUAL);
        }

        [HttpGet]
        [Route("tabuleiro")]
        public HttpResponseMessage ConsultarTabuleiro()
        {
            return ResponderOK(TABULEIRO_ATUAL);
        }

        [HttpPut]
        public HttpResponseMessage AtualizarTabuleiro([FromBody] Tabuleiro tabuleiro, int cor)
        {
            if ((Cor) cor != COR_ATUAL)
                return ResponderErro("Turno do adversário");

            if (TABULEIRO_ATUAL.Validar(tabuleiro))
                return ResponderErro("Apenas uma peça pode ser movimentada por jogada");

            TABULEIRO_ATUAL.PercorrerTabuleiro((Cor)cor);
            COR_ATUAL = (COR_ATUAL == Cor.BRANCA ? Cor.PRETA : Cor.BRANCA);

            return ResponderOK(TABULEIRO_ATUAL);
        }
    }
}
