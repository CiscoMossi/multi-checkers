using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Dominio.Entidades;

namespace Dominio
{
    public class Peca
    {
        private Peca()
        { }

        public Peca(Point posicaoInicial, Cor cor)
        {
            this.PosicaoInicial = posicaoInicial;
            this.Cor = cor;
            this.posicoesPossiveis = new List<Point>();
        }

        public Point PosicaoInicial { get; private set; }

        public Cor Cor { get; private set; }

        public List<Point> posicoesPossiveis { get; private set; }

        public void salvarPosicao(Point novaPosicao)
        {
            this.posicoesPossiveis.Add(novaPosicao);
        }

    }
}
