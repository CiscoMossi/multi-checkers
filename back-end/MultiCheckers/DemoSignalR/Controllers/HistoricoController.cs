using Dominio;
using Infra.Migrations;
using MultiCheckers.Api.App_Start;
using MultiCheckers.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace MultiCheckers.Api.Controllers
{
    public class HistoricoController : ApiController
    {
        private IMultiCheckersContext contexto;

        public HistoricoController(IMultiCheckersContext contexto)
        {
            this.contexto = contexto;
        }

        [BasicAuthorization(Roles = "Jogador")]
        [HttpGet]
        public IHttpActionResult ObterPontosDoUsuario(int usuarioId)
        {
            var historicos = contexto.Historicos.Where(h => h.Usuario.Id == usuarioId);
            if (historicos == null)
                return Ok(0);

            return Ok(historicos.Sum(p => p.Pontos));
        }

    }
}
