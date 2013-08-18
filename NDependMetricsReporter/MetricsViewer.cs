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
                CodeElementsManager codeElementsManager = new CodeElementsManager(lastAnalysisCodebase);
                string selectedNamespaceName = this.lvwNamespacesList.SelectedItems[0].Text;
                INamespace nNamespace = codeElementsManager.GetNamespaceByName(selectedNamespaceName);
                FillTypesListView(nNamespace.ChildTypes);
                Dictionary<NDependMetricDefinition, double> namespaceMetrics = codeElementsManager.GetNamespaceMetrics(nNamespace);
                FillMetricsListView<INamespace>(nNamespace, namespaceMetrics);
                Dictionary<string, string> namespaceMetrics2 = codeElementsManager.GetNamespaceMetrics_NoReflection(nNamespace);
                FillMetricsListView2<INamespace>(nNamespace, namespaceMetrics2);
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
                Dictionary<NDependMetricDefinition, double> typeMetrics = codeElementsManager.GetTypeMetrics(nType);
                FillMetricsListView<IType>(nType, typeMetrics);
                Dictionary<string, string> typeMetrics2 = codeElementsManager.GetTypeMetrics_NoReflection(nType);
                FillMetricsListView2<IType>(nType, typeMetrics2);
            }
        }

        private void lvwMethodsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwMethodsList.SelectedItems.Count > 0)
            {
                CodeElementsManager codeElementsManager = new CodeElementsManager(lastAnalysisCodebase);
                string selectedMethodName = this.lvwMethodsList.SelectedItems[0].Text;
                IMethod nMethod = codeElementsManager.GetMethodByName(selectedMethodName);
                Dictionary<NDependMetricDefinition, double> methodMetrics = codeElementsManager.GetMethodMetrics(nMethod);
                FillMetricsListView<IMethod>(nMethod, methodMetrics);
                Dictionary<string, string> typeMetrics2 = codeElementsManager.GetMethodMetrics_NoReflection(nMethod);
                FillMetricsListView2<IMethod>(nMethod, typeMetrics2);
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
                ShowMetricChart(nDependMetricDefinition.MetricName, metricValues);
            }
        }


    }
}
