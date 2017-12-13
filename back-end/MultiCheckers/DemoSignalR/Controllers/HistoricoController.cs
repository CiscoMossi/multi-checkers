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
using PagedList;
using System.Web.Http.Cors;

namespace MultiCheckers.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/historico")]
    public class HistoricoController : ApiController
    {
        private IMultiCheckersContext contexto;

        public HistoricoController(IMultiCheckersContext contexto)
        {
            this.contexto = contexto;
        }

        [BasicAuthorization(Roles = "Jogador")]
        [HttpGet, Route("{id}")]
        public IHttpActionResult ObterPontosDoUsuario(int id)
        {
            var historicos = contexto.Historicos.Where(h => h.Usuario.Id == id);
            if (historicos.Count() == 0)
            {
                return Ok(0);
            }

            int pontos = historicos.Sum(h => h.Pontos);
            int partidas = historicos.Count();
            int vitorias = historicos.Where(h => h.Venceu).Count();
            int pecasDestruidas = historicos.Sum(h => h.PecasElimandas);

            Object resposta = new
            {
                pontos,
                partidas,
                vitorias,
                pecasDestruidas
            };
            return Ok(resposta);
        }

        [BasicAuthorization(Roles = "Jogador")]
        [HttpGet, Route("leaderboard/{page}")]
        public IHttpActionResult Listar(int? page)
        {
            var historicos = contexto.Historicos.OrderByDescending(x => x.Pontos)
                                                .GroupBy(h => h.Usuario.Id)
                                                .Select(x => new { Login = x.FirstOrDefault().Usuario.Login, Pontos = x.Sum(t => t.Pontos) })
                                                .ToList();
            if (historicos.Count() == 0)
                return BadRequest("Nenhum historico encontrado.");

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return Ok(historicos.ToPagedList(pageNumber, pageSize));
        }

    }
}
