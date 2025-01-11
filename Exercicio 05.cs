using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashHibrida tabelaHash = new HashHibrida(5);

            int[] valores = { 10, 20, 30, 40, 50, 60 };
            foreach (int valor in valores)
            {
                tabelaHash.Inserir(valor);
            }

            Console.WriteLine("Pesquisando 30: " + tabelaHash.Pesquisar(30));
            Console.WriteLine("Pesquisando 60: " + tabelaHash.Pesquisar(60));

            tabelaHash.Remover(30);
            Console.WriteLine("Pesquisando 30 após remoção: " + tabelaHash.Pesquisar(30));
        }
    }

    class Celula
    {
        public int Elemento { get; set; }
        public Celula Prox { get; set; }

        public Celula(int elemento)
        {
            Elemento = elemento;
            Prox = null;
        }
    }

    class Lista
    {
        private Celula primeiro, ultimo;
        private int tamanho;

        public Lista()
        {
            primeiro = null;
            ultimo = null;
            tamanho = 0;
        }

        public bool Pesquisar(int x)
        {
            for (Celula tmp = primeiro; tmp != null; tmp = tmp.Prox)
            {
                if (tmp.Elemento == x)
                {
                    return true;
                }
            }
            return false;
        }

        public void Inserir(int x)
        {
            Celula nova = new Celula(x);

            if (primeiro == null)
            {
                primeiro = nova;
                ultimo = nova;
            }
            else
            {
                ultimo.Prox = nova;
                ultimo = nova;
            }

            tamanho++;
            Console.WriteLine($"Valor {x} inserido.");
        }

        public void Remover(int x)
        {
            if (primeiro == null)
            {
                Console.WriteLine($"Valor {x} não encontrado.");
                return;
            }

            if (primeiro.Elemento == x)
            {
                primeiro = primeiro.Prox;
                tamanho--;
                Console.WriteLine($"Valor {x} removido.");
                return;
            }

            Celula tmp = primeiro;
            while (tmp.Prox != null && tmp.Prox.Elemento != x)
            {
                tmp = tmp.Prox;
            }

            if (tmp.Prox == null)
            {
                Console.WriteLine($"Valor {x} não encontrado.");
            }
            else
            {
                tmp.Prox = tmp.Prox.Prox;
                if (tmp.Prox == null)
                {
                    ultimo = tmp;
                }
                tamanho--;
                Console.WriteLine($"Valor {x} removido.");
            }
        }
    }

    class HashHibrida
    {
        private Lista[] tabela;
        private Lista overflow;

        public HashHibrida(int m)
        {
            tabela = new Lista[m];
            overflow = new Lista();
            for (int i = 0; i < tabela.Length; i++)
            {
                tabela[i] = new Lista();
            }
        }

        public int CriarHash(int x)
        {
            return x % tabela.Length;
        }

        public void Inserir(int x)
        {
            int indice = CriarHash(x);

            if (tabela[indice].Tamanho < 3)
            {
                tabela[indice].Inserir(x);
            }
            else
            {
                overflow.Inserir(x);
                Console.WriteLine($"Valor {x} inserido na área de overflow.");
            }
        }

        public bool Pesquisar(int x)
        {
            int indice = CriarHash(x);

            if (tabela[indice].Pesquisar(x))
            {
                return true;
            }

            return overflow.Pesquisar(x);
        }

        public void Remover(int x)
        {
            int indice = CriarHash(x);

            if (tabela[indice].Pesquisar(x))
            {
                tabela[indice].Remover(x);
            }
            else if (overflow.Pesquisar(x))
            {
                overflow.Remover(x);
            }
            else
            {
                Console.WriteLine($"Valor {x} não encontrado.");
            }
        }
    }


}
