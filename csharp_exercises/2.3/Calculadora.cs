using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercício_2._3
{
    class Calculadora
    {
        // Atributos
        private double valorA;
        private double valorB;

        // Construtor
        public Calculadora(double valorA, double valorB)
        {
            this.valorA = valorA;
            this.valorB = valorB;
        }

        // Métodos
        public double Somar()
        {
            return valorA + valorB;
        }
        public double Subtrair()
        {
            return valorA - valorB;
        }
        public double Multiplicar()
        {
            return valorA * valorB;
        }
        public double Dividir()
        {
            return valorA / valorB;
        }
    }
}
