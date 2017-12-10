using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Dominio.Entidades;

namespace Dominio
{
    public class Partida
    {
        public Partida(Usuario usuarioBrancas, string salaHash)
        {
            this.JogadorBrancas = usuarioBrancas;
            this.SalaHash = salaHash;
            this.PartidaFinalizada = false;
            this.CriarTabuleiro();
        }

        public Usuario JogadorBrancas { get; private set; }

        public Usuario JogadorPretas { get; private set; }

        // public List<Usuario> Expectadores { get; private set; }

        public Tabuleiro Tabuleiro { get; private set; }

        public string SalaHash { get; private set; }

        public bool PartidaFinalizada { get; private set; }

        private void CriarTabuleiro()
        {
            this.Tabuleiro = new Tabuleiro();
            this.Tabuleiro.PosicionarInicioPartida();
            this.Tabuleiro.PercorrerTabuleiro(Cor.BRANCA);
        }

        public bool ValidarFimJogo(Cor CorTurnoAtual)
        {
            if (this.Tabuleiro.Pecas.FirstOrDefault(p => p.Cor == CorTurnoAtual) == null)
                this.PartidaFinalizada = true;

            return this.PartidaFinalizada;
        }

        public void InserirJogadorPretas(Usuario jogadorPretas)
        {
            this.JogadorPretas = jogadorPretas;
        }

        public void EditarTabuleiro(Tabuleiro tabuleiro)
        {
            this.Tabuleiro = tabuleiro;
        }
    }
}
