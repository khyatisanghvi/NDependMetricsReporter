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
            this.lblCodeAssembliesDataGridView = new System.Windows.Forms.Label();
            this.lblCodeNamespacesDataGridView = new System.Windows.Forms.Label();
            this.lvwCodeMetricsList = new System.Windows.Forms.ListView();
            this.MetricName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MetricValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblCodeTypesDataGridView = new System.Windows.Forms.Label();
            this.lblCodeMethodsDataGridView = new System.Windows.Forms.Label();
            this.rtfCodeMetricProperties = new System.Windows.Forms.RichTextBox();
            this.lblCodeElementType = new System.Windows.Forms.Label();
            this.lblCodeElementName = new System.Windows.Forms.Label();
            this.dgvCodeNamespaces = new System.Windows.Forms.DataGridView();
            this.dgvCodeTypes = new System.Windows.Forms.DataGridView();
            this.dgvCodeMethods = new System.Windows.Forms.DataGridView();
            this.dgvCodeAssemblies = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.codeSectionsTabs = new System.Windows.Forms.TabControl();
            this.tabCodeMetrics = new System.Windows.Forms.TabPage();
            this.tabUnitTests = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvUnitTestsAssemblies = new System.Windows.Forms.DataGridView();
            this.dgvUnitTestsMethods = new System.Windows.Forms.DataGridView();
            this.lblUnitTestsAssembliesDataGridView = new System.Windows.Forms.Label();
            this.dgvUnitTestsTypes = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvUnitTestsNamespaces = new System.Windows.Forms.DataGridView();
            this.lvwUnitTestsMetricsList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblUnitTestsCodeElementName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblUnitTestsCodeElementType = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.rtfUnitTestsMetricProperties = new System.Windows.Forms.RichTextBox();
            this.tabSpecFlowBDD = new System.Windows.Forms.TabPage();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.ndependProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tBoxProjectPath = new System.Windows.Forms.TextBox();
            this.lblNDependProjectPath = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodeNamespaces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodeTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodeMethods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodeAssemblies)).BeginInit();
            this.codeSectionsTabs.SuspendLayout();
            this.tabCodeMetrics.SuspendLayout();
            this.tabUnitTests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnitTestsAssemblies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnitTestsMethods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnitTestsTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnitTestsNamespaces)).BeginInit();
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
            // lblCodeAssembliesDataGridView
            // 
            this.lblCodeAssembliesDataGridView.AutoSize = true;
            this.lblCodeAssembliesDataGridView.Location = new System.Drawing.Point(3, 3);
            this.lblCodeAssembliesDataGridView.Name = "lblCodeAssembliesDataGridView";
            this.lblCodeAssembliesDataGridView.Size = new System.Drawing.Size(193, 13);
            this.lblCodeAssembliesDataGridView.TabIndex = 9;
            this.lblCodeAssembliesDataGridView.Text = "Assemblies analyzed in selected project";
            // 
            // lblCodeNamespacesDataGridView
            // 
            this.lblCodeNamespacesDataGridView.AutoSize = true;
            this.lblCodeNamespacesDataGridView.Location = new System.Drawing.Point(3, 113);
            this.lblCodeNamespacesDataGridView.Name = "lblCodeNamespacesDataGridView";
            this.lblCodeNamespacesDataGridView.Size = new System.Drawing.Size(169, 13);
            this.lblCodeNamespacesDataGridView.TabIndex = 11;
            this.lblCodeNamespacesDataGridView.Text = "Namespaces in selected assembly";
            // 
            // lvwCodeMetricsList
            // 
            this.lvwCodeMetricsList.CheckBoxes = true;
            this.lvwCodeMetricsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.MetricName,
            this.MetricValue});
            this.lvwCodeMetricsList.GridLines = true;
            this.lvwCodeMetricsList.Location = new System.Drawing.Point(686, 49);
            this.lvwCodeMetricsList.Name = "lvwCodeMetricsList";
            this.lvwCodeMetricsList.Size = new System.Drawing.Size(262, 386);
            this.lvwCodeMetricsList.TabIndex = 13;
            this.lvwCodeMetricsList.UseCompatibleStateImageBehavior = false;
            this.lvwCodeMetricsList.View = System.Windows.Forms.View.Details;
            this.lvwCodeMetricsList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvwCodeMetricsList_ItemCheck);
            this.lvwCodeMetricsList.SelectedIndexChanged += new System.EventHandler(this.lvwCodeMetricsList_SelectedIndexChanged);
            this.lvwCodeMetricsList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwCodeMetricsList_MouseDoubleClick);
            this.lvwCodeMetricsList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvwCodeMetricsList_MouseDown);
            this.lvwCodeMetricsList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvwCodeMetricsList_MouseUp);
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
            // lblCodeTypesDataGridView
            // 
            this.lblCodeTypesDataGridView.AutoSize = true;
            this.lblCodeTypesDataGridView.Location = new System.Drawing.Point(3, 224);
            this.lblCodeTypesDataGridView.Name = "lblCodeTypesDataGridView";
            this.lblCodeTypesDataGridView.Size = new System.Drawing.Size(150, 13);
            this.lblCodeTypesDataGridView.TabIndex = 15;
            this.lblCodeTypesDataGridView.Text = "Types in selected Namespace";
            // 
            // lblCodeMethodsDataGridView
            // 
            this.lblCodeMethodsDataGridView.AutoSize = true;
            this.lblCodeMethodsDataGridView.Location = new System.Drawing.Point(3, 376);
            this.lblCodeMethodsDataGridView.Name = "lblCodeMethodsDataGridView";
            this.lblCodeMethodsDataGridView.Size = new System.Drawing.Size(129, 13);
            this.lblCodeMethodsDataGridView.TabIndex = 17;
            this.lblCodeMethodsDataGridView.Text = "Methods in selected Type";
            // 
            // rtfCodeMetricProperties
            // 
            this.rtfCodeMetricProperties.Location = new System.Drawing.Point(686, 441);
            this.rtfCodeMetricProperties.Name = "rtfCodeMetricProperties";
            this.rtfCodeMetricProperties.Size = new System.Drawing.Size(262, 108);
            this.rtfCodeMetricProperties.TabIndex = 20;
            this.rtfCodeMetricProperties.Text = "";
            // 
            // lblCodeElementType
            // 
            this.lblCodeElementType.AutoSize = true;
            this.lblCodeElementType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodeElementType.Location = new System.Drawing.Point(792, 18);
            this.lblCodeElementType.Name = "lblCodeElementType";
            this.lblCodeElementType.Size = new System.Drawing.Size(100, 13);
            this.lblCodeElementType.TabIndex = 21;
            this.lblCodeElementType.Text = "Code Element Type";
            // 
            // lblCodeElementName
            // 
            this.lblCodeElementName.AutoSize = true;
            this.lblCodeElementName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodeElementName.Location = new System.Drawing.Point(686, 33);
            this.lblCodeElementName.Name = "lblCodeElementName";
            this.lblCodeElementName.Size = new System.Drawing.Size(104, 13);
            this.lblCodeElementName.TabIndex = 22;
            this.lblCodeElementName.Text = "Code Element Name";
            // 
            // dgvCodeNamespaces
            // 
            this.dgvCodeNamespaces.AllowUserToAddRows = false;
            this.dgvCodeNamespaces.AllowUserToDeleteRows = false;
            this.dgvCodeNamespaces.AllowUserToOrderColumns = true;
            this.dgvCodeNamespaces.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvCodeNamespaces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCodeNamespaces.Location = new System.Drawing.Point(6, 129);
            this.dgvCodeNamespaces.MultiSelect = false;
            this.dgvCodeNamespaces.Name = "dgvCodeNamespaces";
            this.dgvCodeNamespaces.ReadOnly = true;
            this.dgvCodeNamespaces.Size = new System.Drawing.Size(674, 88);
            this.dgvCodeNamespaces.TabIndex = 23;
            this.dgvCodeNamespaces.SelectionChanged += new System.EventHandler(this.dgvCodeNamespaces_SelectionChanged);
            // 
            // dgvCodeTypes
            // 
            this.dgvCodeTypes.AllowUserToAddRows = false;
            this.dgvCodeTypes.AllowUserToDeleteRows = false;
            this.dgvCodeTypes.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvCodeTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCodeTypes.Location = new System.Drawing.Point(6, 240);
            this.dgvCodeTypes.MultiSelect = false;
            this.dgvCodeTypes.Name = "dgvCodeTypes";
            this.dgvCodeTypes.ReadOnly = true;
            this.dgvCodeTypes.Size = new System.Drawing.Size(674, 131);
            this.dgvCodeTypes.TabIndex = 24;
            this.dgvCodeTypes.SelectionChanged += new System.EventHandler(this.dgvCodeTypes_SelectionChanged);
            // 
            // dgvCodeMethods
            // 
            this.dgvCodeMethods.AllowUserToAddRows = false;
            this.dgvCodeMethods.AllowUserToDeleteRows = false;
            this.dgvCodeMethods.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvCodeMethods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCodeMethods.Location = new System.Drawing.Point(6, 392);
            this.dgvCodeMethods.MultiSelect = false;
            this.dgvCodeMethods.Name = "dgvCodeMethods";
            this.dgvCodeMethods.ReadOnly = true;
            this.dgvCodeMethods.Size = new System.Drawing.Size(674, 157);
            this.dgvCodeMethods.TabIndex = 25;
            this.dgvCodeMethods.SelectionChanged += new System.EventHandler(this.dgvCodeMethods_SelectionChanged);
            // 
            // dgvCodeAssemblies
            // 
            this.dgvCodeAssemblies.AllowUserToAddRows = false;
            this.dgvCodeAssemblies.AllowUserToDeleteRows = false;
            this.dgvCodeAssemblies.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvCodeAssemblies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCodeAssemblies.Location = new System.Drawing.Point(6, 19);
            this.dgvCodeAssemblies.MultiSelect = false;
            this.dgvCodeAssemblies.Name = "dgvCodeAssemblies";
            this.dgvCodeAssemblies.ReadOnly = true;
            this.dgvCodeAssemblies.Size = new System.Drawing.Size(674, 89);
            this.dgvCodeAssemblies.TabIndex = 26;
            this.dgvCodeAssemblies.SelectionChanged += new System.EventHandler(this.dgvCodeAssemblies_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(686, 18);
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
            this.codeSectionsTabs.Size = new System.Drawing.Size(966, 584);
            this.codeSectionsTabs.TabIndex = 28;
            // 
            // tabCodeMetrics
            // 
            this.tabCodeMetrics.BackColor = System.Drawing.SystemColors.Control;
            this.tabCodeMetrics.Controls.Add(this.label1);
            this.tabCodeMetrics.Controls.Add(this.dgvCodeAssemblies);
            this.tabCodeMetrics.Controls.Add(this.dgvCodeMethods);
            this.tabCodeMetrics.Controls.Add(this.lblCodeAssembliesDataGridView);
            this.tabCodeMetrics.Controls.Add(this.dgvCodeTypes);
            this.tabCodeMetrics.Controls.Add(this.lblCodeNamespacesDataGridView);
            this.tabCodeMetrics.Controls.Add(this.dgvCodeNamespaces);
            this.tabCodeMetrics.Controls.Add(this.lvwCodeMetricsList);
            this.tabCodeMetrics.Controls.Add(this.lblCodeElementName);
            this.tabCodeMetrics.Controls.Add(this.lblCodeTypesDataGridView);
            this.tabCodeMetrics.Controls.Add(this.lblCodeElementType);
            this.tabCodeMetrics.Controls.Add(this.lblCodeMethodsDataGridView);
            this.tabCodeMetrics.Controls.Add(this.rtfCodeMetricProperties);
            this.tabCodeMetrics.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabCodeMetrics.Location = new System.Drawing.Point(4, 4);
            this.tabCodeMetrics.Name = "tabCodeMetrics";
            this.tabCodeMetrics.Padding = new System.Windows.Forms.Padding(3);
            this.tabCodeMetrics.Size = new System.Drawing.Size(958, 558);
            this.tabCodeMetrics.TabIndex = 0;
            this.tabCodeMetrics.Text = "Code";
            // 
            // tabUnitTests
            // 
            this.tabUnitTests.BackColor = System.Drawing.SystemColors.Control;
            this.tabUnitTests.Controls.Add(this.label2);
            this.tabUnitTests.Controls.Add(this.dgvUnitTestsAssemblies);
            this.tabUnitTests.Controls.Add(this.dgvUnitTestsMethods);
            this.tabUnitTests.Controls.Add(this.lblUnitTestsAssembliesDataGridView);
            this.tabUnitTests.Controls.Add(this.dgvUnitTestsTypes);
            this.tabUnitTests.Controls.Add(this.label4);
            this.tabUnitTests.Controls.Add(this.dgvUnitTestsNamespaces);
            this.tabUnitTests.Controls.Add(this.lvwUnitTestsMetricsList);
            this.tabUnitTests.Controls.Add(this.lblUnitTestsCodeElementName);
            this.tabUnitTests.Controls.Add(this.label6);
            this.tabUnitTests.Controls.Add(this.lblUnitTestsCodeElementType);
            this.tabUnitTests.Controls.Add(this.label8);
            this.tabUnitTests.Controls.Add(this.rtfUnitTestsMetricProperties);
            this.tabUnitTests.Location = new System.Drawing.Point(4, 4);
            this.tabUnitTests.Name = "tabUnitTests";
            this.tabUnitTests.Padding = new System.Windows.Forms.Padding(3);
            this.tabUnitTests.Size = new System.Drawing.Size(958, 558);
            this.tabUnitTests.TabIndex = 1;
            this.tabUnitTests.Text = "Unit Tests";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(686, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Code Element Type:";
            // 
            // dgvUnitTestsAssemblies
            // 
            this.dgvUnitTestsAssemblies.AllowUserToAddRows = false;
            this.dgvUnitTestsAssemblies.AllowUserToDeleteRows = false;
            this.dgvUnitTestsAssemblies.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvUnitTestsAssemblies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnitTestsAssemblies.Location = new System.Drawing.Point(6, 19);
            this.dgvUnitTestsAssemblies.MultiSelect = false;
            this.dgvUnitTestsAssemblies.Name = "dgvUnitTestsAssemblies";
            this.dgvUnitTestsAssemblies.ReadOnly = true;
            this.dgvUnitTestsAssemblies.Size = new System.Drawing.Size(674, 89);
            this.dgvUnitTestsAssemblies.TabIndex = 39;
            this.dgvUnitTestsAssemblies.SelectionChanged += new System.EventHandler(this.dgvUnitTestsAssemblies_SelectionChanged);
            // 
            // dgvUnitTestsMethods
            // 
            this.dgvUnitTestsMethods.AllowUserToAddRows = false;
            this.dgvUnitTestsMethods.AllowUserToDeleteRows = false;
            this.dgvUnitTestsMethods.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvUnitTestsMethods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnitTestsMethods.Location = new System.Drawing.Point(6, 392);
            this.dgvUnitTestsMethods.MultiSelect = false;
            this.dgvUnitTestsMethods.Name = "dgvUnitTestsMethods";
            this.dgvUnitTestsMethods.ReadOnly = true;
            this.dgvUnitTestsMethods.Size = new System.Drawing.Size(674, 156);
            this.dgvUnitTestsMethods.TabIndex = 38;
            // 
            // lblUnitTestsAssembliesDataGridView
            // 
            this.lblUnitTestsAssembliesDataGridView.AutoSize = true;
            this.lblUnitTestsAssembliesDataGridView.Location = new System.Drawing.Point(3, 3);
            this.lblUnitTestsAssembliesDataGridView.Name = "lblUnitTestsAssembliesDataGridView";
            this.lblUnitTestsAssembliesDataGridView.Size = new System.Drawing.Size(171, 13);
            this.lblUnitTestsAssembliesDataGridView.TabIndex = 28;
            this.lblUnitTestsAssembliesDataGridView.Text = "Test assemblies in selected project";
            // 
            // dgvUnitTestsTypes
            // 
            this.dgvUnitTestsTypes.AllowUserToAddRows = false;
            this.dgvUnitTestsTypes.AllowUserToDeleteRows = false;
            this.dgvUnitTestsTypes.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvUnitTestsTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnitTestsTypes.Location = new System.Drawing.Point(6, 240);
            this.dgvUnitTestsTypes.MultiSelect = false;
            this.dgvUnitTestsTypes.Name = "dgvUnitTestsTypes";
            this.dgvUnitTestsTypes.ReadOnly = true;
            this.dgvUnitTestsTypes.Size = new System.Drawing.Size(674, 131);
            this.dgvUnitTestsTypes.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Test namespaces in selected assembly";
            // 
            // dgvUnitTestsNamespaces
            // 
            this.dgvUnitTestsNamespaces.AllowUserToAddRows = false;
            this.dgvUnitTestsNamespaces.AllowUserToDeleteRows = false;
            this.dgvUnitTestsNamespaces.AllowUserToOrderColumns = true;
            this.dgvUnitTestsNamespaces.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvUnitTestsNamespaces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnitTestsNamespaces.Location = new System.Drawing.Point(6, 129);
            this.dgvUnitTestsNamespaces.MultiSelect = false;
            this.dgvUnitTestsNamespaces.Name = "dgvUnitTestsNamespaces";
            this.dgvUnitTestsNamespaces.ReadOnly = true;
            this.dgvUnitTestsNamespaces.Size = new System.Drawing.Size(674, 88);
            this.dgvUnitTestsNamespaces.TabIndex = 36;
            this.dgvUnitTestsNamespaces.SelectionChanged += new System.EventHandler(this.dgvUnitTestsNamespaces_SelectionChanged);
            // 
            // lvwUnitTestsMetricsList
            // 
            this.lvwUnitTestsMetricsList.CheckBoxes = true;
            this.lvwUnitTestsMetricsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvwUnitTestsMetricsList.GridLines = true;
            this.lvwUnitTestsMetricsList.Location = new System.Drawing.Point(686, 49);
            this.lvwUnitTestsMetricsList.Name = "lvwUnitTestsMetricsList";
            this.lvwUnitTestsMetricsList.Size = new System.Drawing.Size(262, 386);
            this.lvwUnitTestsMetricsList.TabIndex = 30;
            this.lvwUnitTestsMetricsList.UseCompatibleStateImageBehavior = false;
            this.lvwUnitTestsMetricsList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Metric Name";
            this.columnHeader1.Width = 195;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblUnitTestsCodeElementName
            // 
            this.lblUnitTestsCodeElementName.AutoSize = true;
            this.lblUnitTestsCodeElementName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnitTestsCodeElementName.Location = new System.Drawing.Point(686, 33);
            this.lblUnitTestsCodeElementName.Name = "lblUnitTestsCodeElementName";
            this.lblUnitTestsCodeElementName.Size = new System.Drawing.Size(104, 13);
            this.lblUnitTestsCodeElementName.TabIndex = 35;
            this.lblUnitTestsCodeElementName.Text = "Code Element Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Test groups in selected namespace";
            // 
            // lblUnitTestsCodeElementType
            // 
            this.lblUnitTestsCodeElementType.AutoSize = true;
            this.lblUnitTestsCodeElementType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnitTestsCodeElementType.Location = new System.Drawing.Point(792, 18);
            this.lblUnitTestsCodeElementType.Name = "lblUnitTestsCodeElementType";
            this.lblUnitTestsCodeElementType.Size = new System.Drawing.Size(100, 13);
            this.lblUnitTestsCodeElementType.TabIndex = 34;
            this.lblUnitTestsCodeElementType.Text = "Code Element Type";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 376);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(139, 13);
            this.label8.TabIndex = 32;
            this.label8.Text = "Unit Tests in selected group";
            // 
            // rtfUnitTestsMetricProperties
            // 
            this.rtfUnitTestsMetricProperties.Location = new System.Drawing.Point(685, 440);
            this.rtfUnitTestsMetricProperties.Name = "rtfUnitTestsMetricProperties";
            this.rtfUnitTestsMetricProperties.Size = new System.Drawing.Size(262, 108);
            this.rtfUnitTestsMetricProperties.TabIndex = 33;
            this.rtfUnitTestsMetricProperties.Text = "";
            // 
            // tabSpecFlowBDD
            // 
            this.tabSpecFlowBDD.BackColor = System.Drawing.SystemColors.Control;
            this.tabSpecFlowBDD.Location = new System.Drawing.Point(4, 4);
            this.tabSpecFlowBDD.Name = "tabSpecFlowBDD";
            this.tabSpecFlowBDD.Size = new System.Drawing.Size(958, 558);
            this.tabSpecFlowBDD.TabIndex = 2;
            this.tabSpecFlowBDD.Text = "SpecFlow BDD";
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
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
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
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(989, 649);
            this.Controls.Add(this.lblNDependProjectPath);
            this.Controls.Add(this.tBoxProjectPath);
            this.Controls.Add(this.tboxProjectName);
            this.Controls.Add(this.codeSectionsTabs);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.lblNDependProjectName);
            this.MainMenuStrip = this.menuMain;
            this.Name = "MetricsViewer";
            this.Text = "Metrics Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodeNamespaces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodeTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodeMethods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodeAssemblies)).EndInit();
            this.codeSectionsTabs.ResumeLayout(false);
            this.tabCodeMetrics.ResumeLayout(false);
            this.tabCodeMetrics.PerformLayout();
            this.tabUnitTests.ResumeLayout(false);
            this.tabUnitTests.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnitTestsAssemblies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnitTestsMethods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnitTestsTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnitTestsNamespaces)).EndInit();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tboxProjectName;
        private System.Windows.Forms.Label lblNDependProjectName;
        private System.Windows.Forms.Label lblCodeAssembliesDataGridView;
        private System.Windows.Forms.Label lblCodeNamespacesDataGridView;
        private System.Windows.Forms.ListView lvwCodeMetricsList;
        private System.Windows.Forms.ColumnHeader MetricName;
        private System.Windows.Forms.ColumnHeader MetricValue;
        private System.Windows.Forms.Label lblCodeTypesDataGridView;
        private System.Windows.Forms.Label lblCodeMethodsDataGridView;
        private System.Windows.Forms.RichTextBox rtfCodeMetricProperties;
        private System.Windows.Forms.Label lblCodeElementType;
        private System.Windows.Forms.Label lblCodeElementName;
        private System.Windows.Forms.DataGridView dgvCodeNamespaces;
        private System.Windows.Forms.DataGridView dgvCodeTypes;
        private System.Windows.Forms.DataGridView dgvCodeMethods;
        private System.Windows.Forms.DataGridView dgvCodeAssemblies;
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvUnitTestsAssemblies;
        private System.Windows.Forms.DataGridView dgvUnitTestsMethods;
        private System.Windows.Forms.Label lblUnitTestsAssembliesDataGridView;
        private System.Windows.Forms.DataGridView dgvUnitTestsTypes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvUnitTestsNamespaces;
        private System.Windows.Forms.ListView lvwUnitTestsMetricsList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label lblUnitTestsCodeElementName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblUnitTestsCodeElementType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox rtfUnitTestsMetricProperties;
    }
}

