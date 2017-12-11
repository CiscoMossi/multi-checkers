using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace MultiCheckers.Api.Models
{
    public class JogadaModel
    {
        public JogadaModel(Point posicaoEscolhida, Point posicaoAntiga)
        {
            this.PosicaoEscolhida = posicaoEscolhida;
            this.PosicaoAntiga = posicaoAntiga;
        }

        public Point PosicaoEscolhida { get; set; }

        public Point PosicaoAntiga { get; set; }
    }
}