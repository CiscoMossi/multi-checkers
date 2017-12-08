using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Posicao
    {
        public Posicao(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public override bool Equals(object obj)
        {
            Posicao comparacao = obj as Posicao;
            return this.X == comparacao.X && this.X == comparacao.Y;
        }
    }
}
