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
            this.lblNDependProjectSelector = new System.Windows.Forms.Label();
            this.btnOpenProject = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvNamespaces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMethods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssemblies)).BeginInit();
            this.SuspendLayout();
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
            this.lblAssembliesList.Location = new System.Drawing.Point(25, 54);
            this.lblAssembliesList.Name = "lblAssembliesList";
            this.lblAssembliesList.Size = new System.Drawing.Size(193, 13);
            this.lblAssembliesList.TabIndex = 9;
            this.lblAssembliesList.Text = "Assemblies analyzed in selected project";
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
            this.lvwMetricsList.CheckBoxes = true;
            this.lvwMetricsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.MetricName,
            this.MetricValue});
            this.lvwMetricsList.GridLines = true;
            this.lvwMetricsList.Location = new System.Drawing.Point(710, 70);
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
            this.lblTypesList.Location = new System.Drawing.Point(25, 337);
            this.lblTypesList.Name = "lblTypesList";
            this.lblTypesList.Size = new System.Drawing.Size(150, 13);
            this.lblTypesList.TabIndex = 15;
            this.lblTypesList.Text = "Types in selected Namespace";
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
            // rtfMetricProperties
            // 
            this.rtfMetricProperties.Location = new System.Drawing.Point(710, 573);
            this.rtfMetricProperties.Name = "rtfMetricProperties";
            this.rtfMetricProperties.Size = new System.Drawing.Size(262, 108);
            this.rtfMetricProperties.TabIndex = 20;
            this.rtfMetricProperties.Text = "";
            // 
            // lblCodeElementType
            // 
            this.lblCodeElementType.AutoSize = true;
            this.lblCodeElementType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodeElementType.Location = new System.Drawing.Point(816, 40);
            this.lblCodeElementType.Name = "lblCodeElementType";
            this.lblCodeElementType.Size = new System.Drawing.Size(100, 13);
            this.lblCodeElementType.TabIndex = 21;
            this.lblCodeElementType.Text = "Code Element Type";
            // 
            // lblCodeElementName
            // 
            this.lblCodeElementName.AutoSize = true;
            this.lblCodeElementName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodeElementName.Location = new System.Drawing.Point(710, 55);
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
            this.dgvNamespaces.Location = new System.Drawing.Point(30, 178);
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
            this.dgvTypes.Location = new System.Drawing.Point(30, 353);
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
            this.dgvMethods.Location = new System.Drawing.Point(30, 530);
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
            this.dgvAssemblies.Location = new System.Drawing.Point(30, 70);
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
            this.label1.Location = new System.Drawing.Point(710, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Code Element Type:";
            // 
            // MetricsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 694);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvAssemblies);
            this.Controls.Add(this.dgvMethods);
            this.Controls.Add(this.dgvTypes);
            this.Controls.Add(this.dgvNamespaces);
            this.Controls.Add(this.lblCodeElementName);
            this.Controls.Add(this.lblCodeElementType);
            this.Controls.Add(this.rtfMetricProperties);
            this.Controls.Add(this.lblMethodsList);
            this.Controls.Add(this.lblTypesList);
            this.Controls.Add(this.lvwMetricsList);
            this.Controls.Add(this.lblNamespacesList);
            this.Controls.Add(this.lblAssembliesList);
            this.Controls.Add(this.btnOpenProject);
            this.Controls.Add(this.lblNDependProjectSelector);
            this.Controls.Add(this.tboxProjectName);
            this.Name = "MetricsViewer";
            this.Text = "Metrics Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.dgvNamespaces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMethods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssemblies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tboxProjectName;
        private System.Windows.Forms.Label lblNDependProjectSelector;
        private System.Windows.Forms.Button btnOpenProject;
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
    }
}

