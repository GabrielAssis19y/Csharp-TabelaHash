using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01
{
    class Program
    {
        static void Main(string[] args)
        {
            Hash tabelaHash = new Hash(5);
            tabelaHash.Inserir(2);
            tabelaHash.Inserir(5);
            tabelaHash.Inserir(21);
            tabelaHash.Inserir(15);
            tabelaHash.Inserir(8);
            tabelaHash.Inserir(67);
            Console.WriteLine(tabelaHash.Pesquisar(5));
            Console.WriteLine(tabelaHash.Pesquisar(67));
            tabelaHash.Remover(67);
            Console.WriteLine(tabelaHash.Pesquisar(67));
            tabelaHash.Remover(2);

        }
    }

    class Hash
    {
        private int[] vetor;
        private int[] areareserva;
        int vazio = -1;
        int indicereserva = 0;
        public Hash(int num)
        {

            areareserva = new int[num / 3];
            vetor = new int[num];
            for (int i = 0; i < vetor.Length; i++)
            {
                vetor[i] = vazio;
            }
            for (int i = 0; i < areareserva.Length; i++)
            {
                areareserva[i] = vazio;
            }
        }
        public int CriarHash(int x)
        {
            return x % vetor.Length;
        }

        public bool Pesquisar(int x)
        {
            if (vetor[CriarHash(x)] == x)
            {
                return true;
            }
            else if (vetor[CriarHash(x)] == vazio)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < areareserva.Length; i++)
                {
                    if (areareserva[i] == x)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public void Inserir(int x)
        {
            if (Pesquisar(x))
            {
                if (indicereserva < areareserva.Length)
                {
                    areareserva[indicereserva] = x;
                    indicereserva++;
                    Console.WriteLine("Valor adicionado na área reserva");
                }
                else
                {
                    Console.WriteLine($"Erro ao inserir {x}! Reserva cheia.");
                }
            }
            else
            {
                if (vetor[CriarHash(x)] == vazio)
                {
                    vetor[CriarHash(x)] = x;
                    Console.WriteLine($"Valor {x} inserido");
                }
                else
                {
                    if (indicereserva < areareserva.Length)
                    {
                        areareserva[indicereserva] = x;
                        indicereserva++;
                        Console.WriteLine("Valor adicionado na área reserva");
                    }
                    else
                    {
                        Console.WriteLine($"Erro ao inserir {x}! Reserva cheia.");
                    }
                }
            }
        }
        public void Remover(int x)
        {
            if (Pesquisar(x))
            {
                if (vetor[CriarHash(x)] == x)
                {
                    vetor[CriarHash(x)] = vazio;
                    Console.WriteLine($"Valor {x} removido");
                }
                else
                {
                    for (int i = 0; i < areareserva.Length; i++)
                    {
                        if (areareserva[i] == x)
                        {
                            areareserva[i] = vazio;
                            Console.WriteLine($"Valor {x} removido");
                            break;
                        }
                    }
                }
                
            }
            else
            {
                throw new Exception("Erro ao remover!");
            }
        }
    }
}

