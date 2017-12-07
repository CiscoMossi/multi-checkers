using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Tabuleiro
    {
        private static int LIMITE_MIN = 0;
        private static int LIMITE_MAX = 8;

        public Tabuleiro()
        {
            this.Pecas = new List<Peca>();
        }

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

            Pecas.Add(new Peca(new Point(6,2), Cor.PRETA));
            Pecas.Add(new Peca(new Point(6,4), Cor.PRETA));
            Pecas.Add(new Peca(new Point(6,6), Cor.PRETA));
            Pecas.Add(new Peca(new Point(6,8), Cor.PRETA));

            Pecas.Add(new Peca(new Point(7,1), Cor.PRETA));
            Pecas.Add(new Peca(new Point(7,3), Cor.PRETA));
            Pecas.Add(new Peca(new Point(7,5), Cor.PRETA));
            Pecas.Add(new Peca(new Point(7,7), Cor.PRETA));

            Pecas.Add(new Peca(new Point(8,2), Cor.PRETA));
            Pecas.Add(new Peca(new Point(8,4), Cor.PRETA));
            Pecas.Add(new Peca(new Point(8,6), Cor.PRETA));
            Pecas.Add(new Peca(new Point(8,8), Cor.PRETA));
        }

        private bool ValidarPecaDentroTabuleiro(Point posicaoDesejada)
        {
            return posicaoDesejada.X >= LIMITE_MIN &&
                   posicaoDesejada.Y >= LIMITE_MIN &&
                   posicaoDesejada.X <= LIMITE_MAX &&
                   posicaoDesejada.Y <= LIMITE_MAX;
        }

        private bool ValidarOutraPecaNoLocal(Peca peca, Point posicaoDesejada)
        {
            return this.Pecas.FirstOrDefault(p => p.PosicaoAtual.X == posicaoDesejada.X &&
                                                  p.PosicaoAtual.Y == posicaoDesejada.Y) != null;
        }

        private bool ValidarPecaInimiga(Peca peca, Point posicaoDesejada)
        {
            return this.Pecas.FirstOrDefault(p => p.PosicaoAtual.X == posicaoDesejada.X &&
                                                  p.PosicaoAtual.Y == posicaoDesejada.Y).Cor != peca.Cor;
        }

        public Peca EncontrarPeca(Point posicaoDesejada)
        {
            return this.Pecas.FirstOrDefault(p => p.PosicaoAtual.X == posicaoDesejada.X &&
                                                  p.PosicaoAtual.Y == posicaoDesejada.Y);
        }

        public void CalcularMovimentos(Peca peca)
        {
            Point posicaoDesejadaEsquerda = new Point(peca.PosicaoAtual.X + 1, peca.PosicaoAtual.Y + 1);
            Point posicaoDesejadaDireita = new Point(peca.PosicaoAtual.X + 1, peca.PosicaoAtual.Y + 1);

            if(this.ValidarPecaDentroTabuleiro(posicaoDesejadaDireita))
            {
                if (this.ValidarOutraPecaNoLocal(peca, posicaoDesejadaDireita))
                {
                    if (this.ValidarPecaInimiga(peca, posicaoDesejadaDireita))
                    {
                        // TO-DO: estruturar métodos para poder chamar de novo CalcularMovimentos com a nova posição
                    }
                } else
                    peca.AdicionarPosicao(posicaoDesejadaDireita);
            }
        } // TO-DO: estruturar métodos para só fazer essa lógica uma vez, sem precisar criar dois Point aqui dentro.

        public void PercorrerTabuleiro(Cor cor)
        {
            List<Peca> pecasAmigas = this.Pecas.FindAll(p => p.Cor == cor);
            pecasAmigas.ForEach(p => this.CalcularMovimentos(p));
        }

    }
}
