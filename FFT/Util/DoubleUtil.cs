using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace FourierTransform
{
    /// <summary>
    /// Classe que expande as funcionalidades dos números do tipo Double.
    /// </summary>
    public static class DoubleUtil
    {
        /// <summary>
        /// Converte o array de doubles em um array de números complexos.
        /// </summary>
        /// <param name="data">Array de doubles que será convertido.</param>
        /// <returns>Retorna um array de números complexos gerado a partir
        /// do array de doubles.</returns>
        public static Complex[] ToComplexArray(this double[] data)
        {
            return data.Select(a => new Complex(a, 0)).ToArray();
        }
    }
}