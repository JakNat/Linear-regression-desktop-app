using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
        }
        double MeanX(Dictionary<double, double> list)
        {
            return list.Select(x => x.Key).Sum() / list.Count;
        }

        double MeanY(Dictionary<double, double> list)
        {
            return list.Select(x => x.Value).Sum() / list.Count;
        }
        private  KeyValuePair<double,double> GetB()
        {
            double meanX = MeanX(dictionary);
            // srednia y
            double meanY = MeanY(dictionary);
            var b = dictionary.Sum(x => (x.Value - meanY) * (x.Key - meanX)) /
                    dictionary.Sum(x => (x.Key - meanX) * (x.Key - meanX));
            var a = meanY - (b * meanX);
            return new KeyValuePair<double,double>( a, b);
        }


    }
}
