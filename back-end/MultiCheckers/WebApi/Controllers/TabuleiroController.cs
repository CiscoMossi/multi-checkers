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
    [RoutePrefix("tabuleiro")]
    public class TabuleiroController : BaseController
    {
        [HttpGet]
        public HttpResponseMessage IniciarPartida()
        {
            Tabuleiro tabuleiro = new Tabuleiro();
            tabuleiro.PosicionarInicioPartida();

            return ResponderOK(tabuleiro);
        }

        [HttpPost]
        public HttpResponseMessage CarregarMovimentos([FromBody] Tabuleiro tabuleiro, int cor)
        {
            tabuleiro.PercorrerTabuleiro((Cor)cor);

            return ResponderOK(tabuleiro);
        }
    }
}
