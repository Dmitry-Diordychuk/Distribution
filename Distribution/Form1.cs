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
using CenterSpace.NMath.Stats;
using CenterSpace.NMath.Core;
using CenterSpace.NMath.Charting.Microsoft;

namespace Distribution
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void uniformButton_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            double a,b;
            if (double.TryParse(aBox.Text, out a) && double.TryParse(bBox.Text, out b))
            {
                if (a != b)
                {
                    UniformDistribution uniform = new UniformDistribution(a, b);
                    //PDF
                    for (double x = 0.1; x < b + (Math.Abs(a-b)/2); x = x + 0.1)
                    {
                        chart1.Series[0].Points.AddXY(x, uniform.PDF(x));
                    }
                    //CDF
                    for (double x = 0.1; x < b + (Math.Abs(a-b)/2) ; x = x + 0.1)
                    {
                        chart2.Series[0].Points.AddXY(x, uniform.CDF(x));
                    }
                }
                else
                {
                    MessageBox.Show("There must be a gap between a and b");
                }
            }
            else
            {
                MessageBox.Show("Wrong input! Try again.");
            }
        }

        private void erlangButton_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            int k;
            double u;
            if(int.TryParse(shapeBox.Text, out k) && double.TryParse(scaleBox.Text, out u))
            {
                GammaDistribution gamma = new GammaDistribution(k, u);
                //PDF
                for(double x = 0.1; x<10*k*u; x=x+0.1)
                {
                    chart1.Series[0].Points.AddXY(x,gamma.PDF(x));
                }
                //CDF
                for (double x = 0.1; x < 20*u; x = x + 0.1)
                {
                    chart2.Series[0].Points.AddXY(x, gamma.CDF(x));
                }
            }
            else
            {
                MessageBox.Show("Wrong input! Try again.");
            }  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart2.Series[0].ChartType = SeriesChartType.Line;
            chart1.Legends.Clear();
            chart2.Legends.Clear();
            chart1.Titles.Add("Probability density function");
            chart2.Titles.Add("Cumulative distribution function");
        }
    }
}
