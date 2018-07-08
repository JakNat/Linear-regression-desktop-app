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

        public KeyValuePair<double, double> Coefficients { get; set; }
        private double bCoefficients;
        private double aCoefficients;
        
        public RegChart(Dictionary<double, double> dictionary)
        {
            this.dictionary = dictionary;
            Coefficients = GetB();
            aCoefficients = Coefficients.Key;
            bCoefficients = Coefficients.Value;
            
        }
        
        private  KeyValuePair<double,double> GetB()
        {   
            double meanX = dictionary.Select(x => x.Key).Sum() / dictionary.Count;
            // srednia y
            double meanY = dictionary.Select(x => x.Value).Sum() / dictionary.Count;
            var b = dictionary.Sum(x => (x.Value - meanY) * (x.Key - meanX)) /
                    dictionary.Sum(x => (x.Key - meanX) * (x.Key - meanX));
            var a = meanY - (b * meanX);
            return new KeyValuePair<double,double>( a, b);
        }

        public void PrintLineChart( string SeriesName, ref Chart chart)
        {
            Chart c = chart;

            chart.Series[0].IsVisibleInLegend = false;
            chart.Series[SeriesName].ChartType = SeriesChartType.Spline;
          //  chart.Series[SeriesName].Color = Color.Blue;
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
