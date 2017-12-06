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
            this.PosicaoAtual = posicaoInicial;
            this.Cor = cor;
            this.PosicoesPossiveis = new List<Point>();
            this.Descartada = false;
        }

        public Point PosicaoAtual { get; private set; }

        public Cor Cor { get; private set; }

        public List<Point> PosicoesPossiveis { get; private set; }

        public bool Descartada { get; private set; }

        public void AdicionarPosicao(Point novaPosicao)
        {
            this.PosicoesPossiveis.Add(novaPosicao);
        }
        public void Descartar()
        {
            this.Descartada = true;
        }
    }
}
