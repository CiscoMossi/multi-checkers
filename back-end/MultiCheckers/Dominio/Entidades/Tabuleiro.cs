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
        private readonly int LIMITE_MIN = 1;
        private readonly int LIMITE_MAX = 8;

        public Tabuleiro()
        {
            this.Pecas = new List<Peca>();
            this.CorTurnoAtual = Cor.BRANCA;
        }

        public List<Peca> Pecas { get; private set; }

        public Cor CorTurnoAtual { get; private set; }

        public void AdicionarPeca(Peca peca)
        {
            this.Pecas.Add(peca);
        }

        public void PosicionarInicioPartida()
        {            
            Pecas.Add(new Peca(new Point(1,1), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(3,1), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(5,1), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(7,1), Cor.BRANCA));

            Pecas.Add(new Peca(new Point(2,2), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(4,2), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(6,2), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(8,2), Cor.BRANCA));

            Pecas.Add(new Peca(new Point(1,3), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(3,3), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(5,3), Cor.BRANCA));
            Pecas.Add(new Peca(new Point(7,3), Cor.BRANCA));

            Pecas.Add(new Peca(new Point(2,6), Cor.PRETA));
            Pecas.Add(new Peca(new Point(4,6), Cor.PRETA));
            Pecas.Add(new Peca(new Point(6,6), Cor.PRETA));
            Pecas.Add(new Peca(new Point(8,6), Cor.PRETA));

            Pecas.Add(new Peca(new Point(1,7), Cor.PRETA));
            Pecas.Add(new Peca(new Point(3,7), Cor.PRETA));
            Pecas.Add(new Peca(new Point(5,7), Cor.PRETA));
            Pecas.Add(new Peca(new Point(7,7), Cor.PRETA));

            Pecas.Add(new Peca(new Point(2,8), Cor.PRETA));
            Pecas.Add(new Peca(new Point(4,8), Cor.PRETA));
            Pecas.Add(new Peca(new Point(6,8), Cor.PRETA));
            Pecas.Add(new Peca(new Point(8,8), Cor.PRETA));
        }

        public void PercorrerTabuleiro(Cor cor)
        {
            this.Pecas.ForEach(p => p.PosicoesPossiveis.RemoveRange(0, p.PosicoesPossiveis.Count));

            List<Peca> pecasDestaCor = this.Pecas.FindAll(p => p.Cor == cor);
            pecasDestaCor.ForEach(p => this.CalcularMovimentos(p));
        }

        public bool AtualizarJogada(Jogada jogada)
        {
            Peca pecaMovida = this.Pecas.FirstOrDefault(p => p.PosicaoAtual == jogada.PosicaoAntiga);
            if (pecaMovida == null)
                return false;

            if (!pecaMovida.PosicoesPossiveis.Exists(p => p == jogada.PosicaoEscolhida))
                return false;

            pecaMovida.Mover(jogada.PosicaoEscolhida);
            this.ValidarDama(pecaMovida);

            if (Math.Abs(jogada.PosicaoEscolhida.X - jogada.PosicaoAntiga.X) == 2 &&
                Math.Abs(jogada.PosicaoEscolhida.Y - jogada.PosicaoAntiga.Y) == 2)
                this.RemoverPecaInimiga(jogada);
            else
                this.AtualizarCorAtual();

            return true;
        }

        public void AplicarRodadaBonus(Jogada jogada)
        {
            Peca pecaMovida = this.Pecas.First(p => p.PosicaoAtual == jogada.PosicaoEscolhida);

            this.CalcularMovimentos(pecaMovida);
            List<Point> posicoesPossiveis = pecaMovida.PosicoesPossiveis.FindAll(p => Math.Abs(p.X - jogada.PosicaoEscolhida.X) == 2 &&
                                                                                      Math.Abs(p.Y - jogada.PosicaoEscolhida.Y) == 2);
            if (posicoesPossiveis.Count == 0)
            {
                this.AtualizarCorAtual();
                this.PercorrerTabuleiro(this.CorTurnoAtual);
                return;
            }
            this.Pecas.ForEach(p => p.PosicoesPossiveis.RemoveRange(0, p.PosicoesPossiveis.Count));
            posicoesPossiveis.ForEach(p => pecaMovida.AdicionarPosicao(p));
        }

        private void ValidarDama(Peca peca)
        {
            if ((peca.Cor == Cor.BRANCA && peca.PosicaoAtual.Y == 8) ||
                (peca.Cor == Cor.PRETA && peca.PosicaoAtual.Y == 1))
                peca.TransformarEmDama();
        }

        private bool ValidarPecaDentroTabuleiro(Point posicaoDesejada)
        {
            return posicaoDesejada.X >= LIMITE_MIN &&
                   posicaoDesejada.Y >= LIMITE_MIN &&
                   posicaoDesejada.X <= LIMITE_MAX &&
                   posicaoDesejada.Y <= LIMITE_MAX;
        }

        private bool ValidarOutraPecaNoLocal(Point posicaoDesejada)
        {
            return this.Pecas.FirstOrDefault(p => p.PosicaoAtual.X == posicaoDesejada.X &&
                                                  p.PosicaoAtual.Y == posicaoDesejada.Y) != null;
        }

        private bool ValidarPecaInimiga(Peca peca, Point posicaoDesejada)
        {
            return this.Pecas.FirstOrDefault(p => p.PosicaoAtual.X == posicaoDesejada.X &&
                                                  p.PosicaoAtual.Y == posicaoDesejada.Y).Cor != peca.Cor;
        }

        private bool ValidarERefazerMovimento(Peca peca, Point posicaoDesejada)
        {
            if(this.ValidarPecaDentroTabuleiro(posicaoDesejada))
            {
                if (this.ValidarOutraPecaNoLocal(posicaoDesejada))
                {
                    if (this.ValidarPecaInimiga(peca, posicaoDesejada))
                        return true;
                }
                else
                    peca.AdicionarPosicao(posicaoDesejada);
            }
            return false;
        }

        private void AplicarMovimentos(Peca peca, Point movimentacao)
        {
            Point posicaoDesejada = new Point(peca.PosicaoAtual.X + movimentacao.X,
                                              peca.PosicaoAtual.Y + movimentacao.Y);

            Point posicaoSalto = new Point(peca.PosicaoAtual.X + 2 * movimentacao.X,
                                           peca.PosicaoAtual.Y + 2 * movimentacao.Y);

            if (this.ValidarERefazerMovimento(peca, posicaoDesejada))
            {
                if (this.ValidarPecaDentroTabuleiro(posicaoSalto) &&
                    !this.ValidarOutraPecaNoLocal(posicaoSalto))
                {
                    peca.AdicionarPosicao(posicaoSalto);
                }
            }
        }

        private void CalcularMovimentos(Peca peca)
        {
            Point avancoDireita = new Point(1, peca.Cor == Cor.BRANCA ? 1 : -1);
            Point avancoEsquerda = new Point(-1, peca.Cor == Cor.BRANCA ? 1 : -1);

            this.AplicarMovimentos(peca, avancoDireita);
            this.AplicarMovimentos(peca, avancoEsquerda);

            if (peca.IsDama)
            {
                Point recuoDireita = new Point(1, peca.Cor == Cor.BRANCA ? -1 : 1);
                Point recuoEsquerda = new Point(-1, peca.Cor == Cor.BRANCA ? -1 : 1);

                this.AplicarMovimentos(peca, recuoDireita);
                this.AplicarMovimentos(peca, recuoEsquerda);
            }
        }

        private void RemoverPecaInimiga(Jogada jogada)
        {
            Peca pecaEliminada = this.Pecas.First(p => p.PosicaoAtual.X == jogada.PosicaoEscolhida.X +
                                                                          (jogada.PosicaoEscolhida.X > jogada.PosicaoAntiga.X ? -1 : 1) &&

                                                       p.PosicaoAtual.Y == jogada.PosicaoEscolhida.Y +
                                                                          (jogada.PosicaoEscolhida.Y > jogada.PosicaoAntiga.Y ? -1 : 1));
            this.Pecas.Remove(pecaEliminada);
        }

        private void AtualizarCorAtual()
        {
            CorTurnoAtual = (CorTurnoAtual == Cor.BRANCA ? Cor.PRETA : Cor.BRANCA);
        }
    }
}
