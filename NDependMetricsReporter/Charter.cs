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
        }

        public void SetSingleVerticalBarChart(string chartTitle, string seriesName, IList xValues, IList yValues)
        {
            chart.Titles[0].Text = chartTitle;
            chart.Series.Clear();
            chart.Series.Add(seriesName);
            chart.Series[seriesName].ChartType = SeriesChartType.Column;
            chart.Series[seriesName]["MinPixelPointWidth"] = "3";
            chart.Series[seriesName].Points.DataBindXY(xValues, yValues);
            SetXLimitsAndIntervals(chart.ChartAreas[0], xValues, yValues);
            SetChartAreaLayout(chart.ChartAreas[0]);
        }

        private void SetSeriesLayout(Series serie)
        {
     
        }

        private void SetChartAreaLayout(ChartArea chartArea)
        {
            chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        }

        private void SetXLimitsAndIntervals(ChartArea chartArea, IList xValues, IList yValues)
        {          
            double min = ((List<double>)xValues).Min();
            double max = ((List<double>)xValues).Max();

            chartArea.AxisX.Minimum = min >= 0 ? 0 : min;
            chartArea.AxisX.Maximum = max;
            double interval = GetBestIntervalValue(min, max, 10);
            chartArea.AxisX.Interval = interval;
            chartArea.AxisY.Enabled = AxisEnabled.Auto;
            chartArea.AxisX.Minimum = chartArea.AxisX.Minimum - interval;
            chartArea.AxisX.Maximum = chartArea.AxisX.Maximum + interval;
        }

        private double GetBestIntervalValue(double min, double max, int expectedNumberOfIntervals)
        {
            List<double> possibleIntervals = new List<double> { 0.1, 1, 5, 10, 20, 50, 100, 500, 1000 };
            double valuesRange = max - min;
            double roughInterval = (double)(valuesRange / expectedNumberOfIntervals);
            //int closestInterval = (int)possibleIntervals.OrderBy(item => Math.Abs(roughInterval - item)).First();
            double closestInterval = possibleIntervals.OrderBy(item => Math.Abs(roughInterval - item)).First();
            return closestInterval;
        }
    }
}
