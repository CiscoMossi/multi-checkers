using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiCheckers.Api.Models
{
    public class UsuarioModel
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }
    }
}