using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Linear_Regression
{
    public class RegChart 
    {
        private List<Tuple<double, double>> tuples;
        public KeyValuePair<double, double> Coefficients { get; set; }
        private double bCoefficients;
        private double aCoefficients;
        
      
        public RegChart(List<Tuple<double, double>> tuples)
        {
            this.tuples = tuples;
            Coefficients = GetCoefficientsByTuple();
            aCoefficients = Coefficients.Key;
            bCoefficients = Coefficients.Value;
        }
        private KeyValuePair<double, double> GetCoefficientsByTuple()
        {
            double meanX = tuples.Select(x => x.Item1).Sum() / tuples.Count;
            // srednia y
            double meanY = tuples.Select(x => x.Item2).Sum() / tuples.Count;
            var b = tuples.Sum(x => (x.Item2- meanY) * (x.Item1 - meanX)) /
                    tuples.Sum(x => (x.Item1- meanX) * (x.Item1- meanX));
            var a = meanY - (b * meanX);
            return new KeyValuePair<double, double>(a, b);
        }
 
        public void PrintLineChartTuple(string SeriesName, ref Chart chart)
        {
            foreach (var key in tuples)
            {
                chart.Series[SeriesName].Points.AddXY(key.Item1, Function(key.Item1, bCoefficients, aCoefficients));
            }
        }

        private double Function(double key, double bCoefficients, double aCoefficients)
        {
            return key * bCoefficients + aCoefficients  ;
        } 
    }
}
