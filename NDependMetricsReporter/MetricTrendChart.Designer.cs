namespace NDependMetricsReporter
{
    partial class MetricTrendChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chartMetricTrendChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartMetricTrendChart)).BeginInit();
            this.SuspendLayout();
            // 
            // chartMetricTrendChart
            // 
            chartArea1.Name = "ChartArea1";
            this.chartMetricTrendChart.ChartAreas.Add(chartArea1);
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "Legend1";
            this.chartMetricTrendChart.Legends.Add(legend1);
            this.chartMetricTrendChart.Location = new System.Drawing.Point(12, 12);
            this.chartMetricTrendChart.Name = "chartMetricTrendChart";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Lines Of Code";
            this.chartMetricTrendChart.Series.Add(series1);
            this.chartMetricTrendChart.Size = new System.Drawing.Size(812, 558);
            this.chartMetricTrendChart.TabIndex = 0;
            this.chartMetricTrendChart.Text = "Metric Trend Chart";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            title1.Name = "Title1";
            title1.Text = "CODE ELEMENT TYPE TREND: Code Element Name";
            this.chartMetricTrendChart.Titles.Add(title1);
            // 
            // MetricTrendChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 582);
            this.Controls.Add(this.chartMetricTrendChart);
            this.Name = "MetricTrendChart";
            this.Text = "Metric Trend Chart";
            ((System.ComponentModel.ISupportInitialize)(this.chartMetricTrendChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartMetricTrendChart;
    }
}