﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Dominio;
using System.Threading.Tasks;
using Dominio.Entidades;
using MultiCheckers.Api.Models;

namespace MultiCheckers.Api
{
    public class MyHub : Hub
    {
        private static Dictionary<string, Partida> SALAS = new Dictionary<string, Partida>();
        private static List<Usuario> USUARIOS = new List<Usuario>();

        public override Task OnConnected()
        {
            USUARIOS.Add(new Usuario("CheckersKing", "email@email.com", "senha"));
            USUARIOS.Add(new Usuario("Mr_Winner", "winner@email.com", "senha"));

            return base.OnConnected();
        }

        public void CriarSala()
        {
            Partida partida = new Partida();
            GeradorUrl gerador = new GeradorUrl();

            string salaHash = gerador.GerarUrl();
            SALAS.Add(salaHash, partida);

            Clients.Caller.criarSala(salaHash);
        }

        public void Consultar(string salaHash)
        {
            Partida partida = SALAS.FirstOrDefault(s => s.Key == salaHash).Value;

            Clients.Group(salaHash).buscarJogo(partida);
            if (partida.PartidaFinalizada)
            {
                Clients.All.fimJogo(String.Concat("Jogo Finalizado. ",
                                      (partida.Tabuleiro.CorTurnoAtual == Cor.BRANCA ? Cor.PRETA : Cor.BRANCA).ToString(),
                                       "S venceram."));
                return;
            }
        }

        public void Atualizar(JogadaModel jogadaModel, int cor, string salaHash)
        {
            Jogada jogada = new Jogada(jogadaModel.PosicaoEscolhida, jogadaModel.PosicaoAntiga);
            Partida partida = SALAS.FirstOrDefault(s => s.Key == salaHash).Value;
            Tabuleiro tabuleiro = partida.Tabuleiro;
            int numPecas = tabuleiro.Pecas.Count;

            if ((Cor)cor != tabuleiro.CorTurnoAtual)
            {
                Clients.Group(salaHash).alterarTabuleiro("Turno do adversário");
                return;
            }
            if (!tabuleiro.AtualizarJogada(jogada))
            {
                Clients.Group(salaHash).alterarTabuleiro("Jogada inválida");
                return;
            }
            if (tabuleiro.Pecas.Count < numPecas)
                tabuleiro.AplicarRodadaBonus(jogada);
            else
                tabuleiro.PercorrerTabuleiro(tabuleiro.CorTurnoAtual);

            partida.EditarTabuleiro(tabuleiro);

            if (partida.ValidarFimJogo(tabuleiro.CorTurnoAtual))
            {
                Clients.Group(salaHash).alterarTabuleiro("Você venceu!");
                return;
            }
            Clients.Group(salaHash).alterarTabuleiro(tabuleiro.CorTurnoAtual.ToString());
        }

        public void InserirUsuario(string login, string salaHash)
        {
            Usuario usuario = USUARIOS.FirstOrDefault(u => u.Login == login);
            Partida partida = SALAS.FirstOrDefault(s => s.Key == salaHash).Value;

            string jogador = partida.InserirUsuario(usuario);
            usuario.InserirUserHash(Context.ConnectionId);

            Clients.Group(salaHash).infoJogador(jogador);
            Groups.Add(Context.ConnectionId, salaHash);
        }
    }
}