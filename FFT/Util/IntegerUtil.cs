using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourierTransform
{
    /// <summary>
    /// Classe que expande as funcionalidades dos números do tipo Integer.
    /// </summary>
    public static class IntegerUtil
    {
        /// <summary>
        /// Obtém o número inteiro perfeito mais próximo do valor atual.
        /// Este cálculo é necessário pois é preciso duplicar o array de tamanho.
        /// </summary>
        /// <param name="data">Quantidade de amostras da grandeza.</param>
        /// <returns>Inteiro perfeito mais próximo do valor informado.</returns>
        public static int GetClosestPowerOfTwo(this int value)
        {
            value = (value << 1) - 1;

            value--;

            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;

            return value + 1;
        }
    }
}
