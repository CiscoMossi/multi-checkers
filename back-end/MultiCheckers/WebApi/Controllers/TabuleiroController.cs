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
    [RoutePrefix("api/tabuleiro")]
    public class TabuleiroController : BaseController
    {
        [HttpPost]
        public HttpResponseMessage EditarClasseVoo([FromBody] Tabuleiro tabuleiro, int cor)
        {
            tabuleiro.PercorrerTabuleiro((Cor) cor);

            return ResponderOK(tabuleiro);
        }
    }
}
