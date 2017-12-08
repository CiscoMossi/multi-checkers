using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Jogada
    {
        public Jogada(Point posicaoEscolhida, Point posicaoAntiga)
        {
            this.PosicaoEscolhida = posicaoEscolhida;
            this.PosicaoAntiga = posicaoAntiga;
        }

        public Point PosicaoEscolhida { get; set; }

        public Point PosicaoAntiga { get; set; }
    }
}
