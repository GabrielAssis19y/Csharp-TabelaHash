using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hash tabelaHash = new Hash(5);
            tabelaHash.Inserir(2);
            tabelaHash.Inserir(5);
            tabelaHash.Inserir(21);
            tabelaHash.Inserir(15);
            tabelaHash.Inserir(8);
            Console.WriteLine(tabelaHash.Pesquisar(5));
            tabelaHash.Remover(5);
            Console.WriteLine(tabelaHash.Pesquisar(5));
            tabelaHash.Inserir(7);

        }
    }
    class Hash
    {
        private int[] vetor;
        int vazio = -1;
        public Hash(int num)
        {
            vetor = new int[num];
            for (int i = 0; i < vetor.Length; i++)
            {
                vetor[i] = vazio;
            }
        }
        public int CriarHash(int x)
        {
            return x % vetor.Length;
        }
        public int Rehash(int x)
        {
            return ++x % vetor.Length;
        }
        public int Pesquisar(int x)
        {
            int resp = vazio;
            if (vetor[CriarHash(x)] == x)
            {
                return resp = CriarHash(x);
            }
            else if (vetor[Rehash(x)] == x)
            {
                return resp = Rehash(x);
            }
            return resp;

        }
        public void Inserir(int x)
        {
            if (vetor[CriarHash(x)] == vazio)
            {
                vetor[CriarHash(x)] = x;
                Console.WriteLine($"valor {x} adicionado");
            }
            else
            {
                if (vetor[Rehash(x)] == vazio)
                {
                    vetor[Rehash(x)] = x;
                    Console.WriteLine($"valor {x} adicionado");
                }
                else
                {
                    Console.WriteLine($"Não foi possível adicionar o valor {x}");
                }
            }

        }
        public void Remover(int x)
        {
            int indice = Pesquisar(x);
            if (indice != vazio)
            {
                vetor[indice] = vazio;
                Console.WriteLine($"valor {x} removido");
            }
            else
            {
                throw new Exception("Erro ao remover!");
            }
        }
    }
}
