using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Historico
    {
        private Historico()
        { }

        public Historico(Usuario usuario, bool venceu, int pecasRestantes, int pecasElimandas)
        {
            this.Usuario = usuario;
            this.Venceu = venceu;
            this.PecasRestantes = pecasRestantes;
            this.PecasElimandas = pecasElimandas;
            this.CalcularPontos();
        }

        public int Id { get; private set; }

        public Usuario Usuario { get; private set; }

        public bool Venceu { get; private set; }

        public int PecasRestantes { get; private set; }

        public int PecasElimandas { get; private set; }

        public int Pontos { get; private set; }

        private void CalcularPontos()
        {
            this.Pontos = (this.Venceu ? 20 : 2);
            this.Pontos += (this.Venceu ? (this.PecasRestantes) * 3 : 0);
        }
    }
}
