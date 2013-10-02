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
            this.lvwCodeMetricsList = new System.Windows.Forms.ListView();
            this.MetricName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MetricValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblTypesList = new System.Windows.Forms.Label();
            this.lblMethodsList = new System.Windows.Forms.Label();
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
            this.tabSpecFlowBDD = new System.Windows.Forms.TabPage();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.ndependProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tBoxProjectPath = new System.Windows.Forms.TextBox();
            this.lblNDependProjectPath = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodeNamespaces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodeTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodeMethods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodeAssemblies)).BeginInit();
            this.codeSectionsTabs.SuspendLayout();
            this.tabCodeMetrics.SuspendLayout();
            this.tabUnitTests.SuspendLayout();
            this.menuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
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
            this.lblAssembliesList.Location = new System.Drawing.Point(1, 3);
            this.lblAssembliesList.Name = "lblAssembliesList";
            this.lblAssembliesList.Size = new System.Drawing.Size(193, 13);
            this.lblAssembliesList.TabIndex = 9;
            this.lblAssembliesList.Text = "Assemblies analyzed in selected project";
            // 
            // lblNamespacesList
            // 
            this.lblNamespacesList.AutoSize = true;
            this.lblNamespacesList.Location = new System.Drawing.Point(1, 113);
            this.lblNamespacesList.Name = "lblNamespacesList";
            this.lblNamespacesList.Size = new System.Drawing.Size(169, 13);
            this.lblNamespacesList.TabIndex = 11;
            this.lblNamespacesList.Text = "Namespaces in selected assembly";
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
            // lblTypesList
            // 
            this.lblTypesList.AutoSize = true;
            this.lblTypesList.Location = new System.Drawing.Point(1, 224);
            this.lblTypesList.Name = "lblTypesList";
            this.lblTypesList.Size = new System.Drawing.Size(150, 13);
            this.lblTypesList.TabIndex = 15;
            this.lblTypesList.Text = "Types in selected Namespace";
            // 
            // lblMethodsList
            // 
            this.lblMethodsList.AutoSize = true;
            this.lblMethodsList.Location = new System.Drawing.Point(3, 379);
            this.lblMethodsList.Name = "lblMethodsList";
            this.lblMethodsList.Size = new System.Drawing.Size(129, 13);
            this.lblMethodsList.TabIndex = 17;
            this.lblMethodsList.Text = "Methods in selected Type";
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
            this.dgvCodeMethods.Location = new System.Drawing.Point(6, 395);
            this.dgvCodeMethods.MultiSelect = false;
            this.dgvCodeMethods.Name = "dgvCodeMethods";
            this.dgvCodeMethods.ReadOnly = true;
            this.dgvCodeMethods.Size = new System.Drawing.Size(674, 154);
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
            this.tabCodeMetrics.Controls.Add(this.lblAssembliesList);
            this.tabCodeMetrics.Controls.Add(this.dgvCodeTypes);
            this.tabCodeMetrics.Controls.Add(this.lblNamespacesList);
            this.tabCodeMetrics.Controls.Add(this.dgvCodeNamespaces);
            this.tabCodeMetrics.Controls.Add(this.lvwCodeMetricsList);
            this.tabCodeMetrics.Controls.Add(this.lblCodeElementName);
            this.tabCodeMetrics.Controls.Add(this.lblTypesList);
            this.tabCodeMetrics.Controls.Add(this.lblCodeElementType);
            this.tabCodeMetrics.Controls.Add(this.lblMethodsList);
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
            this.tabUnitTests.Controls.Add(this.label2);
            this.tabUnitTests.Controls.Add(this.dataGridView1);
            this.tabUnitTests.Controls.Add(this.dataGridView2);
            this.tabUnitTests.Controls.Add(this.label3);
            this.tabUnitTests.Controls.Add(this.dataGridView3);
            this.tabUnitTests.Controls.Add(this.label4);
            this.tabUnitTests.Controls.Add(this.dataGridView4);
            this.tabUnitTests.Controls.Add(this.listView1);
            this.tabUnitTests.Controls.Add(this.label5);
            this.tabUnitTests.Controls.Add(this.label6);
            this.tabUnitTests.Controls.Add(this.label7);
            this.tabUnitTests.Controls.Add(this.label8);
            this.tabUnitTests.Controls.Add(this.richTextBox1);
            this.tabUnitTests.Location = new System.Drawing.Point(4, 4);
            this.tabUnitTests.Name = "tabUnitTests";
            this.tabUnitTests.Padding = new System.Windows.Forms.Padding(3);
            this.tabUnitTests.Size = new System.Drawing.Size(958, 640);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(691, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Code Element Type:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(11, 23);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(674, 89);
            this.dataGridView1.TabIndex = 39;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(11, 483);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(674, 151);
            this.dataGridView2.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Assemblies analyzed in selected project";
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(11, 306);
            this.dataGridView3.MultiSelect = false;
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.Size = new System.Drawing.Size(674, 150);
            this.dataGridView3.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Namespaces in selected assembly";
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.AllowUserToDeleteRows = false;
            this.dataGridView4.AllowUserToOrderColumns = true;
            this.dataGridView4.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(11, 131);
            this.dataGridView4.MultiSelect = false;
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.ReadOnly = true;
            this.dataGridView4.Size = new System.Drawing.Size(674, 150);
            this.dataGridView4.TabIndex = 36;
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(691, 53);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(262, 386);
            this.listView1.TabIndex = 30;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(691, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Code Element Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 290);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Types in selected Namespace";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(797, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Code Element Type";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 467);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 13);
            this.label8.TabIndex = 32;
            this.label8.Text = "Methods in selected Type";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(691, 526);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(262, 108);
            this.richTextBox1.TabIndex = 33;
            this.richTextBox1.Text = "";
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
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tboxProjectName;
        private System.Windows.Forms.Label lblNDependProjectName;
        private System.Windows.Forms.Label lblAssembliesList;
        private System.Windows.Forms.Label lblNamespacesList;
        private System.Windows.Forms.ListView lvwCodeMetricsList;
        private System.Windows.Forms.ColumnHeader MetricName;
        private System.Windows.Forms.ColumnHeader MetricValue;
        private System.Windows.Forms.Label lblTypesList;
        private System.Windows.Forms.Label lblMethodsList;
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
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

