using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Hash tabelaHash = new Hash(5);

            
            tabelaHash.Inserir("casa");
            tabelaHash.Inserir("saco");
            tabelaHash.Inserir("as");
            tabelaHash.Inserir("saca");
            tabelaHash.Inserir("caso");

           
            Console.WriteLine("Pesquisando 'saco': " + tabelaHash.Pesquisar("saco"));
            Console.WriteLine("Pesquisando 'as': " + tabelaHash.Pesquisar("as"));

            
            tabelaHash.Remover("saca");
            Console.WriteLine("Pesquisando 'saca' após remoção: " + tabelaHash.Pesquisar("saca"));
        }
    }

    class Celula
    {
        public string Elemento { get; set; }
        public Celula Prox { get; set; }

        public Celula(string elemento)
        {
            Elemento = elemento;
            Prox = null;
        }
    }

    class Lista
    {
        private Celula primeiro, ultimo;

        public Lista()
        {
            primeiro = null;
            ultimo = null;
        }

        public bool Pesquisar(string x)
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

        public void Inserir(string x)
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

            Console.WriteLine($"Palavra '{x}' inserida.");
        }

        public void Remover(string x)
        {
            if (primeiro == null)
            {
                Console.WriteLine($"Palavra '{x}' não encontrada.");
                return;
            }

            if (primeiro.Elemento == x)
            {
                primeiro = primeiro.Prox; 
                Console.WriteLine($"Palavra '{x}' removida.");
                return;
            }

            Celula tmp = primeiro;
            while (tmp.Prox != null && tmp.Prox.Elemento != x)
            {
                tmp = tmp.Prox;
            }

            if (tmp.Prox == null) 
            {
                Console.WriteLine($"Palavra '{x}' não encontrada.");
            }
            else
            {
                tmp.Prox = tmp.Prox.Prox; 
                if (tmp.Prox == null)
                {
                    ultimo = tmp; 
                }
                Console.WriteLine($"Palavra '{x}' removida.");
            }
        }
    }

    class Hash
    {
        private Lista[] tabela;

        public Hash(int m)
        {
            tabela = new Lista[m];
            for (int i = 0; i < m; i++)
            {
                tabela[i] = new Lista();
            }
        }

        public int CriarHash(string x)
        {
            int somaAscii = 0;
            foreach (char c in x)
            {
                somaAscii += c;
            }
            return somaAscii % tabela.Length;
        }

        public void Inserir(string x)
        {
            int indice = CriarHash(x);
            if (!tabela[indice].Pesquisar(x))
            {
                tabela[indice].Inserir(x);
            }
            else
            {
                Console.WriteLine($"Erro: Palavra '{x}' já existe na tabela.");
            }
        }

        public bool Pesquisar(string x)
        {
            int indice = CriarHash(x);
            return tabela[indice].Pesquisar(x);
        }

        public void Remover(string x)
        {
            int indice = CriarHash(x);
            if (tabela[indice].Pesquisar(x))
            {
                tabela[indice].Remover(x);
            }
            else
            {
                Console.WriteLine($"Erro: Palavra '{x}' não encontrada na tabela.");
            }
        }
    }


}
