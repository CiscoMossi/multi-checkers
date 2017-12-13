using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiCheckers.Api.Models
{
    public class HistoricoModel
    {
        public string LoginUsuario { get; set; }
        public bool Venceu { get; set; }
        public int PecasRestantes { get; set; }
        public int PecasEliminadas { get; set; }
    }
}