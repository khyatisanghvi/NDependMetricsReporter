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
    public partial class MetricTrendChart : Form
    {
        public MetricTrendChart()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        public void RefreshData(string chartTitle, string seriesName, IList dataList)
        {
            this.chartMetricTrendChart.Titles[0].Text = chartTitle;
            this.chartMetricTrendChart.Series.Clear();
            this.chartMetricTrendChart.Series.Add(seriesName);
            this.chartMetricTrendChart.Series[seriesName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            this.chartMetricTrendChart.Series[seriesName].BorderWidth = 2;
            this.chartMetricTrendChart.DataSource = dataList;
            this.chartMetricTrendChart.Series[seriesName].YValueMembers = "Y";
            this.chartMetricTrendChart.DataBind();
            this.chartMetricTrendChart.Update();
            this.Show();
        }
    }
}
