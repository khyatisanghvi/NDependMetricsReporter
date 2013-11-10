using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDependMetricsReporter
{
    public partial class MetricsChart : Form
    {
        public MetricsChart()
        {
            InitializeComponent();
        }

        public void RenderSingleLineTrendChartNoXValues(string chartTitle, string seriesName, IList yValues)
        {
            Charter chart = new Charter(this.chartMetricChart);
            chart.SetSingleLineTrendChartNoXValues(chartTitle, seriesName, yValues);
            this.Icon = Properties.Resources.trend;
            this.Text = "Trend Chart";
            this.chartMetricChart.Update();
            this.Show();
        }

        public void RenderSingleVerticalBarChart(string chartTitle, string seriesName, IList xValues, IList yValues)
        {
            Charter chart = new Charter(this.chartMetricChart);
            chart.SetSingleVerticalBarChart(chartTitle, seriesName, xValues, yValues);
            this.Icon = Properties.Resources.bar;
            this.Text = "Frequencies Chart";
            this.chartMetricChart.Update();
            this.Show();
        }
    }
}
