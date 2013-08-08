namespace NDependMetricsReporter
{
    partial class MetricsViewer
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnLoadCodeBase = new System.Windows.Forms.Button();
            this.btnHistoryChart = new System.Windows.Forms.Button();
            this.chrtLineChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lvwAssembliesList = new System.Windows.Forms.ListView();
            this.asmLstAssemblyName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.assembyPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tboxProjectName = new System.Windows.Forms.TextBox();
            this.lblNDependProjectSelector = new System.Windows.Forms.Label();
            this.btnOpenProject = new System.Windows.Forms.Button();
            this.lblAssembliesList = new System.Windows.Forms.Label();
            this.lvwNamespacesList = new System.Windows.Forms.ListView();
            this.lblNameSpaces = new System.Windows.Forms.Label();
            this.lvwAssembliesMetricsList = new System.Windows.Forms.ListView();
            this.assemblyName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.assmLinesOfCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.assmLinesCommet = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.assmPercComm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NamespaceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.chrtLineChart)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadCodeBase
            // 
            this.btnLoadCodeBase.Location = new System.Drawing.Point(675, 458);
            this.btnLoadCodeBase.Name = "btnLoadCodeBase";
            this.btnLoadCodeBase.Size = new System.Drawing.Size(116, 23);
            this.btnLoadCodeBase.TabIndex = 0;
            this.btnLoadCodeBase.Text = "Load Codebase";
            this.btnLoadCodeBase.UseVisualStyleBackColor = true;
            this.btnLoadCodeBase.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnHistoryChart
            // 
            this.btnHistoryChart.Location = new System.Drawing.Point(675, 57);
            this.btnHistoryChart.Name = "btnHistoryChart";
            this.btnHistoryChart.Size = new System.Drawing.Size(75, 23);
            this.btnHistoryChart.TabIndex = 2;
            this.btnHistoryChart.Text = "History Chart";
            this.btnHistoryChart.UseVisualStyleBackColor = true;
            this.btnHistoryChart.Click += new System.EventHandler(this.btnHistoryChart_Click);
            // 
            // chrtLineChart
            // 
            chartArea3.Name = "ChartArea1";
            this.chrtLineChart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chrtLineChart.Legends.Add(legend3);
            this.chrtLineChart.Location = new System.Drawing.Point(675, 83);
            this.chrtLineChart.Name = "chrtLineChart";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Lines Of Code";
            this.chrtLineChart.Series.Add(series3);
            this.chrtLineChart.Size = new System.Drawing.Size(423, 300);
            this.chrtLineChart.TabIndex = 3;
            this.chrtLineChart.Text = "LineChart";
            // 
            // lvwAssembliesList
            // 
            this.lvwAssembliesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.asmLstAssemblyName,
            this.assembyPath});
            this.lvwAssembliesList.GridLines = true;
            this.lvwAssembliesList.Location = new System.Drawing.Point(39, 83);
            this.lvwAssembliesList.Name = "lvwAssembliesList";
            this.lvwAssembliesList.Size = new System.Drawing.Size(572, 78);
            this.lvwAssembliesList.TabIndex = 4;
            this.lvwAssembliesList.UseCompatibleStateImageBehavior = false;
            this.lvwAssembliesList.View = System.Windows.Forms.View.Details;
            this.lvwAssembliesList.SelectedIndexChanged += new System.EventHandler(this.lvwAssembliesList_SelectedIndexChanged);
            // 
            // asmLstAssemblyName
            // 
            this.asmLstAssemblyName.Text = "Assembly Name";
            this.asmLstAssemblyName.Width = 212;
            // 
            // assembyPath
            // 
            this.assembyPath.Text = "Assembly Path";
            this.assembyPath.Width = 355;
            // 
            // tboxProjectName
            // 
            this.tboxProjectName.Location = new System.Drawing.Point(39, 37);
            this.tboxProjectName.Name = "tboxProjectName";
            this.tboxProjectName.ReadOnly = true;
            this.tboxProjectName.Size = new System.Drawing.Size(455, 20);
            this.tboxProjectName.TabIndex = 6;
            // 
            // lblNDependProjectSelector
            // 
            this.lblNDependProjectSelector.AutoSize = true;
            this.lblNDependProjectSelector.Location = new System.Drawing.Point(36, 21);
            this.lblNDependProjectSelector.Name = "lblNDependProjectSelector";
            this.lblNDependProjectSelector.Size = new System.Drawing.Size(131, 13);
            this.lblNDependProjectSelector.TabIndex = 7;
            this.lblNDependProjectSelector.Text = "NDepend Project Selector";
            // 
            // btnOpenProject
            // 
            this.btnOpenProject.Location = new System.Drawing.Point(510, 35);
            this.btnOpenProject.Name = "btnOpenProject";
            this.btnOpenProject.Size = new System.Drawing.Size(101, 23);
            this.btnOpenProject.TabIndex = 8;
            this.btnOpenProject.Text = "Select Project...";
            this.btnOpenProject.UseVisualStyleBackColor = true;
            this.btnOpenProject.Click += new System.EventHandler(this.btnOpenProject_Click);
            // 
            // lblAssembliesList
            // 
            this.lblAssembliesList.AutoSize = true;
            this.lblAssembliesList.Location = new System.Drawing.Point(36, 67);
            this.lblAssembliesList.Name = "lblAssembliesList";
            this.lblAssembliesList.Size = new System.Drawing.Size(193, 13);
            this.lblAssembliesList.TabIndex = 9;
            this.lblAssembliesList.Text = "Assemblies analyzed in selected project";
            // 
            // lvwNamespacesList
            // 
            this.lvwNamespacesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NamespaceName});
            this.lvwNamespacesList.GridLines = true;
            this.lvwNamespacesList.Location = new System.Drawing.Point(39, 273);
            this.lvwNamespacesList.Name = "lvwNamespacesList";
            this.lvwNamespacesList.Size = new System.Drawing.Size(607, 150);
            this.lvwNamespacesList.TabIndex = 10;
            this.lvwNamespacesList.UseCompatibleStateImageBehavior = false;
            this.lvwNamespacesList.View = System.Windows.Forms.View.Details;
            // 
            // lblNameSpaces
            // 
            this.lblNameSpaces.AutoSize = true;
            this.lblNameSpaces.Location = new System.Drawing.Point(36, 257);
            this.lblNameSpaces.Name = "lblNameSpaces";
            this.lblNameSpaces.Size = new System.Drawing.Size(169, 13);
            this.lblNameSpaces.TabIndex = 11;
            this.lblNameSpaces.Text = "Namespaces in selected assembly";
            // 
            // lvwAssembliesMetricsList
            // 
            this.lvwAssembliesMetricsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.assemblyName,
            this.assmLinesOfCode,
            this.assmLinesCommet,
            this.assmPercComm});
            this.lvwAssembliesMetricsList.GridLines = true;
            this.lvwAssembliesMetricsList.Location = new System.Drawing.Point(39, 167);
            this.lvwAssembliesMetricsList.Name = "lvwAssembliesMetricsList";
            this.lvwAssembliesMetricsList.Size = new System.Drawing.Size(582, 79);
            this.lvwAssembliesMetricsList.TabIndex = 12;
            this.lvwAssembliesMetricsList.UseCompatibleStateImageBehavior = false;
            this.lvwAssembliesMetricsList.View = System.Windows.Forms.View.Details;
            this.lvwAssembliesMetricsList.SelectedIndexChanged += new System.EventHandler(this.lvwAssembliesMetricsList_SelectedIndexChanged);
            // 
            // assemblyName
            // 
            this.assemblyName.Text = "Assembly Name";
            this.assemblyName.Width = 175;
            // 
            // assmLinesOfCode
            // 
            this.assmLinesOfCode.Text = "LOC";
            this.assmLinesOfCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.assmLinesOfCode.Width = 75;
            // 
            // assmLinesCommet
            // 
            this.assmLinesCommet.Text = "LComm";
            // 
            // assmPercComm
            // 
            this.assmPercComm.Text = "%Comm";
            // 
            // NamespaceName
            // 
            this.NamespaceName.Text = "Namespace name";
            this.NamespaceName.Width = 245;
            // 
            // MetricsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 603);
            this.Controls.Add(this.lvwAssembliesMetricsList);
            this.Controls.Add(this.lblNameSpaces);
            this.Controls.Add(this.lvwNamespacesList);
            this.Controls.Add(this.lblAssembliesList);
            this.Controls.Add(this.btnOpenProject);
            this.Controls.Add(this.lblNDependProjectSelector);
            this.Controls.Add(this.tboxProjectName);
            this.Controls.Add(this.lvwAssembliesList);
            this.Controls.Add(this.chrtLineChart);
            this.Controls.Add(this.btnHistoryChart);
            this.Controls.Add(this.btnLoadCodeBase);
            this.Name = "MetricsViewer";
            this.Text = "Metrics Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.chrtLineChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadCodeBase;
        private System.Windows.Forms.Button btnHistoryChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrtLineChart;
        private System.Windows.Forms.ListView lvwAssembliesList;
        private System.Windows.Forms.TextBox tboxProjectName;
        private System.Windows.Forms.Label lblNDependProjectSelector;
        private System.Windows.Forms.Button btnOpenProject;
        private System.Windows.Forms.ColumnHeader asmLstAssemblyName;
        private System.Windows.Forms.ColumnHeader assembyPath;
        private System.Windows.Forms.Label lblAssembliesList;
        private System.Windows.Forms.ListView lvwNamespacesList;
        private System.Windows.Forms.Label lblNameSpaces;
        private System.Windows.Forms.ListView lvwAssembliesMetricsList;
        private System.Windows.Forms.ColumnHeader assemblyName;
        private System.Windows.Forms.ColumnHeader assmLinesOfCode;
        private System.Windows.Forms.ColumnHeader assmLinesCommet;
        private System.Windows.Forms.ColumnHeader assmPercComm;
        private System.Windows.Forms.ColumnHeader NamespaceName;
    }
}

