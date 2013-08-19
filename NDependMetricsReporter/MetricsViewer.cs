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

namespace NDependMetricsReporter
{
    public partial class MetricsViewer : Form
    {
        ListViewColumnSorter lvwColumnSorter;

        NDependServicesProvider nDependServicesProvider;
        IProject nDependProject;
        ICodeBase lastAnalysisCodebase;

        public MetricsViewer()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            this.lvwNamespacesList.ListViewItemSorter = lvwColumnSorter;
            this.lvwTypesList.ListViewItemSorter = lvwColumnSorter;

            nDependServicesProvider = new NDependServicesProvider();           
        }

        private void FillBaseControls()
        {
            this.tboxProjectName.Text = nDependProject.Properties.Name;
            IEnumerable<IAssembly> lastAnalysisAssembliesList = lastAnalysisCodebase.Assemblies;
            FillAssembliesListView(lastAnalysisAssembliesList);
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
            double d = -21;
            this.lvwNamespacesList.Items.Clear();
            foreach (INamespace nspc in namespacesList)
            {
                d = d / (-4);
                ListViewItem lvi = new ListViewItem(new string[] { nspc.Name, d.ToString() });
                this.lvwNamespacesList.Items.Add(lvi);
            }
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

        private void FillMethodsListView(IEnumerable<IMethod> methodsList)
        {
            this.lvwMethodsList.Items.Clear();
            foreach (IMethod m in methodsList)
            {
                ListViewItem lvi = new ListViewItem(new string[] { m.Name });
                this.lvwMethodsList.Items.Add(lvi);
            }
        }

        private void FillMetricsListView<T>(T codeElement, Dictionary<NDependMetricDefinition, double> metrics)
        {
            this.lvwMetricsList.Tag = codeElement;
            this.lvwMetricsList.Items.Clear();
            foreach (KeyValuePair<NDependMetricDefinition, double> metric in metrics)
            {
                string formatSpecifier = "0.####";
                ListViewItem lvi = new ListViewItem(new[] { metric.Key.PropertyName, Math.Round(metric.Value, 4, MidpointRounding.AwayFromZero).ToString(formatSpecifier) });
                lvi.Tag = metric.Key;
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

        private void btnOpenProject_Click(object sender, EventArgs e)
        {
            nDependServicesProvider.ProjectManager.ShowDialogChooseAnExistingProject(out nDependProject);
            if (nDependProject == null) return;
            this.lastAnalysisCodebase = new CodeBaseManager(nDependProject).LoadLastCodebase();
            FillBaseControls();
        }

        private void lvwAssembliesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwAssembliesList.SelectedItems.Count > 0)
            {
                CodeElementsManager codeElementsManager = new CodeElementsManager(lastAnalysisCodebase);
                string selectedAssemblyName = this.lvwAssembliesList.SelectedItems[0].Text;
                IAssembly assembly = codeElementsManager.GetAssemblyByName(selectedAssemblyName);
                FillNamespacesListView(assembly.ChildNamespaces);
                Dictionary<NDependMetricDefinition, double> assemblyMetrics = codeElementsManager.GetCodeElementMetrics<IAssembly>(assembly,"AssemblyMetrics.xml");
                FillMetricsListView<IAssembly>(assembly, assemblyMetrics);
                this.lblCodeElementType.Text = "Assembly";
                this.lblCodeElementName.Text = selectedAssemblyName;
            }
        }

        private void lvwNamespacesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwNamespacesList.SelectedItems.Count > 0)
            {
                CodeElementsManager codeElementsManager = new CodeElementsManager(lastAnalysisCodebase);
                string selectedNamespaceName = this.lvwNamespacesList.SelectedItems[0].Text;
                INamespace nNamespace = codeElementsManager.GetNamespaceByName(selectedNamespaceName);
                FillTypesListView(nNamespace.ChildTypes);
                Dictionary<NDependMetricDefinition, double> namespaceMetrics = codeElementsManager.GetCodeElementMetrics<INamespace>(nNamespace, "NamespaceMetrics.xml");
                FillMetricsListView<INamespace>(nNamespace, namespaceMetrics);
                this.lblCodeElementType.Text = "Namespace";
                this.lblCodeElementName.Text = selectedNamespaceName;
            }
        }

        private void lvwTypesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwTypesList.SelectedItems.Count > 0)
            {
                CodeElementsManager codeElementsManager = new CodeElementsManager(lastAnalysisCodebase);
                string selectedTypeName = this.lvwTypesList.SelectedItems[0].Text;
                IType nType = codeElementsManager.GetTypeByName(selectedTypeName);
                FillMethodsListView(nType.MethodsAndContructors);
                Dictionary<NDependMetricDefinition, double> typeMetrics = codeElementsManager.GetCodeElementMetrics<IType>(nType, "TypeMetrics.xml");
                FillMetricsListView<IType>(nType, typeMetrics);
                this.lblCodeElementType.Text = "Type";
                this.lblCodeElementName.Text = selectedTypeName;
            }
        }

        private void lvwMethodsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwMethodsList.SelectedItems.Count > 0)
            {
                CodeElementsManager codeElementsManager = new CodeElementsManager(lastAnalysisCodebase);
                string selectedMethodName = this.lvwMethodsList.SelectedItems[0].Text;
                IMethod nMethod = codeElementsManager.GetMethodByName(selectedMethodName);
                Dictionary<NDependMetricDefinition, double> methodMetrics = codeElementsManager.GetCodeElementMetrics<IMethod>(nMethod, "MethodMetrics.xml");
                FillMetricsListView<IMethod>(nMethod, methodMetrics);
                this.lblCodeElementType.Text = "Method";
                this.lblCodeElementName.Text = selectedMethodName;
            }
        }

        private void lvwMetricsList_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}
