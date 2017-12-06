using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    class Tabuleiro
    {
        private static int LIMITE_MIN = 0;
        private static int LIMITE_MAX = 8;

        public Tabuleiro()
        { }

        public List<Peca> Pecas { get; private set; }

        public void PosicionarInicioPartida()
        {
            Pecas.Add(new Peca(new Point(1,1), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(1,3), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(1,5), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(1,7), Cor.BRANCA));

            Pecas.Add(new Peca(new Point(2,2), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(2,4), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(2,6), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(2,8), Cor.BRANCA));

            Pecas.Add(new Peca(new Point(3,1), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(3,3), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(3,5), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(3,7), Cor.BRANCA));

            Pecas.Add(new Peca(new Point(6, 2), Cor.PRETA));
            Pecas.Add(new Peca(new Point(6, 4), Cor.PRETA));
            Pecas.Add(new Peca(new Point(6, 6), Cor.PRETA));
            Pecas.Add(new Peca(new Point(6, 8), Cor.PRETA));

            Pecas.Add(new Peca(new Point(7, 1), Cor.PRETA));
            Pecas.Add(new Peca(new Point(7, 3), Cor.PRETA));
            Pecas.Add(new Peca(new Point(7, 5), Cor.PRETA));
            Pecas.Add(new Peca(new Point(7, 7), Cor.PRETA));

            Pecas.Add(new Peca(new Point(8, 2), Cor.PRETA));
            Pecas.Add(new Peca(new Point(8, 4), Cor.PRETA));
            Pecas.Add(new Peca(new Point(8, 6), Cor.PRETA));
            Pecas.Add(new Peca(new Point(8, 8), Cor.PRETA));
        }
        
        public bool ValidarPecaNoTabuleiro(Point posicaoDesejada)
        {
            return posicaoDesejada.X >= LIMITE_MIN &&
                   posicaoDesejada.Y >= LIMITE_MIN &&
                   posicaoDesejada.X <= LIMITE_MAX &&
                   posicaoDesejada.Y <= LIMITE_MAX;
        }

        public bool ValidarPecaDiagonal(Peca peca, Point posicaoDesejada)
        {
            return posicaoDesejada.Y == peca.PosicaoAtual.Y + 1 &&
                  (posicaoDesejada.X == peca.PosicaoAtual.X + 1 ||
                   posicaoDesejada.X == peca.PosicaoAtual.X - 1);
        }

        public void CalcularMovimentos(Peca peca, Point posicaoDesejada)
        {
            if (this.ValidarPecaNoTabuleiro(posicaoDesejada) &&
                this.ValidarPecaDiagonal(peca, posicaoDesejada) &&
                !peca.Descartada)
                peca.AdicionarPosicao(posicaoDesejada);
        }
    }
}
