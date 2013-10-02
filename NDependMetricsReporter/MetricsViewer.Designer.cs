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
            this.tboxProjectName = new System.Windows.Forms.TextBox();
            this.lblNDependProjectName = new System.Windows.Forms.Label();
            this.lblAssembliesList = new System.Windows.Forms.Label();
            this.lblNamespacesList = new System.Windows.Forms.Label();
            this.lvwMetricsList = new System.Windows.Forms.ListView();
            this.MetricName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MetricValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblTypesList = new System.Windows.Forms.Label();
            this.lblMethodsList = new System.Windows.Forms.Label();
            this.rtfMetricProperties = new System.Windows.Forms.RichTextBox();
            this.lblCodeElementType = new System.Windows.Forms.Label();
            this.lblCodeElementName = new System.Windows.Forms.Label();
            this.dgvNamespaces = new System.Windows.Forms.DataGridView();
            this.dgvTypes = new System.Windows.Forms.DataGridView();
            this.dgvMethods = new System.Windows.Forms.DataGridView();
            this.dgvAssemblies = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.codeSectionsTabs = new System.Windows.Forms.TabControl();
            this.tabCodeMetrics = new System.Windows.Forms.TabPage();
            this.tabUnitTests = new System.Windows.Forms.TabPage();
            this.tabSpecFlowBDD = new System.Windows.Forms.TabPage();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.ndependProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tBoxProjectPath = new System.Windows.Forms.TextBox();
            this.lblNDependProjectPath = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNamespaces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMethods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssemblies)).BeginInit();
            this.codeSectionsTabs.SuspendLayout();
            this.tabCodeMetrics.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tboxProjectName
            // 
            this.tboxProjectName.Location = new System.Drawing.Point(89, 30);
            this.tboxProjectName.Name = "tboxProjectName";
            this.tboxProjectName.ReadOnly = true;
            this.tboxProjectName.Size = new System.Drawing.Size(294, 20);
            this.tboxProjectName.TabIndex = 6;
            // 
            // lblNDependProjectName
            // 
            this.lblNDependProjectName.AutoSize = true;
            this.lblNDependProjectName.Location = new System.Drawing.Point(12, 33);
            this.lblNDependProjectName.Name = "lblNDependProjectName";
            this.lblNDependProjectName.Size = new System.Drawing.Size(71, 13);
            this.lblNDependProjectName.TabIndex = 7;
            this.lblNDependProjectName.Text = "Project Name";
            // 
            // lblAssembliesList
            // 
            this.lblAssembliesList.AutoSize = true;
            this.lblAssembliesList.Location = new System.Drawing.Point(3, 48);
            this.lblAssembliesList.Name = "lblAssembliesList";
            this.lblAssembliesList.Size = new System.Drawing.Size(193, 13);
            this.lblAssembliesList.TabIndex = 9;
            this.lblAssembliesList.Text = "Assemblies analyzed in selected project";
            // 
            // lblNamespacesList
            // 
            this.lblNamespacesList.AutoSize = true;
            this.lblNamespacesList.Location = new System.Drawing.Point(3, 156);
            this.lblNamespacesList.Name = "lblNamespacesList";
            this.lblNamespacesList.Size = new System.Drawing.Size(169, 13);
            this.lblNamespacesList.TabIndex = 11;
            this.lblNamespacesList.Text = "Namespaces in selected assembly";
            // 
            // lvwMetricsList
            // 
            this.lvwMetricsList.CheckBoxes = true;
            this.lvwMetricsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.MetricName,
            this.MetricValue});
            this.lvwMetricsList.GridLines = true;
            this.lvwMetricsList.Location = new System.Drawing.Point(688, 64);
            this.lvwMetricsList.Name = "lvwMetricsList";
            this.lvwMetricsList.Size = new System.Drawing.Size(262, 497);
            this.lvwMetricsList.TabIndex = 13;
            this.lvwMetricsList.UseCompatibleStateImageBehavior = false;
            this.lvwMetricsList.View = System.Windows.Forms.View.Details;
            this.lvwMetricsList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvwMetricsList_ItemCheck);
            this.lvwMetricsList.SelectedIndexChanged += new System.EventHandler(this.lvwMetricsList_SelectedIndexChanged);
            this.lvwMetricsList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwMetricsList_MouseDoubleClick);
            this.lvwMetricsList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvwMetricsList_MouseDown);
            this.lvwMetricsList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvwMetricsList_MouseUp);
            // 
            // MetricName
            // 
            this.MetricName.Text = "Metric Name";
            this.MetricName.Width = 195;
            // 
            // MetricValue
            // 
            this.MetricValue.Text = "Value";
            this.MetricValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTypesList
            // 
            this.lblTypesList.AutoSize = true;
            this.lblTypesList.Location = new System.Drawing.Point(3, 331);
            this.lblTypesList.Name = "lblTypesList";
            this.lblTypesList.Size = new System.Drawing.Size(150, 13);
            this.lblTypesList.TabIndex = 15;
            this.lblTypesList.Text = "Types in selected Namespace";
            // 
            // lblMethodsList
            // 
            this.lblMethodsList.AutoSize = true;
            this.lblMethodsList.Location = new System.Drawing.Point(5, 508);
            this.lblMethodsList.Name = "lblMethodsList";
            this.lblMethodsList.Size = new System.Drawing.Size(129, 13);
            this.lblMethodsList.TabIndex = 17;
            this.lblMethodsList.Text = "Methods in selected Type";
            // 
            // rtfMetricProperties
            // 
            this.rtfMetricProperties.Location = new System.Drawing.Point(688, 567);
            this.rtfMetricProperties.Name = "rtfMetricProperties";
            this.rtfMetricProperties.Size = new System.Drawing.Size(262, 108);
            this.rtfMetricProperties.TabIndex = 20;
            this.rtfMetricProperties.Text = "";
            // 
            // lblCodeElementType
            // 
            this.lblCodeElementType.AutoSize = true;
            this.lblCodeElementType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodeElementType.Location = new System.Drawing.Point(794, 34);
            this.lblCodeElementType.Name = "lblCodeElementType";
            this.lblCodeElementType.Size = new System.Drawing.Size(100, 13);
            this.lblCodeElementType.TabIndex = 21;
            this.lblCodeElementType.Text = "Code Element Type";
            // 
            // lblCodeElementName
            // 
            this.lblCodeElementName.AutoSize = true;
            this.lblCodeElementName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodeElementName.Location = new System.Drawing.Point(688, 49);
            this.lblCodeElementName.Name = "lblCodeElementName";
            this.lblCodeElementName.Size = new System.Drawing.Size(104, 13);
            this.lblCodeElementName.TabIndex = 22;
            this.lblCodeElementName.Text = "Code Element Name";
            // 
            // dgvNamespaces
            // 
            this.dgvNamespaces.AllowUserToAddRows = false;
            this.dgvNamespaces.AllowUserToDeleteRows = false;
            this.dgvNamespaces.AllowUserToOrderColumns = true;
            this.dgvNamespaces.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvNamespaces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNamespaces.Location = new System.Drawing.Point(8, 172);
            this.dgvNamespaces.MultiSelect = false;
            this.dgvNamespaces.Name = "dgvNamespaces";
            this.dgvNamespaces.ReadOnly = true;
            this.dgvNamespaces.Size = new System.Drawing.Size(674, 150);
            this.dgvNamespaces.TabIndex = 23;
            this.dgvNamespaces.SelectionChanged += new System.EventHandler(this.dgvNamespaces_SelectionChanged);
            // 
            // dgvTypes
            // 
            this.dgvTypes.AllowUserToAddRows = false;
            this.dgvTypes.AllowUserToDeleteRows = false;
            this.dgvTypes.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTypes.Location = new System.Drawing.Point(8, 347);
            this.dgvTypes.MultiSelect = false;
            this.dgvTypes.Name = "dgvTypes";
            this.dgvTypes.ReadOnly = true;
            this.dgvTypes.Size = new System.Drawing.Size(674, 150);
            this.dgvTypes.TabIndex = 24;
            this.dgvTypes.SelectionChanged += new System.EventHandler(this.dgvTypes_SelectionChanged);
            // 
            // dgvMethods
            // 
            this.dgvMethods.AllowUserToAddRows = false;
            this.dgvMethods.AllowUserToDeleteRows = false;
            this.dgvMethods.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvMethods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMethods.Location = new System.Drawing.Point(8, 524);
            this.dgvMethods.MultiSelect = false;
            this.dgvMethods.Name = "dgvMethods";
            this.dgvMethods.ReadOnly = true;
            this.dgvMethods.Size = new System.Drawing.Size(674, 151);
            this.dgvMethods.TabIndex = 25;
            this.dgvMethods.SelectionChanged += new System.EventHandler(this.dgvMethods_SelectionChanged);
            // 
            // dgvAssemblies
            // 
            this.dgvAssemblies.AllowUserToAddRows = false;
            this.dgvAssemblies.AllowUserToDeleteRows = false;
            this.dgvAssemblies.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvAssemblies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAssemblies.Location = new System.Drawing.Point(8, 64);
            this.dgvAssemblies.MultiSelect = false;
            this.dgvAssemblies.Name = "dgvAssemblies";
            this.dgvAssemblies.ReadOnly = true;
            this.dgvAssemblies.Size = new System.Drawing.Size(674, 89);
            this.dgvAssemblies.TabIndex = 26;
            this.dgvAssemblies.SelectionChanged += new System.EventHandler(this.dgvAssemblies_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(688, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Code Element Type:";
            // 
            // codeSectionsTabs
            // 
            this.codeSectionsTabs.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.codeSectionsTabs.Controls.Add(this.tabCodeMetrics);
            this.codeSectionsTabs.Controls.Add(this.tabUnitTests);
            this.codeSectionsTabs.Controls.Add(this.tabSpecFlowBDD);
            this.codeSectionsTabs.Location = new System.Drawing.Point(12, 60);
            this.codeSectionsTabs.Name = "codeSectionsTabs";
            this.codeSectionsTabs.SelectedIndex = 0;
            this.codeSectionsTabs.Size = new System.Drawing.Size(966, 720);
            this.codeSectionsTabs.TabIndex = 28;
            // 
            // tabCodeMetrics
            // 
            this.tabCodeMetrics.Controls.Add(this.label1);
            this.tabCodeMetrics.Controls.Add(this.dgvAssemblies);
            this.tabCodeMetrics.Controls.Add(this.dgvMethods);
            this.tabCodeMetrics.Controls.Add(this.lblAssembliesList);
            this.tabCodeMetrics.Controls.Add(this.dgvTypes);
            this.tabCodeMetrics.Controls.Add(this.lblNamespacesList);
            this.tabCodeMetrics.Controls.Add(this.dgvNamespaces);
            this.tabCodeMetrics.Controls.Add(this.lvwMetricsList);
            this.tabCodeMetrics.Controls.Add(this.lblCodeElementName);
            this.tabCodeMetrics.Controls.Add(this.lblTypesList);
            this.tabCodeMetrics.Controls.Add(this.lblCodeElementType);
            this.tabCodeMetrics.Controls.Add(this.lblMethodsList);
            this.tabCodeMetrics.Controls.Add(this.rtfMetricProperties);
            this.tabCodeMetrics.Location = new System.Drawing.Point(4, 4);
            this.tabCodeMetrics.Name = "tabCodeMetrics";
            this.tabCodeMetrics.Padding = new System.Windows.Forms.Padding(3);
            this.tabCodeMetrics.Size = new System.Drawing.Size(958, 694);
            this.tabCodeMetrics.TabIndex = 0;
            this.tabCodeMetrics.Text = "Code";
            this.tabCodeMetrics.UseVisualStyleBackColor = true;
            // 
            // tabUnitTests
            // 
            this.tabUnitTests.Location = new System.Drawing.Point(4, 4);
            this.tabUnitTests.Name = "tabUnitTests";
            this.tabUnitTests.Padding = new System.Windows.Forms.Padding(3);
            this.tabUnitTests.Size = new System.Drawing.Size(958, 694);
            this.tabUnitTests.TabIndex = 1;
            this.tabUnitTests.Text = "Unit Tests";
            this.tabUnitTests.UseVisualStyleBackColor = true;
            // 
            // tabSpecFlowBDD
            // 
            this.tabSpecFlowBDD.Location = new System.Drawing.Point(4, 4);
            this.tabSpecFlowBDD.Name = "tabSpecFlowBDD";
            this.tabSpecFlowBDD.Size = new System.Drawing.Size(958, 694);
            this.tabSpecFlowBDD.TabIndex = 2;
            this.tabSpecFlowBDD.Text = "SpecFlow BDD";
            this.tabSpecFlowBDD.UseVisualStyleBackColor = true;
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ndependProjectToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(989, 24);
            this.menuMain.TabIndex = 29;
            this.menuMain.Text = "menuStrip1";
            // 
            // ndependProjectToolStripMenuItem
            // 
            this.ndependProjectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.ndependProjectToolStripMenuItem.Name = "ndependProjectToolStripMenuItem";
            this.ndependProjectToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.ndependProjectToolStripMenuItem.Text = "Ndepend Project";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tBoxProjectPath
            // 
            this.tBoxProjectPath.Location = new System.Drawing.Point(466, 30);
            this.tBoxProjectPath.Name = "tBoxProjectPath";
            this.tBoxProjectPath.ReadOnly = true;
            this.tBoxProjectPath.Size = new System.Drawing.Size(508, 20);
            this.tBoxProjectPath.TabIndex = 30;
            // 
            // lblNDependProjectPath
            // 
            this.lblNDependProjectPath.AutoSize = true;
            this.lblNDependProjectPath.Location = new System.Drawing.Point(389, 33);
            this.lblNDependProjectPath.Name = "lblNDependProjectPath";
            this.lblNDependProjectPath.Size = new System.Drawing.Size(65, 13);
            this.lblNDependProjectPath.TabIndex = 31;
            this.lblNDependProjectPath.Text = "Project Path";
            // 
            // MetricsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 792);
            this.Controls.Add(this.lblNDependProjectPath);
            this.Controls.Add(this.tBoxProjectPath);
            this.Controls.Add(this.tboxProjectName);
            this.Controls.Add(this.codeSectionsTabs);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.lblNDependProjectName);
            this.MainMenuStrip = this.menuMain;
            this.Name = "MetricsViewer";
            this.Text = "Metrics Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.dgvNamespaces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMethods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssemblies)).EndInit();
            this.codeSectionsTabs.ResumeLayout(false);
            this.tabCodeMetrics.ResumeLayout(false);
            this.tabCodeMetrics.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tboxProjectName;
        private System.Windows.Forms.Label lblNDependProjectName;
        private System.Windows.Forms.Label lblAssembliesList;
        private System.Windows.Forms.Label lblNamespacesList;
        private System.Windows.Forms.ListView lvwMetricsList;
        private System.Windows.Forms.ColumnHeader MetricName;
        private System.Windows.Forms.ColumnHeader MetricValue;
        private System.Windows.Forms.Label lblTypesList;
        private System.Windows.Forms.Label lblMethodsList;
        private System.Windows.Forms.RichTextBox rtfMetricProperties;
        private System.Windows.Forms.Label lblCodeElementType;
        private System.Windows.Forms.Label lblCodeElementName;
        private System.Windows.Forms.DataGridView dgvNamespaces;
        private System.Windows.Forms.DataGridView dgvTypes;
        private System.Windows.Forms.DataGridView dgvMethods;
        private System.Windows.Forms.DataGridView dgvAssemblies;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl codeSectionsTabs;
        private System.Windows.Forms.TabPage tabCodeMetrics;
        private System.Windows.Forms.TabPage tabUnitTests;
        private System.Windows.Forms.TabPage tabSpecFlowBDD;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem ndependProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TextBox tBoxProjectPath;
        private System.Windows.Forms.Label lblNDependProjectPath;
    }
}

