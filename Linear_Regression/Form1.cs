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
        private List<double> listX = new List<double>();

        private List<Tuple<double, double>> tupleList = new List<Tuple<double, double>>();
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
                
                };
         
            for (int i = 0; i < 10; i++)
            {
                this.listX.Add((i) * 10);
            }
            foreach (var trackbar in trackBars)
            {
                trackbar.Scroll += new EventHandler(trackBarr_Scroll);
            }
            trackBar3_Scroll(sender, e);
            checkBox1_CheckedChanged(sender, e);
            Chartconfiguration();  
            button1_Click_1(sender, e);

        }

        List<Tuple<double, double>> CreateTupleList(List<double> xlist, List<TrackBar> clist) 
            {
            List<Tuple<double, double>> newTup = new List<Tuple<double, double>>();
            for (int i = 0; i < xlist.Count; i++)
            {
                newTup.Add(new Tuple<double, double>(xlist[i], (double)clist[i].Value));
            }
            return newTup;
        }
        #region Chart conf
        void Chartconfiguration()
        {
            chart1.Series.Add("Wartość");
            ChartPointConfiguration("Wartość", SeriesChartType.Point, Color.Black, 8, true);
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
        #endregion

        private void button1_Click_1(object sender, EventArgs e)
        {
            ClearChart("Wartość");
            var tupl = CreateTupleList(this.listX, trackBars);
            DrawNewLine(trackBars, listX, "REGRESJA");
            foreach (var key in tupl)
            {
                chart1.Series["Wartość"].Points.AddXY(key.Item1, key.Item2);
            }
        }
        void ChartPointConfiguration(string name, SeriesChartType type ,Color color, int MarkerSize, bool isVisible)
        {
            chart1.Series[name].ChartType = type;
            chart1.Series[name].Color= color;
            chart1.Series[name].MarkerSize = MarkerSize;
            chart1.Series[name].IsVisibleInLegend = isVisible;
        }        
        private void button2_Click(object sender, EventArgs e)
        {
            b1 = true;
            trackBar4.Value = (int)double.Parse(textBox13.Text);
            List<TrackBar> trackBarsButton2 = new List<TrackBar>(this.trackBars); 
            trackBarsButton2.Add(trackBar1);
            List<double> listX_btn = new List<double>(this.listX);
            listX_btn.Add(trackBar4.Value);
            DrawNewLine(trackBarsButton2, listX_btn,"REGRESJA 1");
            ClearChart("Wartość 11");
            chart1.Series["Wartość 11"].Points.AddXY(trackBar4.Value, trackBar1.Value);
            if (b3)
            {
                button3_Click_1(sender, e);
            }
        }
        private void DrawNewLine(List<TrackBar> btn_trackBars, List<double> listX, string regName)
        {
            ClearChart(regName);        
            RegChart tupleRegChart = new RegChart(CreateTupleList(listX, btn_trackBars));
            tupleRegChart.PrintLineChartTuple(regName, ref chart1);                    
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            trackBar15.Value = (int)double.Parse(textBox14.Text);
            List<TrackBar> trackBarsButton3 = new List<TrackBar>(this.trackBars);
            trackBarsButton3.Add(trackBar1);
            trackBarsButton3.Add(trackBar2);
            List<double> listX_btn = new List<double>(this.listX);
            listX_btn.Add(double.Parse(textBox13.Text));
            listX_btn.Add(double.Parse(textBox14.Text));
            DrawNewLine(trackBarsButton3, listX_btn, "REGRESJA 2");
            ClearChart("Wartość 12");
            chart1.Series["Wartość 12"].Points.AddXY(trackBar15.Value, trackBar2.Value);
            this.b3 = true;
        }

        private void ClearChart(string name)
        {
            chart1.Series[name].Points.Clear();
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
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisY.Maximum = trackBar3.Value;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            
        }
        
              
        

        private void trackBar4_Scroll_1(object sender, EventArgs e)
        {
            textBox13.Text = trackBar4.Value.ToString();
            button2_Click(sender, e);
           
        }
        private void trackBar15_Scroll(object sender, EventArgs e)
        {
            textBox14.Text = trackBar15.Value.ToString();
            button3_Click_1(sender, e);
        }
        #region Checked(true-false)
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

            if (checkBox1.Checked)
            {
                foreach (var control in controls)
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
        #endregion

        #region 5 - 14 trackbars scroll
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

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar5.Value.ToString();

        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = trackBar6.Value.ToString();


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
        #endregion

        #region generated code
        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click_1(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {


        }

        private void buttonEvent(List<TrackBar> trackBars, List<double> xList)
        {

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
        #endregion
    }
}
