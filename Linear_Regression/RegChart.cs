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
        private Dictionary<double, double> dictionary;
        private List<Tuple<double, double>> tuples;

        public KeyValuePair<double, double> Coefficients { get; set; }
        private double bCoefficients;
        private double aCoefficients;
        
        public RegChart(Dictionary<double, double> dictionary)
        {
            this.dictionary = dictionary;
            Coefficients = GetCoefficientsByDictionary();
            aCoefficients = Coefficients.Key;
            bCoefficients = Coefficients.Value;
            
        }

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
        private  KeyValuePair<double,double> GetCoefficientsByDictionary()
        {   
            double meanX = dictionary.Select(x => x.Key).Sum() / dictionary.Count;
            // srednia y
            double meanY = dictionary.Select(x => x.Value).Sum() / dictionary.Count;
            var b = dictionary.Sum(x => (x.Value - meanY) * (x.Key - meanX)) /
                    dictionary.Sum(x => (x.Key - meanX) * (x.Key - meanX));
            var a = meanY - (b * meanX);
            return new KeyValuePair<double,double>( a, b);
        }
        public void PrintLineChartTuple(string SeriesName, ref Chart chart)
        {
            foreach (var key in tuples)
            {
                chart.Series[SeriesName].Points.AddXY(key.Item1, Function(key.Item1, bCoefficients, aCoefficients));
            }
        }
        public void PrintLineChart( string SeriesName, ref Chart chart)
        {
            Chart c = chart;
            chart.Series[0].IsVisibleInLegend = false;
            foreach (var key in dictionary)
            {
               chart.Series[SeriesName].Points.AddXY(key.Key, Function(key.Key, bCoefficients, aCoefficients));
            }
        }

        private double Function(double key, double bCoefficients, double aCoefficients)
        {
            return key * bCoefficients + aCoefficients  ;
        }

     

      
    }
}
