using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace FourierTransform
{
    [TestClass]
    public class IntegerUtilTest
    {
        private int _quantityLengthA;
        private int _quantityLengthB;
        private int _quantityLengthC;

        private int _nextPerfectIntegerA;
        private int _nextPerfectIntegerB;
        private int _nextPerfectIntegerC;

        [TestInitialize]
        public void Setup()
        {
            Arrange();
            Act();
        }

        private void Arrange()
        {
            this._quantityLengthA = 7;
            this._quantityLengthB = 40;
            this._quantityLengthC = 64;
        }

        private void Act()
        {
            this._nextPerfectIntegerA = this._quantityLengthA.GetClosestPowerOfTwo();
            this._nextPerfectIntegerB = this._quantityLengthB.GetClosestPowerOfTwo();
            this._nextPerfectIntegerC = this._quantityLengthC.GetClosestPowerOfTwo();
        }

        [TestMethod]
        public void Verifica_Se_Os_Inteiros_Perfeitos_Encontrados_Estao_Corretos()
        {
            Assert.AreEqual(16, this._nextPerfectIntegerA);
            Assert.AreEqual(128, this._nextPerfectIntegerB);
            Assert.AreEqual(128, this._nextPerfectIntegerC);
        }

        [TestMethod]
        public void Verifica_Se_Os_Inteiros_Perfeitos_Encontrados_Sao_Maiores_Que_A_Potencia_De_Dois_Do_Tamanho_Atual_Do_Array()
        {
            Assert.IsTrue(this._nextPerfectIntegerA >= (2 * this._quantityLengthA));
            Assert.IsTrue(this._nextPerfectIntegerB >= (2 * this._quantityLengthB));
            Assert.IsTrue(this._nextPerfectIntegerC >= (2 * this._quantityLengthC));
        }
    }
}
