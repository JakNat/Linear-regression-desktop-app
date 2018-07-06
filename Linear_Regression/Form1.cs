using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Linear_Regression
{
    public partial class Form1 : Form
    {
        private bool b1 = false;
        private bool b2 = false;
        private bool b3 = false;
        private List<TrackBar> trackBars; 
        private double x11 = -1;
        private double new_x11 = -1;
        private double x12 = -2;
        private double new_x12 = -1;

        private Dictionary<double, double> list;
        public Form1()
        {
           
            InitializeComponent();

         
        }
   
        private void Form1_Load(object sender, EventArgs e)
        {
            trackBars = new List<TrackBar> {
                trackBar5,
                trackBar6,
                trackBar7,
                trackBar8,
                trackBar9,
                trackBar10,
                trackBar11,
                trackBar12,
                trackBar13,
                trackBar14,
                trackBar15
                };

            foreach (var trackbar in trackBars)
            {
                trackbar.Scroll += new EventHandler(trackBarr_Scroll);
            }
            trackBar3_Scroll(sender, e);
            checkBox1_CheckedChanged(sender, e);
            Chartconfiguration();  
            button1_Click_1(sender, e);
            //      trackBar1_Scroll(sender, e);
            //    trackBar2_Scroll(sender, e); trackBar5.Scroll += new EventHandler(trackBar6_Scroll);
           


        }

    
        double Function(double value, double b, double a)
        {
            return value * b + a;
        }
        double MeanX(Dictionary<double,double> list)
        {
            return list.Select(x => x.Key).Sum() / list.Count;
        }

        double MeanY(Dictionary<double, double> list)
        {
            return list.Select(x => x.Value).Sum() / list.Count;
        }
        double FactorB(Dictionary<double, double> list)
        {  // srednia x
            double meanX = MeanX(list);
            // srednia y
            double meanY = MeanY(list);          
           return list.Sum(x => (x.Value - meanY)*(x.Key - meanX)) /
                   list.Sum(x => (x.Key - meanX) * (x.Key - meanX));
        }
         double FactorA(Dictionary<double,double> list)
        {
            double meanX = MeanX(list);
            double meanY = MeanY(list);
            double b = FactorB(list);
            return meanY - (b * meanX);
        }
        void Chartconfiguration()
        {
           
        
            chart1.Series.Add("Wartość");
            ChartPointConfiguration("Wartość",SeriesChartType.Point, Color.Black, 8, true);
            chart1.Series.Add("Wartość 11");
            ChartPointConfiguration("Wartość 11", SeriesChartType.Point, Color.Blue, 8, true);
            chart1.Series.Add("Wartość 12");
            ChartPointConfiguration("Wartość 12", SeriesChartType.Point, Color.Red, 9, true);
            chart1.Series.Add("REGRESJA");
            ChartPointConfiguration("REGRESJA", SeriesChartType.Spline, Color.Black, 9, true);
            chart1.Series.Add("REGRESJA 1");
            ChartPointConfiguration("REGRESJA 1", SeriesChartType.Spline, Color.Blue, 20, true);
            chart1.Series.Add("REGRESJA 2");
            ChartPointConfiguration("REGRESJA 2", SeriesChartType.Spline, Color.Red, 9, true);


            var chart = chart1.ChartAreas[0];
            chart.AxisX.IntervalType = DateTimeIntervalType.Number;

            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.IsEndLabelVisible = true;

            chart.AxisX.Minimum = 0;
            chart.AxisX.Maximum = 100;

            chart.AxisX.Interval = 12;
            chart.AxisY.Interval = 20;

        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            ClearChart("REGRESJA");
            ClearChart("Wartość");

            this.list = null;
            List<double> listX = new List<double>();
            List<double> listY = new List<double>();
            for (int i = 0; i < 10; i++)
            {
                listX.Add((i + 1) * 10);
            }
                foreach(var trackbar in trackBars)
                {
                    listY.Add(double.Parse(trackbar.Value.ToString()));
                }
                
            Dictionary<double,double> list = new Dictionary<double,double>();
                list.Add(0, 0);
                for (int i = 0; i < listY.Count - 1; i++)
                {
                    list.Add(listX[i], listY[i]);
                }
      
                this.list = list;
            RegChart baseRegChart = new RegChart(list);
                double b = FactorB(list);
                double a = FactorA(list);
                printLineChart(this.list, "REGRESJA", baseRegChart.Coefficients.Value, baseRegChart.Coefficients.Key);
                chart1.Series["REGRESJA"].Color = Color.Black;
            chart1.Series[0].IsVisibleInLegend = false;
            foreach (var key in list)
            {
                chart1.Series["Wartość"].Points.AddXY(key.Key, key.Value);
            }
            try
            {
            }
            catch (Exception)
            {
                return;
            }


        }
        void ChartPointConfiguration(string name, SeriesChartType type ,Color color, int MarkerSize, bool isVisible)
        {
            chart1.Series[name].ChartType = type;
            chart1.Series[name].Color= color;
            chart1.Series[name].MarkerSize = MarkerSize;
            chart1.Series[name].IsVisibleInLegend = isVisible;

        }
        private void button2_Click2(object sender, EventArgs e,ref TextBox xBox,ref TextBox yBox,ref TrackBar xBar,ref TrackBar yBar, string regName, string valueName,ref  bool bol, ref double newX ,ref double oldX)
        {
            bol = true;
            double X = double.Parse(xBox.Text);
            newX = X;
            double Y = double.Parse(yBox.Text);
            ClearChart(regName);
            ClearChart(valueName);
            if (bol)
            {
                this.list.Remove(oldX);
            }
            if (this.b2)
            {
                this.list.Remove(this.x12);
            }
            if ((int)X >= xBar.Minimum && (int)X <= xBar.Maximum)
            {
                xBar.Value = (int)X;
            }
            if ((int)Y >= yBar.Minimum && (int)Y <= yBar.Maximum)
            {
                yBar.Value = (int)Y;
            }
            oldX = newX;
           
            this.list.Add(X, Y);
            this.list = this.list.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            this.list.Count();
            double b = FactorB(this.list);
            double a = FactorA(this.list);
            chart1.Series[valueName].Points.AddXY(X, Y);
            printLineChart(this.list, regName, b, a);
        
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (double.Parse(textBox13.Text) % 10 == 0 || double.Parse(textBox13.Text) == double.Parse(textBox14.Text))
            {
                return;
            }
            button2_Click2(sender, e, ref textBox13, ref textBox11, ref trackBar4, ref trackBar1,  "REGRESJA 1", "Wartość 11", ref this.b1, ref this.new_x11, ref this.x11);
            this.b2 = false;
            if (b3)
            {
                button3_Click_1(sender, e);
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (double.Parse(textBox14.Text) % 10 == 0 || double.Parse(textBox13.Text) == double.Parse(textBox14.Text))
            {
                return;
            }
            button2_Click2(sender, e, ref textBox14, ref textBox12, ref trackBar15, ref trackBar2, "REGRESJA 2", "Wartość 12", ref this.b2, ref this.new_x12, ref this.x12);
            this.b3 = true;
        }

        private void ClearChart(string name)
        {
            chart1.Series[name].Points.Clear();
        }

        void printLineChart(Dictionary<double, double> list, string SeriesName, double b, double a)
        {

            chart1.Series[0].IsVisibleInLegend = false;
            chart1.Series[SeriesName].ChartType = SeriesChartType.Spline;
            foreach (var key in list)
            {
                chart1.Series[SeriesName].Points.AddXY(key.Key, Function(key.Key, b, a));
            }
        }
      

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox11.Text = trackBar1.Value.ToString();
            button2_Click(sender, e);
        }
     

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

            textBox12.Text = trackBar2.Value.ToString();
            button3_Click_1(sender, e);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click_1(object sender, EventArgs e)
        {

        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisY.Maximum = trackBar3.Value;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
           

        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar5.Value.ToString();

        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = trackBar6.Value.ToString();
      
   
        }
        private void trackBarr_Scroll(object sender, EventArgs e)
        {
            button1_Click_1(sender, e);
            if (b1)
            {
                trackBar1_Scroll(sender, e);
            }
            if (b2)
            {
                trackBar2_Scroll(sender, e);
            }
        }
        private void trackBar7_Scroll(object sender, EventArgs e)
        {
            textBox3.Text = trackBar7.Value.ToString();
         
        }
        private void trackBar8_Scroll(object sender, EventArgs e)
        {
            textBox4.Text = trackBar8.Value.ToString();
           

        }

        private void trackBar9_Scroll(object sender, EventArgs e)
        {
            textBox5.Text = trackBar9.Value.ToString();
       
        }

        private void trackBar10_Scroll(object sender, EventArgs e)
        {
            textBox6.Text = trackBar10.Value.ToString();

        }

        private void trackBar11_Scroll(object sender, EventArgs e)
        {
            textBox7.Text = trackBar11.Value.ToString();
        }

        private void trackBar12_Scroll(object sender, EventArgs e)
        {
            textBox8.Text = trackBar12.Value.ToString();
        }

        private void trackBar13_Scroll(object sender, EventArgs e)
        {
            textBox9.Text = trackBar13.Value.ToString();
        }

        private void trackBar14_Scroll(object sender, EventArgs e)
        {
            textBox10.Text = trackBar14.Value.ToString();

        }

      

        private void button1_Click(object sender, EventArgs e)
        {

        }

      
        private void chart_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

       

        private void trackBar4_Scroll_1(object sender, EventArgs e)
        {
           
            if(trackBar4.Value % 10 == 0 || trackBar4.Value == trackBar15.Value)
            {
                return;
            }
            textBox13.Text = trackBar4.Value.ToString();
            button2_Click(sender, e);
           
        }

      

        private void trackBar15_Scroll(object sender, EventArgs e)
        {
            if (trackBar15.Value % 10 == 0 || trackBar4.Value == trackBar15.Value)
            {
                return;
            }
            textBox14.Text = trackBar15.Value.ToString();
            button3_Click_1(sender, e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            List<Control> controls = new List<Control>
        {
            trackBar5,
            trackBar6,
            trackBar7,
            trackBar8,
            trackBar9,
            trackBar10,
            trackBar11,
            trackBar12,
            trackBar13,
            trackBar14,
            textBox1,
            textBox2,
            textBox3,
            textBox4,
            textBox5,
            textBox6,
            textBox7,
            textBox8,
            textBox9,
            textBox10,
            label2,
            label3,
            label4,
            label5,
            label6,
            label7,
            label8,
            label9,
            label10,
            label11,
            button1
        };
           
            if (checkBox1.Checked) {
               foreach(var control in controls)
                {
                    control.Visible = false;
                }
            }
            else
            {
                foreach (var control in controls)
                {
                    control.Visible = true;
                }
            }
            
        }
    }
}
