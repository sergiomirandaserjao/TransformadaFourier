using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FourierTransform
{
    [TestClass]
    public class FourierTest
    {
        private Fourier _instance;
        private MethodInfo _calculateFactors;
        private MethodInfo _calculateAuxiliarArray;
        private BindingFlags _bindingFlags;

        private double[] _arrayDouble;
        private Complex[] _arrayComplex;
        private Complex[] _arrayFFT;
        private Complex[] _arrayFactors;
        private Complex[] _arrayAuxiliar;
        private int _nextPerfectInteger;

        [TestInitialize]
        public void Setup()
        {
            Arrange();
            Act();
        }

        private void Arrange()
        {
            this._arrayDouble = new double[4] { 10.0, 20.0, 30.0, 40.0 };
            this._instance = new Fourier();
            this._bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            this._calculateFactors = typeof(Fourier).GetMethod("CalculateFactors", this._bindingFlags);
            this._calculateAuxiliarArray = typeof(Fourier).GetMethod("CalculateAuxiliarArray", this._bindingFlags);
        }

        private void Act()
        {
            this._arrayComplex = this._arrayDouble.ToComplexArray();
            this._nextPerfectInteger = this._arrayComplex.Length.GetClosestPowerOfTwo();
            this._arrayFFT = this._instance.Calculate(this._arrayDouble);
            this._arrayFactors = (Complex[])this._calculateFactors.Invoke(this._instance, new object[] { this._arrayComplex.Length });
            this._arrayAuxiliar = (Complex[])this._calculateAuxiliarArray.Invoke(this._instance, new object[] { this._arrayFactors });
        }

        [TestMethod]
        public void Verifica_Se_O_Array_De_Saida_Tem_O_Mesmo_Tamanho_Do_Array_De_Entrada()
        {
            Assert.AreEqual(this._arrayDouble.Length, this._arrayFFT.Length);
        }

        #region CalculateFactors

        [TestMethod]
        public void Verifica_Se_O_Quant_De_Fatores_Calculados_Eh_Igual_Ao_Tamanho_Do_Array()
        {
            Assert.AreEqual(this._arrayComplex.Length, this._arrayFactors.Length);
        }

        [TestMethod]
        public void Verifica_Se_Os_Valores_Dos_Fatores_Foram_Calculados_Corretamente()
        {
            Assert.AreEqual(new Complex(1, 0).ToString(), this._arrayFactors[0].ToString());
            Assert.AreEqual(new Complex(0.707106781186548, 0.707106781186547).ToString(), this._arrayFactors[1].ToString());
        }

        #endregion

        #region CalculateAuxiliarArray

        [TestMethod]
        public void Verifica_Se_O_Array_Auxiliar_De_Saida_Tem_O_Mesmo_Tamanho_Do_Array_De_Entrada()
        {
            Assert.AreEqual(this._nextPerfectInteger, this._arrayAuxiliar.Length);
        }

        #endregion
    }
}
