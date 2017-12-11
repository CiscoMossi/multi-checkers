using Dominio;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCheckers.Repositorios
{
    public class PartidaRepository
    {
        private static Partida PARTIDA_ATUAL;

        public PartidaRepository()
        {
            Usuario JogadorBrancas = new Usuario("Damke", "email@email", "senha");
            PARTIDA_ATUAL = new Partida(JogadorBrancas, "sala1");
        }

        public Partida ObterPartida()
        {
            return PARTIDA_ATUAL;
        }
    }
}
