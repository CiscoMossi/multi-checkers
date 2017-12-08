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
        private int LIMITE_MIN = 1;
        private int LIMITE_MAX = 8;

        public Tabuleiro()
        {
            this.Pecas = new List<Peca>();
            this.PosicionarInicioPartida();
            this.PercorrerTabuleiro(Cor.BRANCA);
        }

        public List<Peca> Pecas { get; private set; }

        public bool Validar(Tabuleiro tabuleiro)
        {
            var listaPecas = tabuleiro.Pecas.Except(this.Pecas);

            return listaPecas.Count() != 1;
        }

        public void PosicionarInicioPartida()
        {            
            Pecas.Add(new Peca(new Posicao(1,1), Cor.BRANCA));
            Pecas.Add(new Peca(new Posicao(3,1), Cor.BRANCA));
            Pecas.Add(new Peca(new Posicao(5,1), Cor.BRANCA));
            Pecas.Add(new Peca(new Posicao(7,1), Cor.BRANCA));

            Pecas.Add(new Peca(new Posicao(2,2), Cor.BRANCA));
            Pecas.Add(new Peca(new Posicao(4,2), Cor.BRANCA));
            Pecas.Add(new Peca(new Posicao(6,2), Cor.BRANCA));
            Pecas.Add(new Peca(new Posicao(8,2), Cor.BRANCA));

            Pecas.Add(new Peca(new Posicao(1,3), Cor.BRANCA));
            Pecas.Add(new Peca(new Posicao(3,3), Cor.BRANCA));
            Pecas.Add(new Peca(new Posicao(5,3), Cor.BRANCA));
            Pecas.Add(new Peca(new Posicao(7,3), Cor.BRANCA));

            Pecas.Add(new Peca(new Posicao(2,6), Cor.PRETA));
            Pecas.Add(new Peca(new Posicao(4,6), Cor.PRETA));
            Pecas.Add(new Peca(new Posicao(6,6), Cor.PRETA));
            Pecas.Add(new Peca(new Posicao(8,6), Cor.PRETA));

            Pecas.Add(new Peca(new Posicao(1,7), Cor.PRETA));
            Pecas.Add(new Peca(new Posicao(3,7), Cor.PRETA));
            Pecas.Add(new Peca(new Posicao(5,7), Cor.PRETA));
            Pecas.Add(new Peca(new Posicao(7,7), Cor.PRETA));

            Pecas.Add(new Peca(new Posicao(2,8), Cor.PRETA));
            Pecas.Add(new Peca(new Posicao(4,8), Cor.PRETA));
            Pecas.Add(new Peca(new Posicao(6,8), Cor.PRETA));
            Pecas.Add(new Peca(new Posicao(8,8), Cor.PRETA));
        }

        private void ValidarDama(Peca peca)
        {
            if ((peca.Cor == Cor.BRANCA && peca.PosicaoAtual.Y == 8) ||
                (peca.Cor == Cor.PRETA && peca.PosicaoAtual.Y == 1))
                peca.TransformarEmDama();
        }

        private bool ValidarPecaDentroTabuleiro(Posicao posicaoDesejada)
        {
            return posicaoDesejada.X >= LIMITE_MIN &&
                   posicaoDesejada.Y >= LIMITE_MIN &&
                   posicaoDesejada.X <= LIMITE_MAX &&
                   posicaoDesejada.Y <= LIMITE_MAX;
        }

        private bool ValidarOutraPecaNoLocal(Peca peca, Posicao posicaoDesejada)
        {
            return this.Pecas.FirstOrDefault(p => p.PosicaoAtual.X == posicaoDesejada.X &&
                                                  p.PosicaoAtual.Y == posicaoDesejada.Y) != null;
        }

        private bool ValidarPecaInimiga(Peca peca, Posicao posicaoDesejada)
        {
            return this.Pecas.FirstOrDefault(p => p.PosicaoAtual.X == posicaoDesejada.X &&
                                                  p.PosicaoAtual.Y == posicaoDesejada.Y).Cor != peca.Cor;
        }

        private bool ValidarERefazerMovimento(Peca peca, Posicao posicaoDesejada)
        {
            if(this.ValidarPecaDentroTabuleiro(posicaoDesejada))
            {
                if (this.ValidarOutraPecaNoLocal(peca, posicaoDesejada))
                {
                    if (this.ValidarPecaInimiga(peca, posicaoDesejada))
                        return true;
                }
                else
                    peca.AdicionarPosicao(posicaoDesejada);
            }
            return false;
        }

        private void AplicarMovimentos(Peca peca, Posicao movimentacao)
        {
            if (this.ValidarERefazerMovimento(peca, new Posicao(peca.PosicaoAtual.X + movimentacao.X,
                                                              peca.PosicaoAtual.Y + movimentacao.Y)))
            {
                peca.AdicionarPosicao(new Posicao(peca.PosicaoAtual.X + 2* movimentacao.X,
                                                peca.PosicaoAtual.Y + 2* movimentacao.Y));
            }
        }

        private void CalcularMovimentos(Peca peca)
        {
            Posicao avancoDireita = new Posicao(1, peca.Cor == Cor.BRANCA ? 1 : -1);
            Posicao avancoEsquerda = new Posicao(-1, peca.Cor == Cor.BRANCA ? 1 : -1);

            this.AplicarMovimentos(peca, avancoDireita);
            this.AplicarMovimentos(peca, avancoEsquerda);

            if (peca.IsDama)
            {
                Posicao recuoDireita = new Posicao(1, peca.Cor == Cor.BRANCA ? -1 : 1);
                Posicao recuoEsquerda = new Posicao(-1, peca.Cor == Cor.BRANCA ? -1 : 1);

                this.AplicarMovimentos(peca, recuoDireita);
                this.AplicarMovimentos(peca, recuoEsquerda);
            }
            this.ValidarDama(peca);
        }

        public void PercorrerTabuleiro(Cor cor)
        {
            List<Peca> pecasAmigas = this.Pecas.FindAll(p => p.Cor == cor);
            pecasAmigas.ForEach(p => this.CalcularMovimentos(p));
        }

        public void AdicionarPeca(Peca peca)
        {
            this.Pecas.Add(peca);
        }

    }
}
