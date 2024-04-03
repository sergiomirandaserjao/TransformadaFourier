using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace FourierTransform
{
    /// <summary>
    /// Classe responsável por calcular a transformada de Fourier para a grandeza informada.
    /// Esta classe foi construída utilizando como base os algoritmos da biblioteca Math.NET.
    /// Disponível neste endereço: https://github.com/mathnet
    /// </summary>
    public partial class Fourier
    {
        /// <summary>
        /// Calcula a transformada de Fourier para a grandeza.
        /// </summary>
        /// <param name="quantity">Array contendo as amostras da grandeza informada.</param>
        /// <returns>Array contendo os valores transformados.</returns>
        public Complex[] Calculate(double[] quantity)
        {
            return this.Calculate(quantity.ToComplexArray());
        }

        /// <summary>
        /// Calcula a transformada de Fourier para a grandeza.
        /// </summary>
        /// <param name="quantity">Array contendo as amostras da grandeza informada.</param>
        /// <returns>Array contendo os valores transformados.</returns>
        public Complex[] Calculate(Complex[] quantity)
        {
            int quantityLength = quantity.Length;

            Complex[] resultArray = new Complex[quantityLength];

            // Calcula os fatores para cada índice do array.
            Complex[] factors = this.CalculateFactors(quantityLength);

            // Calcula o primeiro array auxiliar.
            Complex[] firstAuxiliarArray = this.CalculateAuxiliarArray(factors);

            // Calcula o primeiro array auxiliar.
            Complex[] secondAuxiliarArray = this.CalculateSecondAuxiliarArray(factors, quantity);

            var nbinv = 1.0 / quantityLength.GetClosestPowerOfTwo();

            for (var i = 0; i < quantityLength; i++)
            {
                resultArray[i] = nbinv * (new Complex(factors[i].Real, -factors[i].Imaginary)) * secondAuxiliarArray[i];
            }

            return resultArray;
        }

        /// <summary>
        /// Calcula os fatores de multiplicação para cada índice do array.
        /// </summary>
        /// <param name="quantityLength">Quantidade de amostras da grandeza.</param>
        /// <returns>Array contendo o seno e o cosseno para cada índice do array.</returns>
        private Complex[] CalculateFactors(int quantityLength)
        {
            Complex[] factors = new Complex[quantityLength];

            for (var k = 0; k < quantityLength; k++)
            {
                var value = (((Math.PI / quantityLength) * k) * k);
                factors[k] = new Complex(Math.Cos(value), Math.Sin(value));
            }

            return factors;
        }

        /// <summary>
        /// Calcula o primeiro array auxiliar.
        /// Este array será utilizado para inverter a ordem dos índices
        /// para que seja possível calcular a transformada de Fourier.
        /// </summary>
        /// <param name="factors"></param>
        /// <returns></returns>
        private Complex[] CalculateAuxiliarArray(Complex[] factors)
        {
            int powerOfTwo = factors.Length.GetClosestPowerOfTwo();
            int quantityLength = factors.Length;

            Complex[] auxiliarArray = new Complex[powerOfTwo];

            // Adiciona as amostras da grandeza no array auxiliar criado.
            for (var i = 0; i < quantityLength; i++)
            {
                auxiliarArray[i] = factors[i];
            }

            for (var i = powerOfTwo - quantityLength + 1; i < powerOfTwo; i++)
            {
                auxiliarArray[i] = factors[powerOfTwo - i];
            }

            this.CalculateRadix2(auxiliarArray, -1);

            return auxiliarArray;
        }

        /// <summary>
        /// Calcula o segundo array auxiliar.
        /// Este array será utilizado para multiplicar os índices pela
        /// número complexo conjugado.
        /// </summary>
        /// <param name="factors">Fatores de multiplicão de cada índice do array de amostras.</param>
        /// <returns>Array contendo o produto da multiplicação das amostras pela conjugação do número complexo.</returns>
        private Complex[] CalculateSecondAuxiliarArray(Complex[] factors, Complex[] quantity)
        {
            int powerOfTwo = factors.Length.GetClosestPowerOfTwo();
            int quantityLength = quantity.Length;

            Complex[] secondAuxiliarArray = new Complex[powerOfTwo];

            // Adiciona as amostras da grandeza no array auxiliar criado.
            for (var i = 0; i < quantityLength; i++)
            {
                secondAuxiliarArray[i] = (new Complex(factors[i].Real, -factors[i].Imaginary)) * quantity[i]; ;
            }

            this.CalculateRadix2(secondAuxiliarArray, -1);

            return secondAuxiliarArray;
        }

        private Complex[] MultiplicateArrays(Complex[] firstArray, Complex[] secondArray)
        {
            for (var i = 0; i < firstArray.Length; i++)
            {
                secondArray[i] *= firstArray[i];
            }

            this.CalculateRadix2(secondArray, 1);

            return secondArray;
        }

        /// <summary>
        /// Reordena o array de valores para calcular a FFT.
        /// </summary>
        /// <param name="data">Array contendo os dados do sinal digital.</param>
        /// <returns>Array de complexos reordenada.</returns>
        private void ReorderArray(Complex[] data)
        {
            Complex[] dataReordered = data;

            var j = 0;

            for (var i = 0; i < data.Length - 1; i++)
            {
                if (i < j)
                {
                    var temp = data[i];
                    dataReordered[i] = data[j];
                    dataReordered[j] = temp;
                }

                var m = dataReordered.Length;

                do
                {
                    m >>= 1;
                    j ^= m;
                }
                while ((j & m) == 0);
            }
        }

        /// <summary>
        /// Executa o algoritmo Radix2 para calcular os valores reais e imaginários.
        /// </summary>
        /// <param name="data">Array contendo os dados do sinal digital.</param>
        /// <param name="exponentSign">Expoente utilizado na fórmula.</param>
        /// <param name="index">Índice atual do array.</param>
        /// <param name="k"></param>
        private void CalculateRadixSteps(Complex[] data, int exponentSign, int index, int k)
        {
            Complex[] dataCalculated = data;

            var exponent = (exponentSign * k) * Math.PI / index;

            var w = new Complex(Math.Cos(exponent), Math.Sin(exponent));

            var step = index << 1;

            for (var i = k; i < data.Length; i += step)
            {
                var ai = data[i];
                var t = w * data[i + index];

                dataCalculated[i] = ai + t;
                dataCalculated[i + index] = ai - t;
            }
        }

        /// <summary>
        /// Varre os registros do array executando o algoritmo Radix2 para cada índice.
        /// </summary>
        /// <param name="data">Array contendo os dados do sinal digital.</param>
        /// <param name="exponentSign">Expoente utilizado na fórmula.</param>
        private void CalculateRadix2(Complex[] data, int exponentSign)
        {
            this.ReorderArray(data);

            for (var levelSize = 1; levelSize < data.Length; levelSize *= 2)
            {
                for (var k = 0; k < levelSize; k++)
                {
                    CalculateRadixSteps(data, exponentSign, levelSize, k);
                }
            }
        }
    }
}