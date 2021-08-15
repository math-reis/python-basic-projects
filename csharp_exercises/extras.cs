using System;

namespace Exercícios_extras
{
    class Program
    {
        static void Ex_1()
        {
            //Elabore um algoritmo que calcule a idade média de 5 alunos.

            int idade;
            int soma = 0;

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Insira a idade do {0}º aluno: ", i + 1);
                idade = Convert.ToInt32(Console.ReadLine());
                soma = idade + soma;
            }

            Console.WriteLine("A soma das idades é {0}.", soma);
            Console.WriteLine("A média das idades é {0}.", soma / 5.0);
        } // Verificar

        static void Ex_2()
        {
            //Crie um algoritmo que verifique se um número informado é par ou impar.

            Console.WriteLine("Insira um número intero qualquer: ");
            int num = Convert.ToInt32(Console.ReadLine());

            if (num % 2 == 0)
            {
                Console.WriteLine("O número {0} é par.", num);
            }
            else
            {
                Console.WriteLine("O número {0} é impar.", num);
            }
        } // Verificar

        static void Ex_3()
        {
            //Faça um algoritmo que exiba quantas pessoas possuem mais de 18 anos.
            //O algoritmo deverá ler a idade de 5 pessoas.

            int idade;
            int maIdade = 0;
            int meIdade = 0;

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Insira a idade da {0}ª pessoa: ", i + 1);
                idade = Convert.ToInt32(Console.ReadLine());

                if (idade >= 18)
                {
                    maIdade++;
                }
                else
                {
                    meIdade++;
                }
            }

            Console.WriteLine("Maiores de idade: {0}", maIdade);
            Console.WriteLine("Menoress de idade: {0}", meIdade);
        } // Verificar

        static void Ex_4()
        {
            //Faça um algoritmo que calcule e exiba o salário reajustado de 5 funcionários de
            //acordo com a seguinte regra: salário de até 300, reajuste de 50 %; salário maior
            //que 300, reajuste de 30 %.

            double salario;
            double maReajuste;
            double meReajuste;

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Insira {0}º salário: ", i + 1);
                salario = Convert.ToDouble(Console.ReadLine());

                if (salario <= 300)
                {
                    maReajuste = salario * 1.50;
                    Console.WriteLine("Salário reajustado em 50%: {0}", maReajuste);
                }
                else
                {
                    meReajuste = salario * 1.3;
                    Console.WriteLine("Salário reajustado em 30%: {0}", meReajuste);
                }
            }
        } // Verificar

        static void Ex_5()
        {
            //Faça um algoritmo que leia a altura e a matricula de 5 alunos. Mostre a
            //matrícula do aluno mais alto e do aluno mais baixo.

            int altura;
            int maisAlto = 0;
            int maisBaixo = int.MaxValue;
            string matrAlto = string.Empty;
            string matrBaixo = string.Empty;

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Insira a altura e a matrícula do {0}º aluno: ", i + 1);
                altura = Convert.ToInt32(Console.ReadLine());
                string matricula = Console.ReadLine();

                if (altura > maisAlto)
                {
                    maisAlto = altura;
                    matrAlto = matricula;
                }
                if (altura < maisBaixo)
                {
                    maisBaixo = altura;
                    matrBaixo = matricula;
                }
            }

            Console.WriteLine("O aluno mais alto mede: {0}", maisAlto);
            Console.WriteLine("A matrícula do aluno mais alto é: {0}", matrAlto);
            Console.WriteLine("O aluno mais baixo mede: {0}", maisBaixo);
            Console.WriteLine("A matrícula do aluno mais baixo é: {0}", matrBaixo);
        } // Verificar

        static void Main(string[] args)
        {
            Ex_1();
            Console.ReadLine();
        }
    }
}
