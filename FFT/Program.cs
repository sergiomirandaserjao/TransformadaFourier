using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FourierTransform
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] valores = new double[4] { 10.0, 20.0, 30.0, 40.0 };

            Complex[] fft = new Fourier().Calculate(valores);

            foreach (Complex value in fft)
            {
                Console.WriteLine("Real: {0} - Imaginário: {1}", value.Real, value.Imaginary);
            }

            Console.ReadKey();
        }
    }
}
