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
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        private IMultiCheckersContext contexto;

        public UsuarioController(IMultiCheckersContext contexto)
        {
            this.contexto = contexto;
        }

        //[BasicAuthorization(Roles = "Jogador")]
        [HttpGet]
        public IHttpActionResult ObterUsuario(int id)
        {
            Usuario usuario = contexto.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                return BadRequest("Não existe o usuario informado");

            return Ok(usuario);
        }

        [HttpPost]
        public IHttpActionResult AdicionarUsuario([FromBody] UsuarioModel model)
        {
            Usuario usuarioRepetido = contexto.Usuarios
                            .FirstOrDefault(u => u.Login == model.Login || u.Email == model.Email);
            if (usuarioRepetido != null)
                return BadRequest("Já existe um usuário com esse Login ou Email");

            Usuario usuario = new Usuario(model.Login, model.Email, model.Senha);

            var erros = usuario.Validar();

            if (erros.Count > 0)
                return BadRequest(JsonConvert.SerializeObject(erros));

            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();

            return Ok(usuario);
        }

        //[BasicAuthorization]
        [HttpGet, Route("usuariologado")]
        public IHttpActionResult Obter()
        {
            var usuario = contexto.Usuarios.FirstOrDefault(u => u.Email == Thread.CurrentPrincipal.Identity.Name);

            if (usuario == null)
                return BadRequest("Usuário não encontrado.");

            var resposta = usuario.GerarUsuarioResposta();

            return Ok(resposta);
        }
    }
}
