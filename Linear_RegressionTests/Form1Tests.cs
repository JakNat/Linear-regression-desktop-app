using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linear_Regression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Linear_Regression.Tests
{
    [TestClass()]
    public class Form1Tests
    {
        [TestMethod()]
        public void RegressionCoefficients()
        {
            // arrange
            Dictionary<double, double> dictionaryTest = new Dictionary<double, double>();
            dictionaryTest.Add(1, 1);
            dictionaryTest.Add(2, 4);
            dictionaryTest.Add(3, 6);
            dictionaryTest.Add(4, 10);
            dictionaryTest.Add(5, 12);
            dictionaryTest.Add(6, 16);
            dictionaryTest.Add(7, 22);
            dictionaryTest.Add(8, 32);
            dictionaryTest.Add(9, 33);
            dictionaryTest.Add(10, 37);
            RegChart testReg = new RegChart(dictionaryTest);
            // act
            double a = Math.Round(testReg.Coefficients.Key, 2);
            double b = Math.Round(testReg.Coefficients.Value, 2);

            //asert

            Assert.AreEqual(b, 4.22);
            Assert.AreEqual(a, -5.93);

        }
        [TestMethod()]
        public void Regression()
        {
            // arrange
            Dictionary<double, double> dictionaryTest = new Dictionary<double, double>();
            dictionaryTest.Add(1, 1);
            dictionaryTest.Add(2, 4);
            dictionaryTest.Add(3, 6);
            dictionaryTest.Add(4, 10);
            dictionaryTest.Add(5, 12);
            dictionaryTest.Add(6, 16);
            dictionaryTest.Add(7, 22);
            dictionaryTest.Add(8, 32);
            dictionaryTest.Add(9, 33);
            dictionaryTest.Add(10, 37);

            List<Tuple<double, double>> tuplesTest = new List<Tuple<double, double>>();
            tuplesTest.Add(new Tuple<double, double>(1, 1));
            tuplesTest.Add(new Tuple<double, double>(2, 4));
            tuplesTest.Add(new Tuple<double, double>(3, 6));
            tuplesTest.Add(new Tuple<double, double>(4, 10));
            tuplesTest.Add(new Tuple<double, double>(5, 12));
            tuplesTest.Add(new Tuple<double, double>(6, 16));
            tuplesTest.Add(new Tuple<double, double>(7, 22));
            tuplesTest.Add(new Tuple<double, double>(8, 32));
            tuplesTest.Add(new Tuple<double, double>(9, 33));
            tuplesTest.Add(new Tuple<double, double>(10, 37));
;


            // act
            RegChart dictionaryReg = new RegChart(dictionaryTest);
            RegChart tuplesReg = new RegChart(tuplesTest);

            //asert

            Assert.AreEqual(dictionaryReg.Coefficients, tuplesReg.Coefficients);
        

        }

    }
}