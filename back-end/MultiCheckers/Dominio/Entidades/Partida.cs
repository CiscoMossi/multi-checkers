using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Dominio.Entidades;
using MultiCheckers.Api.Models;

namespace Dominio
{
    public class Partida
    {
        public Partida()
        {
            this.PartidaFinalizada = false;
            this.CriarTabuleiro();
            this.Expectadores = new List<Usuario>();
        }

        public Usuario JogadorBrancas { get; private set; }

        public Usuario JogadorPretas { get; private set; }

        public List<Usuario> Expectadores { get; private set; }

        public Tabuleiro Tabuleiro { get; private set; }

        public bool PartidaFinalizada { get; private set; }

        private void CriarTabuleiro()
        {
            this.Tabuleiro = new Tabuleiro();
            this.Tabuleiro.PosicionarInicioPartida();
            this.Tabuleiro.PercorrerTabuleiro(Cor.BRANCA);
        }

        public bool ValidarFimJogo(Cor CorTurnoAtual)
        {
            if (this.Tabuleiro.Pecas.FirstOrDefault(p => p.Cor == CorTurnoAtual) == null ||
                this.Tabuleiro.Pecas.Sum(p => p.PosicoesPossiveis.Count) < 1)
                this.PartidaFinalizada = true;

            return this.PartidaFinalizada;
        }

        public string InserirUsuario(Usuario usuario)
        {
            if (this.JogadorBrancas == null)
            {
                this.JogadorBrancas = usuario;
                return "BRANCAS";
            }              
            else if (this.JogadorBrancas != null &&
                    this.JogadorPretas == null)
            {
                this.JogadorPretas = usuario;
                return "PRETAS";
            }               
            else
                this.Expectadores.Add(usuario);
            return "EXPECTADOR";
        }

        public void EditarTabuleiro(Tabuleiro tabuleiro)
        {
            this.Tabuleiro = tabuleiro;
        }

        public void SetPartidaFinalizada(bool partida)
        {
            this.PartidaFinalizada = partida;
        }

        public void TransformaJogadorNulo(Usuario usuario)
        {
            if (this.JogadorBrancas == usuario)
            {
                this.JogadorBrancas = null;
            }
            if (this.JogadorPretas == usuario)
            {
                this.JogadorPretas = null;
            }
        }

        public JogadorModel RemoverJogador(Usuario usuario)
        {
            if (Expectadores.Count > 0 && (this.JogadorBrancas == usuario || this.JogadorPretas == usuario))
            {
                this.TransformaJogadorNulo(usuario);                 
                string funcao = this.InserirUsuario(this.Expectadores[0]);
                string idConexao = this.Expectadores[0].UserHash;
                this.Expectadores.RemoveAt(0);
                return new JogadorModel(idConexao, funcao);
            }else if (Expectadores.Count == 0)
            {
                this.TransformaJogadorNulo(usuario);
                return null;
            }
            this.Expectadores.Remove(usuario);
            return null;
        }
    }
}
