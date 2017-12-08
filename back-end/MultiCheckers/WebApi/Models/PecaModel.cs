using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class PecaModel
    {
        public PecaModel(Point posicaoInicial, int cor, bool isDama)
        {
            this.PosicaoAtual = posicaoInicial;
            this.Cor = (Cor) cor;
            this.IsDama = isDama;
        }

        public Point PosicaoAtual { get; set; }

        public Cor Cor { get; set; }

        public bool IsDama { get; set; }
    }

}