using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace NDependMetricsReporter
{
    public class Charter
    {
        Chart chart;

        public Charter(Chart chart)
        {
            this.chart = chart;
        }
        
        public void SetSingleLineTrendChartNoXValues(string chartTitle, string seriesName, IList yValues)
        {
            chart.Titles[0].Text = chartTitle;
            chart.Series.Clear();
            chart.Series.Add(seriesName);
            chart.Series[seriesName].ChartType = SeriesChartType.Line;
            chart.Series[seriesName].BorderWidth = 2;
            chart.Series[seriesName].Points.DataBindY(yValues);
            //chart.DataSource = yValues;
            //chart.Series[seriesName].YValueMembers = "Y";
            //chart.DataBind();
        }

        public void SetSingleVerticalBarChart(string chartTitle, string seriesName, List<uint> xValues, List<uint> yValues)
        {
            chart.Titles[0].Text = chartTitle;
            chart.Series.Clear();
            chart.Series.Add(seriesName);
            chart.Series[seriesName].ChartType = SeriesChartType.Column;
            chart.Series[seriesName].Points.DataBindXY(xValues, yValues);
        }
    }
}
