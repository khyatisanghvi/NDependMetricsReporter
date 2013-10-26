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

        List<double> metricsValuesFromAllBrotherCodeElements;
        List<double> metricsValuesOfAllSameCodeElementsInAssembly;

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

            tabParentCondeElement.Text = "All " + codeElementTypePlural + " in " + parentCodeElementName;
            if (codeElementType == "Assembly")
            {
                tabctlMetricStatistics.TabPages.Remove(tabAsemblyName);
            }
            else
            {
                tabAsemblyName.Text = "All " + codeElementTypePlural + " in " + assemblyName;
            }
            Type metricsType = Type.GetType(nDependMetricDefinition.NDependMetricType);
            Type[] types = new Type[] { metricsType };
            GenericsHelper.InvokeInstanceGenericMethod(this, this.GetType().FullName, "FillBaseStatistics", types, null);

            FillStatisticChartSelector();
        }

        public void FillBaseStatistics<CodeElementType>()
        {
            double minValue;
            double maxValue;

            List<CodeElementType> metricsValuesFromAllBrotherCodeElements_GenericValues = DataTableHelper.GetDataTableColumn<CodeElementType>(
                selectedCodeElementMatricsDataTable, nDependMetricDefinition.PropertyName);
            metricsValuesFromAllBrotherCodeElements = metricsValuesFromAllBrotherCodeElements_GenericValues.Select(val => Convert.ToDouble(val)).ToList();
            minValue = metricsValuesFromAllBrotherCodeElements.Min();
            maxValue = metricsValuesFromAllBrotherCodeElements.Max();
            tboxParentCodeElementMinValue.Text = minValue % 1 == 0 ? minValue.ToString() : minValue.ToString("0.0000");
            tboxParentCodeElementMaxValue.Text = maxValue % 1 == 0 ? maxValue.ToString() : maxValue.ToString("0.0000");
            tboxParentCodeElementAverageValue.Text = metricsValuesFromAllBrotherCodeElements.Average().ToString("0.0000");
            tboxParentCodeElementStdDevValue.Text = Statistics.StandardDeviation<double>(metricsValuesFromAllBrotherCodeElements).ToString("0.0000");

            if (codeElementType != "Assembly")
            {
                metricsValuesOfAllSameCodeElementsInAssembly = codeElementsManager.GetMetricFromAllCodeElementsInAssembly(nDependMetricDefinition, assemblyName);
                minValue = metricsValuesOfAllSameCodeElementsInAssembly.Min();
                maxValue = metricsValuesOfAllSameCodeElementsInAssembly.Max();
                tboxAllInAssemblyMinValue.Text = minValue % 1 == 0 ? minValue.ToString() : minValue.ToString("0.0000");
                tboxAllInAssemblyMaxValue.Text = maxValue % 1 == 0 ? maxValue.ToString() : maxValue.ToString("0.0000");
                tboxAllInAssemblyAverageValue.Text = metricsValuesOfAllSameCodeElementsInAssembly.Average().ToString("0.0000");
                tboxAllInAssemblyStdDevValue.Text = Statistics.StandardDeviation<double>(metricsValuesOfAllSameCodeElementsInAssembly).ToString("0.0000");
            }
        }

        private void FillStatisticChartSelector()
        {
            this.cboxParentCodeElementChartSelector.Items.Add("Frequencies Bar Chart");
            cboxParentCodeElementChartSelector.SelectedIndex = 0;

            this.cboxAllCodeElementsInAssemblyChartSelector.Items.Add("Frequencies Bar Chart");
            cboxAllCodeElementsInAssemblyChartSelector.SelectedIndex = 0;
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
            string chartTitle = parentCodeElementName.ToUpper() + ": " + codeElementName;
            ShowMetricHistoricTrendChart(chartTitle, nDependMetricDefinition.MetricName, metricValues);
        }

        private void btnParentCodeElementDrawChart_Click(object sender, EventArgs e)
        {
            string columnName = nDependMetricDefinition.PropertyName;
            string chartTitle = tabParentCondeElement.Text + ": " + nDependMetricDefinition.PropertyName;

            Dictionary<double, int> frequencies = Statistics.FrequencesList<double>(metricsValuesFromAllBrotherCodeElements);
            IList xValues = frequencies.Keys.ToList();
            IList yValues = frequencies.Values.ToList();

            /*Type metricType = Type.GetType(nDependMetricDefinition.NDependMetricType);
            object[] methodParameters = new object[] { selectedCodeElementMatricsDataTable, columnName };
            var frequencies = GenericsHelper.InvokeStaticGenericMethod("NDependMetricsReporter.DataTableHelper", "GetDataTableColumnFrequencies", new Type[] {metricType}, methodParameters);
            IList xValues = (IList)GenericsHelper.InvokeStaticGenericMethod("NDependMetricsReporter.GenericsHelper", "GetDictionaryKeys", new Type[] { metricType, typeof(int) }, new object[] { frequencies });
            IList yValues = (IList)GenericsHelper.InvokeStaticGenericMethod("NDependMetricsReporter.GenericsHelper", "GetDictionaryValues", new Type[] { metricType, typeof(int) }, new object[] { frequencies });*/

            ShowMetricFrequencyBarChart(chartTitle, "Frequecies", xValues, yValues);
        }

        private void btnAllCodeElementsInAssemblyDrawChart_Click(object sender, EventArgs e)
        {
            string columnName = nDependMetricDefinition.PropertyName;
            string chartTitle = tabAsemblyName.Text + ": " + nDependMetricDefinition.PropertyName;
            Type metricType = Type.GetType(nDependMetricDefinition.NDependMetricType);

            Dictionary<double, int> frequencies = Statistics.FrequencesList<double>(metricsValuesOfAllSameCodeElementsInAssembly);
            IList xValues = frequencies.Keys.ToList();
            IList yValues = frequencies.Values.ToList();

            ShowMetricFrequencyBarChart(chartTitle, "Frequecies", xValues, yValues);

        }
    }
}
