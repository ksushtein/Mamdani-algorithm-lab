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

namespace examKsuMamdani
{
    public partial class Form1 : Form
    {
        Manager _manager;

        public Form1(Manager manager)
        {
            _manager = manager;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // chart1.ChartAreas[0].AxisY.Maximum = 1;
        }

        private void btnGetResult_Click(object sender, EventArgs e)
        {
            _manager.x.Clear();
            _manager.y.Clear();
            _manager.GetResult(txtBxProcent.Text, txtBxTime.Text);
            ShowRules();

            txtBxMax.Text = (_manager.CountMax()/100.0).ToString();
            txtBxCentre.Text = _manager.CountCentroid().ToString();
            ShowFinalGraf();
        }

        void ShowRules()
        {

            Chart[] charts = new Chart[16] {chart1, chart2, chart3,chart4, chart5, chart6, chart7, chart8, chart9, chart10, chart11, chart12, chart13, chart14, chart15, chart16};
            foreach (Chart chart in charts)
            {
                chart.Series[0].Points.Clear();
                chart.Series[0].ChartType = SeriesChartType.FastLine;
                chart.Series[0].BorderWidth = 3;
                chart.Series[0].Color = Color.MediumBlue;
                 chart.ChartAreas[0].AxisY.Maximum = 1;//ПОСЛЕ ПОЧИНКИ ЗАККОМЕНТИТЬ - КСЮ
            }
            
            List<Rule> rule = _manager.GetRules();
            List<Grafic> grafics = new List<Grafic>();
            foreach (Rule r in rule)
            {
                grafics.Add(r.grafIf1);
                grafics.Add(r.grafIf2);
                grafics.Add(r.grafRes);
                grafics.Add(r.grafResWithOtsech);
            }

            for (int chartIndex=0; chartIndex < charts.Count(); chartIndex++)
            {
                //if1
                if (chartIndex == 0 || chartIndex == 4 || chartIndex == 8 || chartIndex == 12)
                    for (int i = 0; i < 10000; i++)//до 100%
                    {
                        double y = grafics[chartIndex].GetValueY(_manager.x[i]);
                        charts[chartIndex].Series[0].Points.AddXY(_manager.x[i], y);
                    }
                //if2
                if (chartIndex == 1 || chartIndex == 5 || chartIndex == 9 || chartIndex == 13)
                    for (int i = 0; i < 3000; i++)//до 30 дней
                    {
                        double y = grafics[chartIndex].GetValueY(_manager.x[i]);
                        charts[chartIndex].Series[0].Points.AddXY(_manager.x[i], y);
                    }
                //Res
                if (chartIndex == 2 || chartIndex == 6 || chartIndex == 10 || chartIndex == 14)
                    for (int i = 0; i < 10000; i++)//до 100% дней
                    {
                        double y = grafics[chartIndex].GetValueY(_manager.x[i]);
                        charts[chartIndex].Series[0].Points.AddXY(_manager.x[i], y);
                    }
                //ResWithOtsech
                if (chartIndex == 3 || chartIndex == 7 || chartIndex == 11 || chartIndex == 15)
                    for (int i = 0; i < 10000; i++)//до 100% дней
                    {
                        double y = grafics[chartIndex].GetValueY(_manager.x[i]);
                        charts[chartIndex].Series[0].Points.AddXY(_manager.x[i], y);
                    }
            }
            
            
        }

        void ShowFinalGraf()
        {
            chart0_Final.Series[0].Points.Clear();
            chart0_Final.Series[0].ChartType = SeriesChartType.FastLine;
            chart0_Final.Series[0].BorderWidth = 3;
            chart0_Final.Series[0].Color = Color.Navy;
            chart0_Final.ChartAreas[0].AxisY.Maximum = 1;

            for (int i = 0; i < 10000; i++)
            {
                chart0_Final.Series[0].Points.AddXY(_manager.x[i], _manager.y[i]);
            }
            //Grafic graficTime1 = rule[0].grafIf2;


            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
        }




    }
}
