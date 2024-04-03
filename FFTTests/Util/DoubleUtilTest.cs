using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace FourierTransform
{
    [TestClass]
    public class DoubleUtilTest
    {
        private double[] _arrayDouble;
        private Complex[] _arrayComplex;

        [TestInitialize]
        public void Setup()
        {
            Arrange();
            Act();
        }

        private void Arrange()
        {
            this._arrayDouble = new double[4] { 10.0, 20.0, 30.0, 40.0 };
        }

        private void Act()
        {
            this._arrayComplex = this._arrayDouble.ToComplexArray();
        }

        [TestMethod]
        public void Verifica_Se_O_Array_De_Saida_Tem_O_Mesmo_Tamanho_Do_Array_De_Entrada()
        {
            Assert.AreEqual(this._arrayDouble.Length, this._arrayComplex.Length);
        }

        [TestMethod]
        public void Verifica_Se_Os_Valores_Do_Array_De_Saida_Estao_Corretos()
        {
            Assert.AreEqual(this._arrayComplex[0], new Complex(10.0, 0));
            Assert.AreEqual(this._arrayComplex[1], new Complex(20.0, 0));
            Assert.AreEqual(this._arrayComplex[2], new Complex(30.0, 0));
            Assert.AreEqual(this._arrayComplex[3], new Complex(40.0, 0));
        }
    }
}
