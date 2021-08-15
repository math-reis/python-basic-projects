using System;

namespace Exercício_2._1
{
    class Program
    {
        static void Main(string[] args)
        {
            string nome = Environment.UserName;

            Console.WriteLine("Bem-vindo {0}, agora são {1:HH:mm:ss}.", nome, DateTime.Now);

            Console.WriteLine("Digite o 1º valor para a soma: ");
            double va = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Digite o 2º valor para a soma: ");
            double vb = Convert.ToDouble(Console.ReadLine());

            double soma = va + vb;

            Console.WriteLine("{0}, o resultado da operação é {1}.", nome, soma);

            Console.ReadLine();
        }
    }
}
