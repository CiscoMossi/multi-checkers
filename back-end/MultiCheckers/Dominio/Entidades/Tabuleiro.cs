using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    class Tabuleiro
    {
        private static int LIMITE_MIN = 0;
        private static int LIMITE_MAX = 8;

        public Tabuleiro()
        { }

        public List<Peca> pecas { get; private set; }
        
    }
}
