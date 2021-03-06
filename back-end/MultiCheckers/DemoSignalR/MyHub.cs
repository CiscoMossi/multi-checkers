﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Dominio;
using System.Threading.Tasks;
using Dominio.Entidades;
using MultiCheckers.Api.Models;
using Microsoft.AspNet.SignalR.Hubs;
using Infra.Migrations;
using Infra;

namespace MultiCheckers.Api
{
    [HubName("HubMessage")]
    public class MyHub : Hub
    {
        private static Dictionary<string, Partida> SALAS = new Dictionary<string, Partida>();
        private static List<Usuario> USUARIOS = new List<Usuario>();
        private IMultiCheckersContext contexto;
        public MyHub()
        {
            contexto = new MultiCheckersContext();
        }
        public MyHub(IMultiCheckersContext context)
        {
            contexto = context;
        }

        public override Task OnConnected()
        {
            Clients.Caller.isConnect(true);
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
            if (partida == null)
            {
                Clients.Caller.partidaInexistente("Esta partida não existe.");
                return;
            }
            Clients.Group(salaHash).buscarJogo(partida);
            if (partida.PartidaFinalizada)
            {
                Clients.Group(salaHash).fimJogo((partida.Tabuleiro.CorTurnoAtual == Cor.BRANCA ? Cor.PRETA : Cor.BRANCA).ToString());
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
                return;
            }
            if (!tabuleiro.AtualizarJogada(jogada))
            {
                return;
            }
            if (tabuleiro.Pecas.Count < numPecas)
                tabuleiro.AplicarRodadaBonus(jogada);
            else
                tabuleiro.PercorrerTabuleiro(tabuleiro.CorTurnoAtual);

            partida.EditarTabuleiro(tabuleiro);

            if (partida.ValidarFimJogo(tabuleiro.CorTurnoAtual))
            {
                return;
            }
            Clients.Group(salaHash).ativaSom("movimentacao peca");
        }

        public void InserirUsuario(string login, string salaHash)
        {
            Usuario usuario = contexto.Usuarios.FirstOrDefault(x => x.Login == login);
            Partida partida = SALAS.FirstOrDefault(s => s.Key == salaHash).Value;
            if (partida == null)
            {
                Clients.Caller.partidaInexistente("Esta partida não existe.");
                return;
            }
            usuario.InserirUserHash(Context.ConnectionId);
            usuario.InserirsalaHash(salaHash);
            USUARIOS.Add(usuario);
            string jogador = partida.InserirUsuario(usuario);
            Groups.Add(Context.ConnectionId, salaHash);
            Clients.Caller.infoJogador(jogador);
        }
        public void AtualizarJogadores(JogadorModel jogador)
        {
            Clients.Client(jogador.IdConexao).infoJogador(jogador.Funcao);
            Clients.Client(jogador.IdConexao).ativaSom("troca usuario");
            Clients.Client(jogador.IdConexao).alterarTabuleiro("Você é um jogador!");
        }
        public void FinalizarJogo(HistoricoModel historicoModel)
        {
            Usuario usuario = USUARIOS.FirstOrDefault(x => x.Login == historicoModel.LoginUsuario);
            Usuario user = contexto.Usuarios.FirstOrDefault(x => x.Id == usuario.Id);
            Partida partida = SALAS.FirstOrDefault(x => x.Key == usuario.SalaHash).Value;
            if (partida.HistoricoInserido < 2)
            {
                if (partida.HistoricoInserido == 0)
                {
                    Clients.Group(usuario.SalaHash).ativaSom("vitoria");
                }
                Historico historico = new Historico(user, historicoModel.Venceu, historicoModel.PecasRestantes, historicoModel.PecasEliminadas);
                contexto.Historicos.Add(historico);
                contexto.SaveChanges();
                partida.HistoricoInserido++;
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Usuario usuario = USUARIOS.FirstOrDefault(x => x.UserHash == Context.ConnectionId);
            if (usuario != null)
            {
                Partida partida = SALAS.FirstOrDefault(x => x.Key == usuario.SalaHash).Value;
                if (partida != null)
                {
                    JogadorModel jogador = partida.RemoverJogador(usuario);
                    if (jogador != null)
                    {
                        AtualizarJogadores(jogador);
                    }
                    if (partida.JogadorBrancas == null && partida.JogadorPretas == null && partida.Expectadores.Count == 0)
                    {
                        SALAS.Remove(usuario.SalaHash);
                        return base.OnDisconnected(stopCalled);
                    }
                }
                this.Consultar(usuario.SalaHash);
                USUARIOS.Remove(usuario);
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}