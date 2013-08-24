using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NDepend;
using NDepend.Path;
using NDepend.Analysis;
using NDepend.Project;
using NDepend.CodeModel;
using NDepend.PowerTools;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using ExtensionMethods;

namespace NDependMetricsReporter
{
    public partial class MetricsViewer : Form
    {
        List<string> checkedAssemblyMetrics;
        List<string> checkedNamespaceMetrics;
        List<string> checkedTypeMetrics;
        List<string> checkedMethodMetrics;
        ListViewColumnSorter lvwColumnSorter;
        bool inhibitAutocheckOnDoubleClick;

        NDependServicesProvider nDependServicesProvider;
        CodeElementsManager codeElementsManager;
        IProject nDependProject;

        public MetricsViewer()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            this.lvwNamespacesList.ListViewItemSorter = lvwColumnSorter;
            this.lvwTypesList.ListViewItemSorter = lvwColumnSorter;
            checkedAssemblyMetrics = new List<string>();
            checkedNamespaceMetrics = new List<string>();
            checkedTypeMetrics = new List<string>();
            checkedMethodMetrics = new List<string>();

            nDependServicesProvider = new NDependServicesProvider();           
        }

        private void FillBaseControls()
        {
            this.tboxProjectName.Text = nDependProject.Properties.Name;
            IEnumerable<IAssembly> lastAnalysisAssembliesList = codeElementsManager.CodeBase.Assemblies;
            FillAssembliesListView(lastAnalysisAssembliesList);
        }

        private void FillMetricDataGridView(DataGridView codeElementMetricsDataGridView, DataTable codeElementMetricsDataTable)
        {
            codeElementMetricsDataGridView.DataSource = codeElementMetricsDataTable;
            codeElementMetricsDataGridView.RowHeadersWidth = 25;
            foreach (DataGridViewColumn dvgc in codeElementMetricsDataGridView.Columns)
            {
                dvgc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dvgc.Visible = false;
            }
            codeElementMetricsDataGridView.Columns[0].Visible = true;
            codeElementMetricsDataGridView.Columns[0].Frozen = true;
        }

        private void FillAssembliesListView(IEnumerable<IAssembly> assembliesList)
        {
            lvwAssembliesList.Items.Clear();
            foreach (IAssembly assembly in assembliesList)
            {
                if (assembly.IsThirdParty) continue;
                ListViewItem lvi = new ListViewItem(new string[] { assembly.Name, assembly.FilePath.ToString() });
                this.lvwAssembliesList.Items.Add(lvi);
            }
        }

        private void FillNamespacesListView(IEnumerable<INamespace> namespacesList)
        {
            this.lvwNamespacesList.Items.Clear();
            foreach (INamespace nspc in namespacesList)
            {
                ListViewItem lvi = new ListViewItem(new string[] { nspc.Name });
                this.lvwNamespacesList.Items.Add(lvi);
            }
        }

        private void FillNamespacesGridView(DataTable namespacesMetricsDataTable, List<string> rowHeaders)
        {
            this.dgvNamespaces.DataSource = namespacesMetricsDataTable;
            dgvNamespaces.RowHeadersWidth = 25;
            foreach (DataGridViewColumn dvgc in dgvNamespaces.Columns)
            {
                dvgc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dvgc.Visible = false;
            }
            dgvNamespaces.Columns[0].Visible = true;
            dgvNamespaces.Columns[0].Frozen = true;
        }

        private void FillTypesListView(IEnumerable<IType> typesList)
        {
            this.lvwTypesList.Items.Clear();
            foreach (IType tp in typesList)
            {
                ListViewItem lvi = new ListViewItem(new string[] { tp.Name });
                this.lvwTypesList.Items.Add(lvi);
            }
        }

        private void FillTypesGridView(DataTable typesMetricTable, List<string> rowHeaders)
        {
            this.dgvTypes.DataSource = typesMetricTable;
            dgvTypes.RowHeadersWidth = 25;
            foreach (DataGridViewColumn dvgc in dgvTypes.Columns)
            {
                dvgc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dvgc.Visible = false;
            }
            dgvTypes.Columns[0].Visible = true;
            dgvTypes.Columns[0].Frozen = true;
        }

        private void FillMethodsListView(IEnumerable<IMethod> methodsList)
        {
            this.lvwMethodsList.Items.Clear();
            foreach (IMethod m in methodsList)
            {
                ListViewItem lvi = new ListViewItem(new string[] { m.Name });
                this.lvwMethodsList.Items.Add(lvi);
            }
        }

        private void FillMetricsListView<T>(T codeElement, Dictionary<NDependMetricDefinition, double> metrics, List<string> visibleDataGridColumns)
        {
            string formatSpecifier = "0.####";
            this.lvwMetricsList.Tag = codeElement;
            this.lvwMetricsList.Items.Clear();
            foreach (KeyValuePair<NDependMetricDefinition, double> metric in metrics)
            {
                ListViewItem lvi = new ListViewItem(new[] { metric.Key.PropertyName, Math.Round(metric.Value, 4, MidpointRounding.AwayFromZero).ToString(formatSpecifier) });
                lvi.Tag = metric.Key;
                if (visibleDataGridColumns.Contains(metric.Key.PropertyName)) lvi.Checked=true;
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


        private void AddMetricColumn(NDependMetricDefinition metricDefinition)
        {
            string formatSpecifier = "0.####";
            switch (metricDefinition.NDependCodeElementType)
            {
                case "NDepend.CodeModel.IAssembly":
                    break;
                case "NDepend.CodeModel.INamespace":
                    List<string> namespaceNames = new List<string>();
                    ColumnHeader columnHeader = new ColumnHeader();
                    columnHeader.Text = metricDefinition.PropertyName;
                    columnHeader.Name = metricDefinition.PropertyName;
                    lvwNamespacesList.Columns.Add(columnHeader);
                    foreach (ListViewItem lvi in lvwNamespacesList.Items)
                    {
                        double metricValue= codeElementsManager.GetCodeElementMetricValue<INamespace>(codeElementsManager.GetNamespaceByName(lvi.Text), metricDefinition);
                        lvi.SubItems.Add(metricValue.ToString(formatSpecifier));
                    }
                    break;
                case "NDepend.CodeModel.IType":
                    break;
                case "NDepend.CodeModel.IMethod":
                    break;
            }
        }

        private void RemoveMetricColumn(NDependMetricDefinition metricDefinition)
        {
            switch (metricDefinition.NDependCodeElementType)
            {
                case "NDepend.CodeModel.IAssembly":
                    break;
                case "NDepend.CodeModel.INamespace":
                    int columnIndex= lvwNamespacesList.Columns.IndexOfKey(metricDefinition.PropertyName);
                    lvwNamespacesList.Columns.RemoveAt(columnIndex);
                    foreach (ListViewItem lvi in lvwNamespacesList.Items)
                    {
                        lvi.SubItems.RemoveAt(columnIndex);
                    }
                    break;
                case "NDepend.CodeModel.IType":
                    break;
                case "NDepend.CodeModel.IMethod":
                    break;
            }
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
                //string codeElementName = (string)typeof(ICodeElement).GetProperty("Name").GetValue(codeElement);
                row[0] = codeElementName;
                foreach (NDependMetricDefinition metricDefinition in metricsDefinitionList)
                {
                    row[metricDefinition.PropertyName] = codeElementsManager.GetCodeElementMetricValue<CodeElementType>((CodeElementType)codeElement, metricDefinition);
                }
                metricsTable.Rows.Add(row);
            }
        }

        private DataTable CreateNamespacesDataTable (IAssembly assembly)
        {
            List<NDependMetricDefinition> nDependNamespaceMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("NamespaceMetrics.xml");
            List<string> columnHeaders = new List<string>();
            columnHeaders.Add("NamespaceName");
            foreach (NDependMetricDefinition nDmd in nDependNamespaceMetricsDefinionsList)
            {
                columnHeaders.Add(nDmd.PropertyName);
            }
            DataTable table = new DataTable();
            DataColumn namespaceNameColumn = new DataColumn("Namespace");
            namespaceNameColumn.DataType= typeof(string);
            table.Columns.Add(namespaceNameColumn);
            foreach (NDependMetricDefinition nDmd in nDependNamespaceMetricsDefinionsList)
            {
                DataColumn metricColumn = new DataColumn(nDmd.PropertyName);
                metricColumn.DataType = typeof(double); //Type.GetType(nDmd.NDependMetricType);
                table.Columns.Add(metricColumn);
            }
            foreach (INamespace nNamespce in assembly.ChildNamespaces)
            {
                DataRow row = table.NewRow();
                row[0] = nNamespce.Name;
                foreach (NDependMetricDefinition nDmd in nDependNamespaceMetricsDefinionsList)
                {
                    row[nDmd.PropertyName] = codeElementsManager.GetCodeElementMetricValue<INamespace>(nNamespce, nDmd);
                }
                table.Rows.Add(row);
            }
            return table;
        }

        private List<string> GetVisbleMetricColumnNames(DataGridViewColumnCollection columnCollection)
        {
            return (columnCollection.Cast<DataGridViewColumn>())
                .Where(column => column.Index != 0 && column.Visible)
                .Select(column => column.Name).ToList();
        }

        private void btnOpenProject_Click(object sender, EventArgs e)
        {
            nDependServicesProvider.ProjectManager.ShowDialogChooseAnExistingProject(out nDependProject);
            if (nDependProject == null) return;
            ICodeBase lastAnalysisCodebase = new CodeBaseManager(nDependProject).LoadLastCodebase();
            codeElementsManager = new CodeElementsManager(lastAnalysisCodebase);
            FillBaseControls();
        }

        private void lvwAssembliesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwAssembliesList.SelectedItems.Count > 0)
            {
                string selectedAssemblyName = this.lvwAssembliesList.SelectedItems[0].Text;
                IAssembly assembly = codeElementsManager.GetAssemblyByName(selectedAssemblyName);

                //FillNamespacesListView(assembly.ChildNamespaces);

                Dictionary<NDependMetricDefinition, double> assemblyMetrics = codeElementsManager.GetCodeElementMetrics<IAssembly>(assembly,"AssemblyMetrics.xml");
                FillMetricsListView<IAssembly>(assembly, assemblyMetrics, new List<string>());
                
                this.lblCodeElementType.Text = "Assembly";
                this.lblCodeElementName.Text = selectedAssemblyName;

                //DataTable namespacesMetricsDataTable = CreateNamespacesDataTable(assembly);
                List<NDependMetricDefinition> namespaceMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("NamespaceMetrics.xml");
                //List<string> namespacesNamesList = assembly.ChildNamespaces.Select(a => a.Name).ToList();
                DataTable namespacesMetricsDataTable = CreateCodeElemetMetricsDataTable<INamespace>(assembly.ChildNamespaces, namespaceMetricsDefinionsList);
                //FillNamespacesGridView(namespacesMetricsDataTable, namespacesNamesList);
                FillMetricDataGridView(dgvNamespaces, namespacesMetricsDataTable); 

            }
        }

        private void lvwNamespacesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwNamespacesList.SelectedItems.Count > 0)
            {
                string selectedNamespaceName = this.lvwNamespacesList.SelectedItems[0].Text;
                INamespace nNamespace = codeElementsManager.GetNamespaceByName(selectedNamespaceName);
                
                FillTypesListView(nNamespace.ChildTypes);
                
                Dictionary<NDependMetricDefinition, double> namespaceMetrics = codeElementsManager.GetCodeElementMetrics<INamespace>(nNamespace, "NamespaceMetrics.xml");
                FillMetricsListView<INamespace>(nNamespace, namespaceMetrics, new List<string>());
                
                this.lblCodeElementType.Text = "Namespace";
                this.lblCodeElementName.Text = selectedNamespaceName;

                List<NDependMetricDefinition> typeMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("TypeMetrics.xml");
                List<string> typesList = nNamespace.ChildTypes.Select(a => a.Name).ToList();
                DataTable typesMetricsDataTable = CreateCodeElemetMetricsDataTable<IType>(nNamespace.ChildTypes, typeMetricsDefinionsList);
                FillNamespacesGridView(typesMetricsDataTable, typesList);
            }
        }

        private void lvwTypesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwTypesList.SelectedItems.Count > 0)
            {
                string selectedTypeName = this.lvwTypesList.SelectedItems[0].Text;
                IType nType = codeElementsManager.GetTypeByName(selectedTypeName);
                FillMethodsListView(nType.MethodsAndContructors);
                Dictionary<NDependMetricDefinition, double> typeMetrics = codeElementsManager.GetCodeElementMetrics<IType>(nType, "TypeMetrics.xml");
                FillMetricsListView<IType>(nType, typeMetrics, new List<string>());
                this.lblCodeElementType.Text = "Type";
                this.lblCodeElementName.Text = selectedTypeName;
            }
        }

        private void lvwMethodsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwMethodsList.SelectedItems.Count > 0)
            {
                string selectedMethodName = this.lvwMethodsList.SelectedItems[0].Text;
                IMethod nMethod = codeElementsManager.GetMethodByName(selectedMethodName);
                Dictionary<NDependMetricDefinition, double> methodMetrics = codeElementsManager.GetCodeElementMetrics<IMethod>(nMethod, "MethodMetrics.xml");
                FillMetricsListView<IMethod>(nMethod, methodMetrics, new List<string>());
                this.lblCodeElementType.Text = "Method";
                this.lblCodeElementName.Text = selectedMethodName;
            }
        }

        private void lvwNamespacesList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lvwNamespacesList.Sort();
        }

        private void lvwTypesList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lvwTypesList.Sort();
        }

        private void lvwMetricsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lvwMetricsList.SelectedItems.Count > 0)
            {
                ListViewItem lvi = this.lvwMetricsList.SelectedItems[0];
                NDependMetricDefinition nDependMetricDefinition = (NDependMetricDefinition)lvi.Tag;
                IList metricValues = new AnalysisHistoryManager(nDependProject).GetMetricHistory(lvwMetricsList.Tag, nDependMetricDefinition);
                FillMetricDescriptionRTFBox(nDependMetricDefinition);
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
                if (e.NewValue == CheckState.Checked)
                {

                    AddMetricColumn((NDependMetricDefinition)this.lvwMetricsList.Items[e.Index].Tag);
                }
                else
                {
                    RemoveMetricColumn((NDependMetricDefinition)this.lvwMetricsList.Items[e.Index].Tag);
                }
                this.dgvNamespaces.Columns[this.lvwMetricsList.Items[e.Index].Text].Visible = (e.NewValue==CheckState.Checked);
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

        private void dgvNamespaces_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView thisDataGridView = (DataGridView)sender;
            if (thisDataGridView.SelectedRows.Count>0)
            {
                string selectedNamespaceName = thisDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                INamespace nNamespace = codeElementsManager.GetNamespaceByName(selectedNamespaceName);

                //FillTypesListView(nNamespace.ChildTypes);

                Dictionary<NDependMetricDefinition, double> namespaceMetrics = codeElementsManager.GetCodeElementMetrics<INamespace>(nNamespace, "NamespaceMetrics.xml");
                List<string> visibleColumnNames = GetVisbleMetricColumnNames(dgvNamespaces.Columns);
                FillMetricsListView<INamespace>(nNamespace, namespaceMetrics, visibleColumnNames);

                this.lblCodeElementType.Text = "Namespace";
                this.lblCodeElementName.Text = selectedNamespaceName;

                //DataTable namespacesMetricsDataTable = CreateNamespacesDataTable(assembly);
                List<NDependMetricDefinition> typeMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("TypeMetrics.xml");
                //List<string> namespacesNamesList = assembly.ChildNamespaces.Select(a => a.Name).ToList();
                DataTable typeMetricsDataTable = CreateCodeElemetMetricsDataTable<IType>(nNamespace.ChildTypes, typeMetricsDefinionsList);
                //FillNamespacesGridView(namespacesMetricsDataTable, namespacesNamesList);
                FillMetricDataGridView(dgvTypes, typeMetricsDataTable); 
            }
        }

        private void dgvTypes_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView thisDataGridView = (DataGridView)sender;
            if (thisDataGridView.SelectedRows.Count > 0)
            {
                string selectedType = thisDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                IType nType = codeElementsManager.GetTypeByName(selectedType);

                //FillTypesListView(nNamespace.ChildTypes);

                Dictionary<NDependMetricDefinition, double> namespaceMetrics = codeElementsManager.GetCodeElementMetrics<IType>(nType, "TypeMetrics.xml");
                List<string> visibleColumnNames = GetVisbleMetricColumnNames(dgvNamespaces.Columns);
                FillMetricsListView<INamespace>(nType, namespaceMetrics, visibleColumnNames);

                this.lblCodeElementType.Text = "Type";
                this.lblCodeElementName.Text = selectedType;

                //DataTable namespacesMetricsDataTable = CreateNamespacesDataTable(assembly);
                List<NDependMetricDefinition> typeMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("TypeMetrics.xml");
                //List<string> namespacesNamesList = assembly.ChildNamespaces.Select(a => a.Name).ToList();
                DataTable typeMetricsDataTable = CreateCodeElemetMetricsDataTable<IType>(nType.ChildTypes, typeMetricsDefinionsList);
                //FillNamespacesGridView(namespacesMetricsDataTable, namespacesNamesList);
                FillMetricDataGridView(dgvTypes, typeMetricsDataTable);
            }

        }
    }
}
