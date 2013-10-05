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
        DataTableHelper dataTableHelper;

        public MetricsViewer()
        {
            InitializeComponent();
            nDependServicesProvider = new NDependServicesProvider();           
        }

        private void OpenNdependProject()
        {
            nDependServicesProvider.ProjectManager.ShowDialogChooseAnExistingProject(out nDependProject);
            if (nDependProject == null) return;
            ICodeBase lastAnalysisCodebase = new CodeBaseManager(nDependProject).LoadLastCodebase();
            codeElementsManager = new CodeElementsManager(lastAnalysisCodebase);
            dataTableHelper = new DataTableHelper(codeElementsManager);
            FillBaseControls();
        }

        private void FillBaseControls()
        {
            FillNDependProjectInfo();
            List<NDependMetricDefinition> assemblyMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("AssemblyMetrics.xml");
            IEnumerable<IAssembly> lastAnalysisAssembliesList = codeElementsManager.GetNonThirdPartyAssembliesInApplication();
            DataTable assemblyMetricsDataTable = dataTableHelper.CreateCodeElemetMetricsDataTable<IAssembly>(lastAnalysisAssembliesList, assemblyMetricsDefinionsList);
            FillCodeAsembliestDataGridView(assemblyMetricsDataTable);
            FillTestAssembliesDataGridView(assemblyMetricsDataTable);
        }

        private void FillNDependProjectInfo()
        {
            this.tboxProjectName.Text = nDependProject.Properties.Name;
            this.tBoxProjectPath.Text = nDependProject.Properties.FilePath.ToString();
        }

        private void FillCodeAsembliestDataGridView(DataTable assemblyMetricsDataTable)
        {
            string rowSelectionFiler = "([Code Element] NOT LIKE '*UnitTest*') AND ([Code Element] NOT LIKE '*SpecFlowBDD*')";
            DataTable onlyCodeAsemblies = assemblyMetricsDataTable.CloneSelection(rowSelectionFiler);
            FillCodeElementsDataGridView(this.dgvCodeAssemblies, onlyCodeAsemblies);
        }

        private void FillTestAssembliesDataGridView(DataTable assemblyMetricsDataTable)
        {
            string rowSelectionFiler = "([Code Element] LIKE '*UnitTest*')";
            DataTable onlyTestAssemblies = assemblyMetricsDataTable.CloneSelection(rowSelectionFiler);
            FillCodeElementsDataGridView(this.dgvUnitTestsAssemblies, onlyTestAssemblies);
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

        private void FillCodeMetricsListView(DataGridViewRow selectedDataGridViewRow, List<NDependMetricDefinition> metricDefinitionsList)
        {
            FillMetricsListView(this.lvwCodeMetricsList, selectedDataGridViewRow, metricDefinitionsList);
        }

        private void FillUnitTestsMetricsListView(DataGridViewRow selectedDataGridViewRow, List<NDependMetricDefinition> metricDefinitionsList)
        {
            FillMetricsListView(this.lvwUnitTestsMetricsList, selectedDataGridViewRow, metricDefinitionsList);
        }

        private void FillMetricsListView(ListView targetListview, DataGridViewRow selectedDataGridViewRow, List<NDependMetricDefinition> metricDefinitionsList)
        {
            string formatSpecifier = "0.####";
            DataGridView sourceDataGridView = selectedDataGridViewRow.DataGridView;
            targetListview.Tag = sourceDataGridView;
            targetListview.Items.Clear();
            foreach (NDependMetricDefinition metricDefinition in metricDefinitionsList)
            {
                double cellValue = Double.Parse(selectedDataGridViewRow.Cells[metricDefinition.PropertyName].Value.ToString());
                string formatedCellValue = cellValue.ToString(formatSpecifier);
                ListViewItem lvi = new ListViewItem(new[] { metricDefinition.PropertyName, formatedCellValue });
                lvi.Tag = metricDefinition;
                lvi.Checked = selectedDataGridViewRow.Cells[metricDefinition.PropertyName].Displayed;
                targetListview.Items.Add(lvi);
            }
        }

        private void FillCodeMetricDescriptionRTFBox(NDependMetricDefinition nDependMetricDefinition)
        {
            FillMetricDescriptionRTFBox(rtfCodeMetricProperties, nDependMetricDefinition);
        }

        private void FillMetricDescriptionRTFBox(RichTextBox targetRichBox, NDependMetricDefinition nDependMetricDefinition)
        {
            targetRichBox.Clear();
            targetRichBox.AppendText(nDependMetricDefinition.MetricName);
            targetRichBox.Find(nDependMetricDefinition.MetricName);
            targetRichBox.SelectionFont = new Font(rtfCodeMetricProperties.Font, rtfCodeMetricProperties.Font.Style ^ FontStyle.Bold);
            targetRichBox.SelectionStart = this.rtfCodeMetricProperties.Text.Length;
            targetRichBox.SelectionLength = 0;
            targetRichBox.SelectionFont = rtfCodeMetricProperties.Font;
            targetRichBox.AppendText(Environment.NewLine + nDependMetricDefinition.Description);
        }

        private void UptadeNamespacesList()
        {
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

        private void lvwCodeMetricsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lvwCodeMetricsList.SelectedItems.Count > 0)
            {
                ListViewItem lvi = this.lvwCodeMetricsList.SelectedItems[0];
                NDependMetricDefinition nDependMetricDefinition = (NDependMetricDefinition)lvi.Tag;
                IList metricValues = new AnalysisHistoryManager(nDependProject).GetMetricHistory(this.lblCodeElementName.Text, nDependMetricDefinition);
                string chartTitle = this.lblCodeElementType.Text.ToUpper() + ": " + this.lblCodeElementName.Text;
                ShowMetricChart(chartTitle, nDependMetricDefinition.MetricName, metricValues);
            }
        }

        private void lvwCodeMetricsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (inhibitAutocheckOnDoubleClick)
            {
                e.NewValue = e.CurrentValue;
            }
            else
            {
                DataGridView sourceDataGridView = (DataGridView)lvwCodeMetricsList.Tag;
                sourceDataGridView.Columns[this.lvwCodeMetricsList.Items[e.Index].Text].Visible = (e.NewValue == CheckState.Checked);
            }
        }

        private void lvwCodeMetricsList_MouseDown(object sender, MouseEventArgs e)
        {
            inhibitAutocheckOnDoubleClick = true;
        }

        private void lvwCodeMetricsList_MouseUp(object sender, MouseEventArgs e)
        {
            inhibitAutocheckOnDoubleClick = false;
        }

        private void dgvCodeAssemblies_SelectionChanged(object sender, EventArgs e)
        {
            AssembliesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvCodeNamespaces, lvwCodeMetricsList, lblCodeElementType, lblCodeElementName);
        }

        private void dgvUnitTestsAssemblies_SelectionChanged(object sender, EventArgs e)
        {
            AssembliesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvUnitTestsNamespaces, lvwUnitTestsMetricsList, lblUnitTestsCodeElementType, lblUnitTestsCodeElementName);
        }

        private void dgvCodeNamespaces_SelectionChanged(object sender, EventArgs e)
        {
            NamespacesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvCodeTypes, lvwCodeMetricsList, lblCodeElementType, lblCodeElementName);
        }

        private void dgvUnitTestsNamespaces_SelectionChanged(object sender, EventArgs e)
        {
            NamespacesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvUnitTestsTypes, lvwUnitTestsMetricsList, lblUnitTestsCodeElementType, lblUnitTestsCodeElementName);
        }

        private void dgvCodeTypes_SelectionChanged(object sender, EventArgs e)
        {
            TypesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvCodeMethods, lvwCodeMetricsList, lblCodeElementType, lblCodeElementName);
        }

        private void dgvUnitTestsTypes_SelectionChanged(object sender, EventArgs e)
        {
            TypesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvUnitTestsMethods, lvwUnitTestsMetricsList, lblUnitTestsCodeElementType, lblUnitTestsCodeElementName);
        }

        private void dgvCodeMethods_SelectionChanged(object sender, EventArgs e)
        {
            MethodsDataGridViewSelectionChangedEventManager((DataGridView)sender, lvwCodeMetricsList, lblCodeElementType, lblCodeElementName);
        }

        private void dgvUnitTestsMethods_SelectionChanged(object sender, EventArgs e)
        {
            MethodsDataGridViewSelectionChangedEventManager((DataGridView)sender, lvwUnitTestsMetricsList, lblUnitTestsCodeElementType, lblUnitTestsCodeElementName);
        }

        private void AssembliesDataGridViewSelectionChangedEventManager(DataGridView senderDataGridView, DataGridView targetNamespacesDataGridView, ListView targetMetricsListView, Label targetElementTypeLabel, Label targetElementNameLabel)
        {
            if (senderDataGridView.SelectedRows.Count > 0)
            {
                string selectedAssemblyName = senderDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                IAssembly assembly = codeElementsManager.GetAssemblyByName(selectedAssemblyName);

                List<NDependMetricDefinition> assemblyMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("AssemblyMetrics.xml");
                FillMetricsListView(targetMetricsListView, senderDataGridView.SelectedRows[0], assemblyMetricsDefinionsList);

                targetElementTypeLabel.Text = "Assembly";
                targetElementNameLabel.Text = selectedAssemblyName;

                List<NDependMetricDefinition> namespaceMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("NamespaceMetrics.xml");
                DataTable namespaceMetricsDataTable = dataTableHelper.CreateCodeElemetMetricsDataTable<INamespace>(assembly.ChildNamespaces, namespaceMetricsDefinionsList);
                FillCodeElementsDataGridView(targetNamespacesDataGridView, namespaceMetricsDataTable);
            }
        }

        private void NamespacesDataGridViewSelectionChangedEventManager(DataGridView senderDataGridView, DataGridView targetTypesDataGridView, ListView targetMetricsListView, Label targetElementTypeLabel, Label targetElementNameLabel)
        {
            if (senderDataGridView.SelectedRows.Count > 0)
            {
                string selectedNamespaceName = senderDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                INamespace nNamespace = codeElementsManager.GetNamespaceByName(selectedNamespaceName);

                List<NDependMetricDefinition> namespaceMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("NamespaceMetrics.xml");
                FillMetricsListView(targetMetricsListView, senderDataGridView.SelectedRows[0], namespaceMetricsDefinionsList);

                targetElementTypeLabel.Text = "Namespace";
                targetElementNameLabel.Text = selectedNamespaceName;

                List<NDependMetricDefinition> typeMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("TypeMetrics.xml");
                DataTable typeMetricsDataTable = dataTableHelper.CreateCodeElemetMetricsDataTable<IType>(nNamespace.ChildTypes, typeMetricsDefinionsList);
                FillCodeElementsDataGridView(targetTypesDataGridView, typeMetricsDataTable);
            }
        }

        private void TypesDataGridViewSelectionChangedEventManager(DataGridView senderDataGridView, DataGridView targetMethodsDataGridView, ListView targetMetricsListView, Label targetElementTypeLabel, Label targetElementNameLabel)
        {
            if (senderDataGridView.SelectedRows.Count > 0)
            {
                string selectedType = senderDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                IType nType = codeElementsManager.GetTypeByName(selectedType);

                List<NDependMetricDefinition> typesMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("TypeMetrics.xml");
                FillMetricsListView(targetMetricsListView, senderDataGridView.SelectedRows[0], typesMetricsDefinionsList);

                targetElementTypeLabel.Text = "Type";
                targetElementNameLabel.Text = selectedType;

                List<NDependMetricDefinition> methodMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("MethodMetrics.xml");
                DataTable methodMetricsDataTable = dataTableHelper.CreateCodeElemetMetricsDataTable<IMethod>(nType.MethodsAndContructors, methodMetricsDefinionsList);
                FillCodeElementsDataGridView(targetMethodsDataGridView, methodMetricsDataTable);
            }
        }



        private void MethodsDataGridViewSelectionChangedEventManager(DataGridView senderDataGridView, ListView targetMetricsListView, Label targetElementTypeLabel, Label targetElementNameLabel)
        {
            if (senderDataGridView.SelectedRows.Count > 0)
            {
                string selectedMethod = senderDataGridView.SelectedRows[0].Cells[0].Value.ToString();

                List<NDependMetricDefinition> methodsMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("MethodMetrics.xml");
                FillMetricsListView(targetMetricsListView, senderDataGridView.SelectedRows[0], methodsMetricsDefinionsList);

                targetElementTypeLabel.Text = "Method";
                targetElementNameLabel.Text = selectedMethod;
            }
        }

        private void lvwCodeMetricsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwCodeMetricsList.SelectedItems.Count > 0)
            {
                ListViewItem lvi = this.lvwCodeMetricsList.SelectedItems[0];
                NDependMetricDefinition nDependMetricDefinition = (NDependMetricDefinition)lvi.Tag;
                FillCodeMetricDescriptionRTFBox(nDependMetricDefinition);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenNdependProject();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }








    }
}
