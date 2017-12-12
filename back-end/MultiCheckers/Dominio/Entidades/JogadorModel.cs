using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiCheckers.Api.Models
{
    public class JogadorModel
    {
        public string IdConexao { private set; get; }
        public string Funcao { private set; get; }
        public JogadorModel(string idConexao, string funcao)
        {
            this.IdConexao = idConexao;
            this.Funcao = funcao;
        }
    }
}