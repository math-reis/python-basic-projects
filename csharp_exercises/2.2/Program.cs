using System;

namespace Exercício_2._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite o valor A: ");
            double va = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Digite o valor B: ");
            double vb = Convert.ToDouble(Console.ReadLine());

            Calculadora a = new Calculadora(va, vb);

            Console.WriteLine("O resultado da soma de {0} e {1} é {2}.", va, vb, a.Somar());
            Console.WriteLine("O resultado da substração de {0} e {1} é {2}.", va, vb, a.Subtrair());
            Console.WriteLine("O resultado da multiplicação de {0} e {1} é {2}.", va, vb, a.Multiplicar());
            Console.WriteLine("O resultado da divisão de {0} e {1} é {2}.", va, vb, a.Dividir());

            Console.ReadLine();
        }
    }
}
