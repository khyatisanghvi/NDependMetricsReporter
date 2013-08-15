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
        NDependServicesProvider nDependServicesProvider;
        IProject nDependProject;
        ICodeBase lastAnalysisCodebase;

        public MetricsViewer()
        {
            InitializeComponent();

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
            this.lvwNamespacesList.Items.Clear(); ;
            foreach (INamespace nspc in namespacesList)
            {
                ListViewItem lvi = new ListViewItem(new string[] { nspc.Name });
                this.lvwNamespacesList.Items.Add(lvi);
            }
        }

        private void FillTypesListView(IEnumerable<IType> typesList)
        {
            this.lvwTypesList.Items.Clear(); ;
            foreach (IType tp in typesList)
            {
                ListViewItem lvi = new ListViewItem(new string[] { tp.Name });
                this.lvwTypesList.Items.Add(lvi);
            }
        }

        private void FillMethodsListView(IEnumerable<IMethod> methodsList)
        {
            this.lvwMethodsList.Items.Clear(); ;
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

        private void FillMetricsListView2<T>(T codeElement, Dictionary<string, string> metrics)
        {
            this.lvwMetricsList2.Items.Clear();
            foreach (KeyValuePair<string, string> metric in metrics)
            {
                ListViewItem lvi = new ListViewItem(new[] { metric.Key, metric.Value });
                this.lvwMetricsList2.Items.Add(lvi);
            }
        }

        private void ShowMetricChart(string serieName, IList chartData)
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
            metricTrendChart.RefreshData(serieName, chartData);
        }

        private void btnOpenProject_Click(object sender, EventArgs e)
        {
            nDependServicesProvider.ProjectManager.ShowDialogChooseAnExistingProject(out nDependProject);
            this.lastAnalysisCodebase = new NDependCodeBaseManager(nDependProject).LoadLastCodebase();
            FillBaseControls();
        }

        private void lvwAssembliesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwAssembliesList.SelectedItems.Count > 0)
            {
                NDependCodeElementsManager codeElementsManager = new NDependCodeElementsManager(lastAnalysisCodebase);
                string selectedAssemblyName = this.lvwAssembliesList.SelectedItems[0].Text;
                IAssembly assembly = codeElementsManager.GetAssemblyByName(selectedAssemblyName);
                FillNamespacesListView(assembly.ChildNamespaces);
                Dictionary<NDependMetricDefinition, double> assemblyMetrics = codeElementsManager.GetAssemblyMetrics(assembly);
                FillMetricsListView<IAssembly>(assembly, assemblyMetrics);
                Dictionary<string, string> assemblyMetrics2 = codeElementsManager.GetAssemblyMetrics_NoReflection(assembly);
                FillMetricsListView2<IAssembly>(assembly, assemblyMetrics2);
            }
        }

        private void lvwNamespacesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwNamespacesList.SelectedItems.Count > 0)
            {
                string selectedNamespaceName = this.lvwNamespacesList.SelectedItems[0].Text;
                var typesList = new NDependCodeElementsManager(lastAnalysisCodebase).GetNamespaceByName(selectedNamespaceName).ChildTypes;
                FillTypesListView(typesList);
            }
        }

        private void lvwTypesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwTypesList.SelectedItems.Count > 0)
            {
                string selectedTypeName = this.lvwTypesList.SelectedItems[0].Text;
                var methodsList = new NDependCodeElementsManager(lastAnalysisCodebase).GetTypeByName(selectedTypeName).MethodsAndContructors;
                FillMethodsListView(methodsList);
            }
        }

        private void lvwMetricsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwMetricsList.SelectedItems.Count > 0)
            {
                ListViewItem lvi = this.lvwMetricsList.SelectedItems[0];
                IList metricValues = new NDependAnalysisHistoryManager(nDependProject).GetMetricHistory(lvwMetricsList.Tag, lvi.Tag);
                ShowMetricChart(((NDependMetricDefinition)lvi.Tag).MetricName, metricValues);
            }
        }
    }
}
