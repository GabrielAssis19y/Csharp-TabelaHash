using _06;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace _06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hash tabelaHash = new Hash(11);
            tabelaHash.Inserir(10);
            tabelaHash.Inserir(21);
            tabelaHash.Inserir(32);
            tabelaHash.Inserir(22);
            tabelaHash.Inserir(54);
            tabelaHash.Inserir(65);
            tabelaHash.Inserir(1);

            Console.WriteLine("Testando redimensionamento:");
            for (int i = 1; i < 66; i += 10)
            {
                Console.WriteLine($"Pesquisando {i}: " + tabelaHash.Pesquisar(i));
            }
            tabelaHash.Inserir(1000);
            tabelaHash.ImprimirTabela();
        }
    }
    class Celula
    {
        private int elemento;
        private Celula prox;
        public Celula(int elemento)
        {
            this.elemento = elemento;
            this.prox = null;
        }
        public Celula()
        {
            this.elemento = -1;
            this.prox = null;
        }
        public Celula Prox
        {
            get { return prox; }
            set { prox = value; }
        }
        public int Elemento
        {
            get { return elemento; }
            set { elemento = value; }
        }
    }

    class Lista
    {
        private Celula primeiro, ultimo;
        public Lista()
        {
            primeiro = new Celula();
            ultimo = primeiro;
        }
        public Celula Primeiro
        {
            get { return primeiro; }
        }

        public Celula Ultimo
        {
            get { return ultimo; }
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

            if (primeiro.Elemento == -1)
            {
                primeiro.Elemento = x;
                Console.WriteLine($"Valor {x} inserido.");
            }
            else
            {
                Celula celulanova = new Celula(x);
                ultimo.Prox = celulanova;
                ultimo = celulanova;
                Console.WriteLine($"Valor {x} inserido.");
            }
        }
        public void Remover(int x)
        {
            if (primeiro.Elemento == x)
            {
                primeiro.Elemento = -1;
                primeiro = primeiro.Prox;
                Console.WriteLine($"Valor {x} removido.");
            }
            else
            {
                Celula tmp = primeiro;
                while (tmp.Prox.Elemento != x && tmp.Prox != null)
                {
                    tmp = tmp.Prox;
                }
                Celula tmp2 = tmp.Prox;
                tmp.Prox.Elemento = -1;
                tmp.Prox = tmp2.Prox;
                tmp2.Prox = null;

                if (ultimo == tmp2)
                {
                    ultimo = tmp;
                }
                Console.WriteLine($"Valor {x} removido.");
            }
        }


    }
    class Hash
    {
        public Lista[] tabela;
        int quantidade = 0;
        public Hash(int m)
        {
            tabela = new Lista[m];
            for (int i = 0; i < m; i++)
            {
                tabela[i] = new Lista();
            }

        }
        public int CriarHash(int x)
        {
            return x % tabela.Length;
        }
        private double Ocupacao(int quantidade)
        {
            double taxa = (quantidade / tabela.Length) * 100;
            return taxa;
        }
        public void Redimensionar()
        {
            Lista[] novaTabela = new Lista[tabela.Length * 2];

            for (int i = 0; i < tabela.Length * 2; i++)
            {
                novaTabela[i] = new Lista();
            }
             
            for (int i = 0; i < tabela.Length; i++)
            {
                Celula tmp = tabela[i].Primeiro;
                while (tmp != null && tmp.Elemento != -1)
                { 
                    int Novohash = tmp.Elemento % novaTabela.Length;
                    novaTabela[Novohash].Inserir(tmp.Elemento);
                    tmp = tmp.Prox;
                }
            }
            tabela = novaTabela;
        }

        public void Inserir(int x)
        {
            if (Pesquisar(x) == true)
            {
                throw new Exception("Erro ao inserir!, valor já existente");
            }
            else if (Ocupacao(quantidade) > 75)
            {
                Redimensionar();
                Console.WriteLine("Redimensionando tabela");
                tabela[CriarHash(x)].Inserir(x);
                quantidade++;
            }
            else
            {
                tabela[CriarHash(x)].Inserir(x);
                quantidade++;
            }
        }
        public bool Pesquisar(int x)
        {
            return tabela[CriarHash(x)].Pesquisar(x);
        }

        public void Remover(int x)
        {
            if (Pesquisar(x) == false)
            {
                throw new Exception("Erro ao remover!, valor não existe");
            }
            else
            {
                tabela[CriarHash(x)].Remover(x);
            }
        }
        public void ImprimirTabela()
        {
            Console.WriteLine("tabela: ");
            for (int i = 0; i < tabela.Length; i++)
            {
                Console.Write($"Índice {i}: ");
                Celula tmp = tabela[i].Primeiro;
                while (tmp != null && tmp.Elemento != -1)
                {
                    Console.Write($"{tmp.Elemento} -> ");
                    tmp = tmp.Prox;
                }
                Console.WriteLine(" ");
            }
        }

    }
}
