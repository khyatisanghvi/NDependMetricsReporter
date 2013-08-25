using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using NDepend;
using NDepend.Project;
using NDepend.CodeModel;
using System.Reflection;
using ExtensionMethods;

namespace NDependMetricsReporter
{
    public partial class MetricsViewer : Form
    {
        bool inhibitAutocheckOnDoubleClick;
        NDependServicesProvider nDependServicesProvider;
        CodeElementsManager codeElementsManager;
        IProject nDependProject;

        public MetricsViewer()
        {
            InitializeComponent();
            nDependServicesProvider = new NDependServicesProvider();           
        }

        private void FillBaseControls()
        {
            this.tboxProjectName.Text = nDependProject.Properties.Name;
            List<NDependMetricDefinition> assemblyMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("AssemblyMetrics.xml");
            IEnumerable<IAssembly> lastAnalysisAssembliesList = codeElementsManager.GetNonThirdPartyAssembliesInApplication();
            DataTable assemblyMetricsDataTable = CreateCodeElemetMetricsDataTable<IAssembly>(lastAnalysisAssembliesList, assemblyMetricsDefinionsList);
            FillCodeElementsDataGridView(this.dgvAssemblies, assemblyMetricsDataTable);
        }

        private void FillCodeElementsDataGridView(DataGridView codeElementMetricsDataGridView, DataTable codeElementMetricsDataTable)
        {
            bool filledForFirstTime = (codeElementMetricsDataGridView.DataSource == null);
            codeElementMetricsDataGridView.DataSource = codeElementMetricsDataTable;
            if (filledForFirstTime)
            {
                codeElementMetricsDataGridView.RowHeadersWidth = 25;
                foreach (DataGridViewColumn dvgc in codeElementMetricsDataGridView.Columns)
                {
                    dvgc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dvgc.Visible = false;
                }
                codeElementMetricsDataGridView.Columns[0].Visible = true;
                codeElementMetricsDataGridView.Columns[0].Frozen = true;
            }
        }

        private void FillMetricsListView(DataGridViewRow selectedDataGridViewRow, List<NDependMetricDefinition> metricDefinitionsList)
        {
            string formatSpecifier = "0.####";
            DataGridView sourceDataGridView = selectedDataGridViewRow.DataGridView;
            this.lvwMetricsList.Tag = sourceDataGridView;
            this.lvwMetricsList.Items.Clear();
            foreach (NDependMetricDefinition metricDefinition in metricDefinitionsList)
            {
                double cellValue = Double.Parse(selectedDataGridViewRow.Cells[metricDefinition.PropertyName].Value.ToString());
                string formatedCellValue = cellValue.ToString(formatSpecifier);
                ListViewItem lvi = new ListViewItem(new[] { metricDefinition.PropertyName, formatedCellValue });
                lvi.Tag = metricDefinition;
                lvi.Checked = selectedDataGridViewRow.Cells[metricDefinition.PropertyName].Displayed;
                this.lvwMetricsList.Items.Add(lvi);
            }
        }

        private void FillMetricDescriptionRTFBox(NDependMetricDefinition nDependMetricDefinition)
        {
            this.rtfMetricProperties.Clear();
            this.rtfMetricProperties.AppendText(nDependMetricDefinition.MetricName);
            this.rtfMetricProperties.Find(nDependMetricDefinition.MetricName);
            this.rtfMetricProperties.SelectionFont = new Font(rtfMetricProperties.Font, rtfMetricProperties.Font.Style ^ FontStyle.Bold);
            this.rtfMetricProperties.SelectionStart = this.rtfMetricProperties.Text.Length;
            this.rtfMetricProperties.SelectionLength = 0;
            this.rtfMetricProperties.SelectionFont = rtfMetricProperties.Font;
            this.rtfMetricProperties.AppendText(Environment.NewLine + nDependMetricDefinition.Description);
        }

        private void ShowMetricChart(string chartTitle, string serieName, IList chartData)
        {
            MetricTrendChart metricTrendChart;
            if (Application.OpenForms["metricTrendChart"] == null)
            {
                metricTrendChart = new MetricTrendChart();
            }
            else
            {
                metricTrendChart = (MetricTrendChart)Application.OpenForms["metricTrendChart"];
            }
            metricTrendChart.RefreshData(chartTitle, serieName, chartData);
        }

        private DataTable CreateCodeElemetMetricsDataTable<CodeElementType>(IEnumerable<CodeElementType> codeElementLists, List<NDependMetricDefinition> metricsDefinitionList)
        {
            DataTable metricsTable = new DataTable();
            AddCodeElementsColumnToTable(metricsTable);
            AddMetricsColumnsToTable(metricsTable, metricsDefinitionList);
            AddMetricRowsToTable<CodeElementType>(metricsTable, codeElementLists, metricsDefinitionList);
            return metricsTable;
        }

        private void AddCodeElementsColumnToTable(DataTable metricsTable)
        {
            DataColumn codeElementNameColumn = new DataColumn("Code Element");
            codeElementNameColumn.DataType = typeof(string);
            metricsTable.Columns.Add(codeElementNameColumn);
        }

        private void AddMetricsColumnsToTable(DataTable metricsTable, List<NDependMetricDefinition> metricsDefinitionList)
        {
            foreach (NDependMetricDefinition metricDefinition in metricsDefinitionList)
            {
                DataColumn metricColumn = new DataColumn(metricDefinition.PropertyName);
                metricColumn.DataType = Type.GetType(metricDefinition.NDependMetricType); //typeof(double)
                metricsTable.Columns.Add(metricColumn);
            }
        }

        private void AddMetricRowsToTable<CodeElementType>(DataTable metricsTable, IEnumerable<CodeElementType> codeElementLists, List<NDependMetricDefinition> metricsDefinitionList)
        {
            foreach (CodeElementType codeElement in codeElementLists)
            {
                DataRow row = metricsTable.NewRow();
                string codeElementName = (string)typeof(CodeElementType).GetPublicProperty("Name").GetValue(codeElement);
                row[0] = codeElementName;
                foreach (NDependMetricDefinition metricDefinition in metricsDefinitionList)
                {
                    row[metricDefinition.PropertyName] = codeElementsManager.GetCodeElementMetricValue<CodeElementType>((CodeElementType)codeElement, metricDefinition);
                }
                metricsTable.Rows.Add(row);
            }
        }

        private void btnOpenProject_Click(object sender, EventArgs e)
        {
            nDependServicesProvider.ProjectManager.ShowDialogChooseAnExistingProject(out nDependProject);
            if (nDependProject == null) return;
            ICodeBase lastAnalysisCodebase = new CodeBaseManager(nDependProject).LoadLastCodebase();
            codeElementsManager = new CodeElementsManager(lastAnalysisCodebase);
            FillBaseControls();
        }

        private void lvwMetricsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lvwMetricsList.SelectedItems.Count > 0)
            {
                ListViewItem lvi = this.lvwMetricsList.SelectedItems[0];
                NDependMetricDefinition nDependMetricDefinition = (NDependMetricDefinition)lvi.Tag;
                IList metricValues = new AnalysisHistoryManager(nDependProject).GetMetricHistory(this.lblCodeElementName.Text, nDependMetricDefinition);
                string chartTitle = this.lblCodeElementType.Text.ToUpper() + ": " + this.lblCodeElementName.Text;
                ShowMetricChart(chartTitle, nDependMetricDefinition.MetricName, metricValues);
            }
        }

        private void lvwMetricsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (inhibitAutocheckOnDoubleClick)
            {
                e.NewValue = e.CurrentValue;
            }
            else
            {
                DataGridView sourceDataGridView = (DataGridView)lvwMetricsList.Tag;
                sourceDataGridView.Columns[this.lvwMetricsList.Items[e.Index].Text].Visible = (e.NewValue == CheckState.Checked);
            }
        }

        private void lvwMetricsList_MouseDown(object sender, MouseEventArgs e)
        {
            inhibitAutocheckOnDoubleClick = true;
        }

        private void lvwMetricsList_MouseUp(object sender, MouseEventArgs e)
        {
            inhibitAutocheckOnDoubleClick = false;
        }

        private void dgvAssemblies_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView thisDataGridView = (DataGridView)sender;
            if (thisDataGridView.SelectedRows.Count > 0)
            {
                string selectedAssemblyName = thisDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                IAssembly assembly = codeElementsManager.GetAssemblyByName(selectedAssemblyName);

                List<NDependMetricDefinition> assemblyMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("AssemblyMetrics.xml");
                FillMetricsListView(thisDataGridView.SelectedRows[0], assemblyMetricsDefinionsList);

                this.lblCodeElementType.Text = "Assembly";
                this.lblCodeElementName.Text = selectedAssemblyName;

                List<NDependMetricDefinition> namespaceMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("NamespaceMetrics.xml");
                DataTable namespaceMetricsDataTable = CreateCodeElemetMetricsDataTable<INamespace>(assembly.ChildNamespaces, namespaceMetricsDefinionsList);
                FillCodeElementsDataGridView(this.dgvNamespaces, namespaceMetricsDataTable);
            }

        }

        private void dgvNamespaces_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView thisDataGridView = (DataGridView)sender;
            if (thisDataGridView.SelectedRows.Count>0)
            {
                string selectedNamespaceName = thisDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                INamespace nNamespace = codeElementsManager.GetNamespaceByName(selectedNamespaceName);

                List<NDependMetricDefinition> namespaceMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("NamespaceMetrics.xml");
                FillMetricsListView(thisDataGridView.SelectedRows[0], namespaceMetricsDefinionsList);

                this.lblCodeElementType.Text = "Namespace";
                this.lblCodeElementName.Text = selectedNamespaceName;

                List<NDependMetricDefinition> typeMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("TypeMetrics.xml");
                DataTable typeMetricsDataTable = CreateCodeElemetMetricsDataTable<IType>(nNamespace.ChildTypes, typeMetricsDefinionsList);
                FillCodeElementsDataGridView(this.dgvTypes, typeMetricsDataTable); 
            }
        }

        private void dgvTypes_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView thisDataGridView = (DataGridView)sender;
            if (thisDataGridView.SelectedRows.Count > 0)
            {
                string selectedType = thisDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                IType nType = codeElementsManager.GetTypeByName(selectedType);

                List<NDependMetricDefinition> typesMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("TypeMetrics.xml");
                FillMetricsListView(thisDataGridView.SelectedRows[0], typesMetricsDefinionsList);

                this.lblCodeElementType.Text = "Type";
                this.lblCodeElementName.Text = selectedType;

                List<NDependMetricDefinition> methodMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("MethodMetrics.xml");
                DataTable methodMetricsDataTable = CreateCodeElemetMetricsDataTable<IMethod>(nType.MethodsAndContructors, methodMetricsDefinionsList);
                FillCodeElementsDataGridView(this.dgvMethods, methodMetricsDataTable);
            }
        }

        private void dgvMethods_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView thisDataGridView = (DataGridView)sender;
            if (thisDataGridView.SelectedRows.Count > 0)
            {
                string selectedType = thisDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                IType nType = codeElementsManager.GetTypeByName(selectedType);

                List<NDependMetricDefinition> typesMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("MethodMetrics.xml");
                FillMetricsListView(thisDataGridView.SelectedRows[0], typesMetricsDefinionsList);

                this.lblCodeElementType.Text = "Type";
                this.lblCodeElementName.Text = selectedType;
            }
        }

        private void lvwMetricsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwMetricsList.SelectedItems.Count > 0)
            {
                ListViewItem lvi = this.lvwMetricsList.SelectedItems[0];
                NDependMetricDefinition nDependMetricDefinition = (NDependMetricDefinition)lvi.Tag;
                FillMetricDescriptionRTFBox(nDependMetricDefinition);
            }
        }
    }
}
