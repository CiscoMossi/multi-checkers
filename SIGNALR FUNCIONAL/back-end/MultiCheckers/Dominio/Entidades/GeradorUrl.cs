using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    class GeradorUrl
    {
        public GeradorUrl() { }

        public string GerarUrl()
        {
            string URL = "";
            List<int> numeros = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            List<char> caracteres = new List<char>()
                {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

            Random randomico = new Random();
            for (int i = 0; i < 11; i++)
            {
                int random = randomico.Next(0, 3);
                if (random == 1)
                {
                    random = randomico.Next(0, numeros.Count);
                    URL += numeros[random].ToString();
                }
                else
                {
                    random = randomico.Next(0, caracteres.Count);
                    URL += caracteres[random].ToString();
                }
            }

            return URL;
        }
    }
}
