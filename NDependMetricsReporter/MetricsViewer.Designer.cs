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
            this.lvwAssembliesList = new System.Windows.Forms.ListView();
            this.asmLstAssemblyName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.assembyPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tboxProjectName = new System.Windows.Forms.TextBox();
            this.lblNDependProjectSelector = new System.Windows.Forms.Label();
            this.btnOpenProject = new System.Windows.Forms.Button();
            this.lblAssembliesList = new System.Windows.Forms.Label();
            this.lvwNamespacesList = new System.Windows.Forms.ListView();
            this.NamespaceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblNamespacesList = new System.Windows.Forms.Label();
            this.lvwMetricsList = new System.Windows.Forms.ListView();
            this.MetricName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MetricValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTypesList = new System.Windows.Forms.ListView();
            this.TypeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblTypesList = new System.Windows.Forms.Label();
            this.lvwMethodsList = new System.Windows.Forms.ListView();
            this.MethodName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblMethodsList = new System.Windows.Forms.Label();
            this.lvwMetricsList2 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rtfMetricProperties = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lvwAssembliesList
            // 
            this.lvwAssembliesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.asmLstAssemblyName,
            this.assembyPath});
            this.lvwAssembliesList.GridLines = true;
            this.lvwAssembliesList.Location = new System.Drawing.Point(28, 73);
            this.lvwAssembliesList.Name = "lvwAssembliesList";
            this.lvwAssembliesList.Size = new System.Drawing.Size(740, 78);
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
            this.assembyPath.Width = 395;
            // 
            // tboxProjectName
            // 
            this.tboxProjectName.Location = new System.Drawing.Point(28, 27);
            this.tboxProjectName.Name = "tboxProjectName";
            this.tboxProjectName.ReadOnly = true;
            this.tboxProjectName.Size = new System.Drawing.Size(455, 20);
            this.tboxProjectName.TabIndex = 6;
            // 
            // lblNDependProjectSelector
            // 
            this.lblNDependProjectSelector.AutoSize = true;
            this.lblNDependProjectSelector.Location = new System.Drawing.Point(25, 11);
            this.lblNDependProjectSelector.Name = "lblNDependProjectSelector";
            this.lblNDependProjectSelector.Size = new System.Drawing.Size(131, 13);
            this.lblNDependProjectSelector.TabIndex = 7;
            this.lblNDependProjectSelector.Text = "NDepend Project Selector";
            // 
            // btnOpenProject
            // 
            this.btnOpenProject.Location = new System.Drawing.Point(499, 25);
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
            this.lblAssembliesList.Location = new System.Drawing.Point(25, 57);
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
            this.lvwNamespacesList.Location = new System.Drawing.Point(28, 178);
            this.lvwNamespacesList.Name = "lvwNamespacesList";
            this.lvwNamespacesList.Size = new System.Drawing.Size(498, 150);
            this.lvwNamespacesList.TabIndex = 10;
            this.lvwNamespacesList.UseCompatibleStateImageBehavior = false;
            this.lvwNamespacesList.View = System.Windows.Forms.View.Details;
            this.lvwNamespacesList.SelectedIndexChanged += new System.EventHandler(this.lvwNamespacesList_SelectedIndexChanged);
            // 
            // NamespaceName
            // 
            this.NamespaceName.Text = "Namespace name";
            this.NamespaceName.Width = 245;
            // 
            // lblNamespacesList
            // 
            this.lblNamespacesList.AutoSize = true;
            this.lblNamespacesList.Location = new System.Drawing.Point(25, 162);
            this.lblNamespacesList.Name = "lblNamespacesList";
            this.lblNamespacesList.Size = new System.Drawing.Size(169, 13);
            this.lblNamespacesList.TabIndex = 11;
            this.lblNamespacesList.Text = "Namespaces in selected assembly";
            // 
            // lvwMetricsList
            // 
            this.lvwMetricsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.MetricName,
            this.MetricValue});
            this.lvwMetricsList.GridLines = true;
            this.lvwMetricsList.Location = new System.Drawing.Point(554, 178);
            this.lvwMetricsList.Name = "lvwMetricsList";
            this.lvwMetricsList.Size = new System.Drawing.Size(214, 385);
            this.lvwMetricsList.TabIndex = 13;
            this.lvwMetricsList.UseCompatibleStateImageBehavior = false;
            this.lvwMetricsList.View = System.Windows.Forms.View.Details;
            this.lvwMetricsList.SelectedIndexChanged += new System.EventHandler(this.lvwMetricsList_SelectedIndexChanged);
            // 
            // MetricName
            // 
            this.MetricName.Text = "Metric Name";
            this.MetricName.Width = 141;
            // 
            // MetricValue
            // 
            this.MetricValue.Text = "Value";
            this.MetricValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MetricValue.Width = 55;
            // 
            // lvwTypesList
            // 
            this.lvwTypesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TypeName});
            this.lvwTypesList.GridLines = true;
            this.lvwTypesList.Location = new System.Drawing.Point(28, 353);
            this.lvwTypesList.Name = "lvwTypesList";
            this.lvwTypesList.Size = new System.Drawing.Size(498, 150);
            this.lvwTypesList.TabIndex = 14;
            this.lvwTypesList.UseCompatibleStateImageBehavior = false;
            this.lvwTypesList.View = System.Windows.Forms.View.Details;
            this.lvwTypesList.SelectedIndexChanged += new System.EventHandler(this.lvwTypesList_SelectedIndexChanged);
            // 
            // TypeName
            // 
            this.TypeName.Text = "Type Name";
            this.TypeName.Width = 245;
            // 
            // lblTypesList
            // 
            this.lblTypesList.AutoSize = true;
            this.lblTypesList.Location = new System.Drawing.Point(25, 337);
            this.lblTypesList.Name = "lblTypesList";
            this.lblTypesList.Size = new System.Drawing.Size(150, 13);
            this.lblTypesList.TabIndex = 15;
            this.lblTypesList.Text = "Types in selected Namespace";
            // 
            // lvwMethodsList
            // 
            this.lvwMethodsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.MethodName});
            this.lvwMethodsList.GridLines = true;
            this.lvwMethodsList.Location = new System.Drawing.Point(28, 531);
            this.lvwMethodsList.Name = "lvwMethodsList";
            this.lvwMethodsList.Size = new System.Drawing.Size(498, 150);
            this.lvwMethodsList.TabIndex = 16;
            this.lvwMethodsList.UseCompatibleStateImageBehavior = false;
            this.lvwMethodsList.View = System.Windows.Forms.View.Details;
            // 
            // MethodName
            // 
            this.MethodName.Text = "MethodName";
            this.MethodName.Width = 245;
            // 
            // lblMethodsList
            // 
            this.lblMethodsList.AutoSize = true;
            this.lblMethodsList.Location = new System.Drawing.Point(27, 514);
            this.lblMethodsList.Name = "lblMethodsList";
            this.lblMethodsList.Size = new System.Drawing.Size(129, 13);
            this.lblMethodsList.TabIndex = 17;
            this.lblMethodsList.Text = "Methods in selected Type";
            // 
            // lvwMetricsList2
            // 
            this.lvwMetricsList2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvwMetricsList2.GridLines = true;
            this.lvwMetricsList2.Location = new System.Drawing.Point(791, 178);
            this.lvwMetricsList2.Name = "lvwMetricsList2";
            this.lvwMetricsList2.Size = new System.Drawing.Size(214, 385);
            this.lvwMetricsList2.TabIndex = 19;
            this.lvwMetricsList2.UseCompatibleStateImageBehavior = false;
            this.lvwMetricsList2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Metric Name";
            this.columnHeader1.Width = 141;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader2.Width = 55;
            // 
            // rtfMetricProperties
            // 
            this.rtfMetricProperties.Location = new System.Drawing.Point(554, 572);
            this.rtfMetricProperties.Name = "rtfMetricProperties";
            this.rtfMetricProperties.Size = new System.Drawing.Size(214, 109);
            this.rtfMetricProperties.TabIndex = 20;
            this.rtfMetricProperties.Text = "";
            // 
            // MetricsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 694);
            this.Controls.Add(this.rtfMetricProperties);
            this.Controls.Add(this.lvwMetricsList2);
            this.Controls.Add(this.lblMethodsList);
            this.Controls.Add(this.lvwMethodsList);
            this.Controls.Add(this.lblTypesList);
            this.Controls.Add(this.lvwTypesList);
            this.Controls.Add(this.lvwMetricsList);
            this.Controls.Add(this.lblNamespacesList);
            this.Controls.Add(this.lvwNamespacesList);
            this.Controls.Add(this.lblAssembliesList);
            this.Controls.Add(this.btnOpenProject);
            this.Controls.Add(this.lblNDependProjectSelector);
            this.Controls.Add(this.tboxProjectName);
            this.Controls.Add(this.lvwAssembliesList);
            this.Name = "MetricsViewer";
            this.Text = "Metrics Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvwAssembliesList;
        private System.Windows.Forms.TextBox tboxProjectName;
        private System.Windows.Forms.Label lblNDependProjectSelector;
        private System.Windows.Forms.Button btnOpenProject;
        private System.Windows.Forms.ColumnHeader asmLstAssemblyName;
        private System.Windows.Forms.ColumnHeader assembyPath;
        private System.Windows.Forms.Label lblAssembliesList;
        private System.Windows.Forms.ListView lvwNamespacesList;
        private System.Windows.Forms.Label lblNamespacesList;
        private System.Windows.Forms.ColumnHeader NamespaceName;
        private System.Windows.Forms.ListView lvwMetricsList;
        private System.Windows.Forms.ColumnHeader MetricName;
        private System.Windows.Forms.ColumnHeader MetricValue;
        private System.Windows.Forms.ListView lvwTypesList;
        private System.Windows.Forms.ColumnHeader TypeName;
        private System.Windows.Forms.Label lblTypesList;
        private System.Windows.Forms.ListView lvwMethodsList;
        private System.Windows.Forms.ColumnHeader MethodName;
        private System.Windows.Forms.Label lblMethodsList;
        private System.Windows.Forms.ListView lvwMetricsList2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.RichTextBox rtfMetricProperties;
    }
}

