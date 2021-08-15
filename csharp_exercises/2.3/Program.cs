using System;

namespace Exercício_2._3
{
    class Program
    {
        static void Main(string[] args)
        {
            Operacao menu = new Operacao();

            Console.WriteLine("1 - Soma");
            Console.WriteLine("2 - Subtração");
            Console.WriteLine("3 - Multiplicação");
            Console.WriteLine("4 - Divisão");
            Console.WriteLine("Qual operação deseja realizar?");
            menu = (Operacao)Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite o valor A: ");
            double va = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Digite o valor B: ");
            double vb = Convert.ToDouble(Console.ReadLine());

            Calculadora a = new Calculadora(va, vb);

            if (menu == Operacao.Somar)
            {
                double resultado = a.Somar();
                Console.WriteLine("O resultado da soma de {0} e {1} é {2}.", va, vb, resultado);
            }
            else if (menu == Operacao.Subtrair)
            {
                double resultado = a.Subtrair();
                Console.WriteLine("O resultado da subtração de {0} e {1} é {2}.", va, vb, resultado);
            }
            else if (menu == Operacao.Multiplicar)
            {
                double resultado = a.Multiplicar();
                Console.WriteLine("O resultado da multiplicação de {0} e {1} é {2}.", va, vb, resultado);
            }
            else
            {
                double resultado = a.Dividir();
                Console.WriteLine("O resultado da divisão de {0} e {1} é {2}.", va, vb, resultado);
            }

            Console.ReadLine();
        }
    }
}
