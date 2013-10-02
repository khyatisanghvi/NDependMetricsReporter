﻿using System;
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

        private void OpenNdependProject()
        {
            nDependServicesProvider.ProjectManager.ShowDialogChooseAnExistingProject(out nDependProject);
            if (nDependProject == null) return;
            ICodeBase lastAnalysisCodebase = new CodeBaseManager(nDependProject).LoadLastCodebase();
            codeElementsManager = new CodeElementsManager(lastAnalysisCodebase);
            FillBaseControls();
        }

        private void FillBaseControls()
        {
            FillNDependProjectInfo();
            List<NDependMetricDefinition> assemblyMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("AssemblyMetrics.xml");
            IEnumerable<IAssembly> lastAnalysisAssembliesList = codeElementsManager.GetNonThirdPartyAssembliesInApplication();
            DataTable assemblyMetricsDataTable = CreateCodeElemetMetricsDataTable<IAssembly>(lastAnalysisAssembliesList, assemblyMetricsDefinionsList);
            FillCodeAsembliestDataGridView(assemblyMetricsDataTable);
            //FillCodeElementsDataGridView(this.dgvAssemblies, assemblyMetricsDataTable);
        }

        private void FillNDependProjectInfo()
        {
            this.tboxProjectName.Text = nDependProject.Properties.Name;
            this.tBoxProjectPath.Text = nDependProject.Properties.FilePath.ToString();
        }

        private void FillCodeAsembliestDataGridView(DataTable assemblyMetricsDataTable)
        {
            DataRow[] selectedDataRows = assemblyMetricsDataTable.Select("([Code Element] NOT LIKE '*UnitTest*') AND ([Code Element] NOT LIKE '*SpecFlowBDD*')");
            DataTable onlyCodeAsemblies = assemblyMetricsDataTable.Clone();
            foreach (DataRow d in selectedDataRows)
            {
                onlyCodeAsemblies.ImportRow(d);
            }
            FillCodeElementsDataGridView(this.dgvCodeAssemblies, onlyCodeAsemblies);
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
            this.lvwCodeMetricsList.Tag = sourceDataGridView;
            this.lvwCodeMetricsList.Items.Clear();
            foreach (NDependMetricDefinition metricDefinition in metricDefinitionsList)
            {
                double cellValue = Double.Parse(selectedDataGridViewRow.Cells[metricDefinition.PropertyName].Value.ToString());
                string formatedCellValue = cellValue.ToString(formatSpecifier);
                ListViewItem lvi = new ListViewItem(new[] { metricDefinition.PropertyName, formatedCellValue });
                lvi.Tag = metricDefinition;
                lvi.Checked = selectedDataGridViewRow.Cells[metricDefinition.PropertyName].Displayed;
                this.lvwCodeMetricsList.Items.Add(lvi);
            }
        }

        private void FillMetricDescriptionRTFBox(NDependMetricDefinition nDependMetricDefinition)
        {
            this.rtfCodeMetricProperties.Clear();
            this.rtfCodeMetricProperties.AppendText(nDependMetricDefinition.MetricName);
            this.rtfCodeMetricProperties.Find(nDependMetricDefinition.MetricName);
            this.rtfCodeMetricProperties.SelectionFont = new Font(rtfCodeMetricProperties.Font, rtfCodeMetricProperties.Font.Style ^ FontStyle.Bold);
            this.rtfCodeMetricProperties.SelectionStart = this.rtfCodeMetricProperties.Text.Length;
            this.rtfCodeMetricProperties.SelectionLength = 0;
            this.rtfCodeMetricProperties.SelectionFont = rtfCodeMetricProperties.Font;
            this.rtfCodeMetricProperties.AppendText(Environment.NewLine + nDependMetricDefinition.Description);
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

/*        private void btnOpenProject_Click(object sender, EventArgs e)
        {
            OpenNdependProject();
        }*/

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
                FillCodeElementsDataGridView(this.dgvCodeNamespaces, namespaceMetricsDataTable);
            }

        }

        private void dgvCodeNamespaces_SelectionChanged(object sender, EventArgs e)
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
                FillCodeElementsDataGridView(this.dgvCodeTypes, typeMetricsDataTable); 
            }
        }

        private void dgvCodeTypes_SelectionChanged(object sender, EventArgs e)
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
                FillCodeElementsDataGridView(this.dgvCodeMethods, methodMetricsDataTable);
            }
        }

        private void dgvCodeMethods_SelectionChanged(object sender, EventArgs e)
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

        private void lvwCodeMetricsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwCodeMetricsList.SelectedItems.Count > 0)
            {
                ListViewItem lvi = this.lvwCodeMetricsList.SelectedItems[0];
                NDependMetricDefinition nDependMetricDefinition = (NDependMetricDefinition)lvi.Tag;
                FillMetricDescriptionRTFBox(nDependMetricDefinition);
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
