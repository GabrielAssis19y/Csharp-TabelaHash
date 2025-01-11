using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace _03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hash tabelaHash = new Hash(5);

            tabelaHash.Inserir(5);
            tabelaHash.Inserir(15);
            tabelaHash.Inserir(18);
            tabelaHash.Inserir(20);
            tabelaHash.Inserir(4);
            

            Console.WriteLine("Pesquisando 15: " + tabelaHash.Pesquisar(15));
            tabelaHash.Remover(15);
            Console.WriteLine("Pesquisando 15 após remoção: " + tabelaHash.Pesquisar(15));
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
        public void Inserir (int x)
        {

            if(primeiro.Elemento == -1)
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
        public void Remover (int x)
        {
            if (primeiro.Elemento == x)
            {
                primeiro.Elemento = -1;
                Console.WriteLine($"Valor {x} removido.");
            }
            else
            {
                Celula tmp = primeiro;
                while(tmp.Prox.Elemento != x && tmp.Prox != null)
                {
                    tmp = tmp.Prox;
                }
                Celula tmp2 = tmp.Prox;
                tmp.Prox.Elemento = -1;
                tmp.Prox = tmp2.Prox;
                tmp2.Prox = null;

                if(ultimo == tmp2)
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
        public void Inserir(int x)
        {
            if (Pesquisar(x) == true)
            {
                throw new Exception("Erro ao inserir!, valor já existente");
            }
            else
            {
                tabela[CriarHash(x)].Inserir(x);
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
    }
    
}
