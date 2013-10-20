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
using NDepend.CodeModel;
using NDepend.Project;

namespace NDependMetricsReporter
{
    public partial class MetricProperties : Form
    {
        NDependMetricDefinition nDependMetricDefinition;
        string codeElementName;
        string codeElementType;
        DataTable selectedCodeElementMatricsDataTable;
        string parentCodeElementName;
        string assemblyName;

        IProject nDependProject;
        CodeElementsManager codeElementsManager;

        DataTableHelper dataTableHelper;

        Dictionary<string, string> codeElementsTypePlurals = new Dictionary<string,string>() { { "Assembly", "Assemblies" }, { "Namespace", "Namepaces" }, { "Type", "Types" }, { "Method", "Methods" } };
        Dictionary<string, string> codeElementsTypePrecedences = new Dictionary<string, string>() { { "Assembly", "Application" }, { "Namespace", "Assembly" }, { "Type", "Namespace" }, { "Method", "Type" } };

        public MetricProperties(
            NDependMetricDefinition nDependMetricDefinition,
            string codeElementName,
            DataTable selectedCodeElementMatricsDataTable,
            string parentCodeElementName,
            string assemblyName,
            IProject nDependProject)
        {
            InitializeComponent();
            
            this.nDependMetricDefinition = nDependMetricDefinition;
            this.codeElementName = codeElementName;
            this.codeElementType = nDependMetricDefinition.NDependCodeElementType.Split('.').Last<string>().Substring(1);
            this.selectedCodeElementMatricsDataTable = selectedCodeElementMatricsDataTable;
            this.parentCodeElementName = parentCodeElementName;
            this.assemblyName = assemblyName;
            
            InitNDependProjectElements(nDependProject);
            
            FillControls();
        }

        private void InitNDependProjectElements(IProject nDependProject)
        {
            this.nDependProject = nDependProject;
            ICodeBase lastAnalysisCodebase = new CodeBaseManager(nDependProject).LoadLastCodebase();
            codeElementsManager = new CodeElementsManager(lastAnalysisCodebase);
            dataTableHelper = new DataTableHelper(codeElementsManager);
        }

        private void FillControls()
        {
            string codeElementTypePlural = codeElementsTypePlurals[codeElementType];
            string parentCodeElementType = codeElementsTypePrecedences[codeElementType];

            this.lblMetricName.Text = nDependMetricDefinition.PropertyName;
            this.lblCodeElementName.Text = codeElementName;
            this.lblCodeElementType.Text = codeElementType;
            this.lblParentCodeElementName.Text = parentCodeElementName;
            this.lblParentCodeElementType.Text = parentCodeElementType;
            this.lblAssemblyName.Text = assemblyName;

            FillMetricDescriptionRTFBox(nDependMetricDefinition);

            tabParentCondeElement.Text = "All " + codeElementTypePlural + " in parent " + parentCodeElementType;
            tabAsemblyName.Text = "All " + codeElementTypePlural + " in Assembly";
            Type metricsType = Type.GetType(nDependMetricDefinition.NDependMetricType);
            Type[] types = new Type[] { metricsType };
            GenericsHelper.InvokeInstanceGenericMethod(this, this.GetType().FullName, "FillBaseStatistics", types, null);
        }

        public void FillBaseStatistics<CodeElementType>()
        {
            List<CodeElementType> metricsValues = DataTableHelper.GetDataTableColumn <CodeElementType>(
                selectedCodeElementMatricsDataTable, nDependMetricDefinition.PropertyName);
            tboxParentCodeElementMinValue.Text = metricsValues.Min().ToString();
            tboxParentCodeElementMaxValue.Text = metricsValues.Max().ToString();
            List<double> doubleList = metricsValues.Select(val => Convert.ToDouble(val)).ToList();
            tboxParentCodeElementAverageValue.Text = doubleList.Average().ToString("0.0000");
            tboxParentCodeElementStdDevValue.Text = Statistics.StandardDeviation<double>(doubleList).ToString("0.0000");
        }

        private void FillMetricDescriptionRTFBox(NDependMetricDefinition nDependMetricDefinition)
        {
            rtboxMetricDescription.Clear();
            rtboxMetricDescription.AppendText(nDependMetricDefinition.MetricName);
            rtboxMetricDescription.Find(nDependMetricDefinition.MetricName);
            rtboxMetricDescription.SelectionFont = new Font(rtboxMetricDescription.Font, rtboxMetricDescription.Font.Style ^ FontStyle.Bold);
            rtboxMetricDescription.SelectionStart = this.rtboxMetricDescription.Text.Length;
            rtboxMetricDescription.SelectionLength = 0;
            rtboxMetricDescription.SelectionFont = rtboxMetricDescription.Font;
            rtboxMetricDescription.AppendText(Environment.NewLine + nDependMetricDefinition.Description);
        }



        private void ShowMetricHistoricTrendChart(string chartTitle, string serieName, IList chartData)
        {
            MetricsChart metricChart = new MetricsChart();
            metricChart.RenderSingleLineTrendChartNoXValues(chartTitle, serieName, chartData);
        }

        private void ShowMetricFrequencyBarChart(string chartTitle, string serieName, IList xData, IList yData)
        {
            MetricsChart metricChart = new MetricsChart();
            metricChart.RenderSingleVerticalBarChart(chartTitle, serieName, xData, yData);
        }

        private void btnDrawTrendChart_Click(object sender, EventArgs e)
        {
            IList metricValues = new AnalysisHistoryManager(nDependProject).GetMetricHistory(codeElementName, nDependMetricDefinition);
            string chartTitle = codeElementName.ToUpper() + ": " + codeElementName;
            ShowMetricHistoricTrendChart(chartTitle, nDependMetricDefinition.MetricName, metricValues);
        }

        private void btnParentCodeElementDrawChart_Click(object sender, EventArgs e)
        {
            string columnName = nDependMetricDefinition.PropertyName;
            string chartTitle = parentCodeElementName + ": " + nDependMetricDefinition.PropertyName;
            Type metricType = Type.GetType(nDependMetricDefinition.NDependMetricType);

            //Not Generic Solution - only uint -
            /*
            List<uint> metricsValues = DataTableHelper.GetDataTableColumn<uint>(metricsDataTable, columnName);
            Dictionary<uint, int> metricsFrequencies= Statistics.FrequencesList<uint>(metricsValues);
            IList xValues = metricsFrequencies.Keys.ToList();
            IList yValues = metricsFrequencies.Values.ToList();*/

            object[] methodParameters = new object[] { selectedCodeElementMatricsDataTable, columnName };
            var frequencies = GenericsHelper.InvokeStaticGenericMethod("NDependMetricsReporter.DataTableHelper", "GetDataTableColumnFrequencies", new Type[] {metricType}, methodParameters);
            IList xValues = (IList)GenericsHelper.InvokeStaticGenericMethod("NDependMetricsReporter.GenericsHelper", "GetDictionaryKeys", new Type[] { metricType, typeof(int) }, new object[] { frequencies });
            IList yValues = (IList)GenericsHelper.InvokeStaticGenericMethod("NDependMetricsReporter.GenericsHelper", "GetDictionaryValues", new Type[] { metricType, typeof(int) }, new object[] { frequencies });

            ShowMetricFrequencyBarChart(chartTitle, "Frequecies", xValues, yValues);
        }
    }
}
