using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linear_Regression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

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
            RegChart testReg = new RegChart(dictionaryTest);
            Chart chart
            // act
            double a = Math.Round(testReg.Coefficients.Key, 2);
            double b = Math.Round(testReg.Coefficients.Value, 2);

            //asert

            Assert.AreEqual(b, 4.22);
            Assert.AreEqual(a, -5.93);

        }

    }
}