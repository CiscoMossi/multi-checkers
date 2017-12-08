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
        public Peca(Posicao posicaoInicial, Cor cor)
        {
            this.PosicaoAtual = posicaoInicial;
            this.Cor = cor;
            this.PosicoesPossiveis = new List<Posicao>();
            this.IsDama = false;
        }

        public Posicao PosicaoAtual { get; set; }

        public Cor Cor { get; set; }

        public List<Posicao> PosicoesPossiveis { get; set; }

        public bool IsDama { get; set; }

        public void AdicionarPosicao(Posicao novaPosicao)
        {
            this.PosicoesPossiveis.Add(novaPosicao);
        }

        public void TransformarEmDama()
        {
            this.IsDama = true;
        }

    }
}
