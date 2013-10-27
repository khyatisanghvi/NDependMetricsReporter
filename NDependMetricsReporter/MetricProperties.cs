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
        UserDefinedMetricDefinition userDefinedMetricDefinition;
        MetricDefinitionType metricDefinitionType;

        DataTable selectedCodeElementMatricsDataTable;
        string codeElementName;
        string codeElementType;
        string parentCodeElementName;
        string assemblyName;
        string metricResumedName;
        string metricFullName;
        string metricDescription;
        string metricType;

        IProject nDependProject;
        CodeElementsManager codeElementsManager;
        UserDefinedMetrics userDefinedMetrics;

        Dictionary<string, string> codeElementsTypePlurals = new Dictionary<string,string>() { { "Assembly", "Assemblies" }, { "Namespace", "Namepaces" }, { "Type", "Types" }, { "Method", "Methods" } };
        Dictionary<string, string> codeElementsTypePrecedences = new Dictionary<string, string>() { { "Assembly", "Application" }, { "Namespace", "Assembly" }, { "Type", "Namespace" }, { "Method", "Type" } };

        List<double> metricsValuesFromAllBrotherCodeElements;
        List<double> metricsValuesOfAllSameCodeElementsInAssembly;

        private MetricProperties(
            string codeElementName,
            DataTable selectedCodeElementMatricsDataTable,
            string parentCodeElementName,
            string assemblyName,
            IProject nDependProject)
        {
            InitializeComponent();
            
            this.codeElementName = codeElementName;
            this.selectedCodeElementMatricsDataTable = selectedCodeElementMatricsDataTable;
            this.parentCodeElementName = parentCodeElementName;
            this.assemblyName = assemblyName;

            InitCodeAnalisysElements(nDependProject);
        }

        public MetricProperties(
            NDependMetricDefinition nDependMetricDefinition,
            string codeElementName,
            DataTable selectedCodeElementMatricsDataTable,
            string parentCodeElementName,
            string assemblyName,
            IProject nDependProject)
            :this(codeElementName, selectedCodeElementMatricsDataTable, parentCodeElementName, assemblyName, nDependProject)
        {
            this.metricDefinitionType = MetricDefinitionType.NDependMetric;
            this.nDependMetricDefinition = nDependMetricDefinition;
            this.codeElementType = nDependMetricDefinition.NDependCodeElementType.Split('.').Last<string>().Substring(1);
            this.metricResumedName = nDependMetricDefinition.PropertyName;
            this.metricFullName = nDependMetricDefinition.MetricName;
            this.metricDescription = nDependMetricDefinition.Description;
            this.metricType = nDependMetricDefinition.NDependMetricType;
              
            FillControls();
        }

        public MetricProperties(
            UserDefinedMetricDefinition userDefinedMetricDefinition,
            string codeElementName,
            DataTable selectedCodeElementMatricsDataTable,
            string parentCodeElementName,
            string assemblyName,
            IProject nDependProject)
            : this(codeElementName, selectedCodeElementMatricsDataTable, parentCodeElementName, assemblyName, nDependProject)
        {
            this.metricDefinitionType = MetricDefinitionType.UserDefinedMetric;
            this.userDefinedMetricDefinition = userDefinedMetricDefinition;
            this.codeElementType = userDefinedMetricDefinition.NDependCodeElementType.Split('.').Last<string>().Substring(1);
            this.metricResumedName = userDefinedMetricDefinition.ResumedMetricName;
            this.metricFullName = userDefinedMetricDefinition.MetricName;
            this.metricDescription = userDefinedMetricDefinition.Description;
            this.metricType = userDefinedMetricDefinition.MetricType;

            FillControls();
        }

        enum MetricDefinitionType {NDependMetric, UserDefinedMetric};
        
/*        public MetricProperties(
            NDependMetricDefinition nDependMetricDefinition,
            UserDefinedMetricDefinition userDefinedMetricDefinition,
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
        }*/

        private void InitCodeAnalisysElements(IProject nDependProject)
        {
            this.nDependProject = nDependProject;
            ICodeBase lastAnalysisCodebase = new CodeBaseManager(nDependProject).LoadLastCodebase();
            codeElementsManager = new CodeElementsManager(lastAnalysisCodebase);
            userDefinedMetrics = new UserDefinedMetrics(lastAnalysisCodebase);
        }

        private void FillControls()
        {
            string codeElementTypePlural = codeElementsTypePlurals[codeElementType];
            string parentCodeElementType = codeElementsTypePrecedences[codeElementType];

            this.lblMetricName.Text = metricResumedName;
            this.lblCodeElementName.Text = codeElementName;
            this.lblCodeElementType.Text = codeElementType;
            this.lblParentCodeElementName.Text = parentCodeElementName;
            this.lblParentCodeElementType.Text = parentCodeElementType;
            this.lblAssemblyName.Text = assemblyName;

            FillMetricDescriptionRTFBox(metricFullName, metricDescription);

            tabParentCondeElement.Text = "All " + codeElementTypePlural + " in " + parentCodeElementName;
            if (codeElementType == "Assembly")
            {
                tabctlMetricStatistics.TabPages.Remove(tabAsemblyName);
            }
            else
            {
                tabAsemblyName.Text = "All " + codeElementTypePlural + " in " + assemblyName;
            }
            //Type metricsType = Type.GetType(nDependMetricDefinition.NDependMetricType);
            Type metricsType = Type.GetType(this.metricType);
            Type[] types = new Type[] { metricsType };
            GenericsHelper.InvokeInstanceGenericMethod(this, this.GetType().FullName, "FillBaseStatistics", types, null);

            FillStatisticChartSelector();
        }

        public void FillBaseStatistics<CodeElementType>()
        {
            double minValue;
            double maxValue;

            //List<CodeElementType> metricsValuesFromAllBrotherCodeElements_GenericValues = DataTableHelper.GetDataTableColumn<CodeElementType>(
            //    selectedCodeElementMatricsDataTable, nDependMetricDefinition.PropertyName);
            List<CodeElementType> metricsValuesFromAllBrotherCodeElements_GenericValues = DataTableHelper.GetDataTableColumn<CodeElementType>(
                selectedCodeElementMatricsDataTable, this.metricResumedName);
            metricsValuesFromAllBrotherCodeElements = metricsValuesFromAllBrotherCodeElements_GenericValues.Select(val => Convert.ToDouble(val)).ToList();
            gbxParentCodeElementNameBasicStats.Text = "Basic Stats - Count: " + metricsValuesFromAllBrotherCodeElements.Count;
            minValue = metricsValuesFromAllBrotherCodeElements.Min();
            maxValue = metricsValuesFromAllBrotherCodeElements.Max();
            tboxParentCodeElementMinValue.Text = minValue % 1 == 0 ? minValue.ToString() : minValue.ToString("0.0000");
            tboxParentCodeElementMaxValue.Text = maxValue % 1 == 0 ? maxValue.ToString() : maxValue.ToString("0.0000");
            tboxParentCodeElementAverageValue.Text = metricsValuesFromAllBrotherCodeElements.Average().ToString("0.0000");
            tboxParentCodeElementStdDevValue.Text = Statistics.StandardDeviation<double>(metricsValuesFromAllBrotherCodeElements).ToString("0.0000");

            if (codeElementType != "Assembly")
            {
                switch (metricDefinitionType)
                {
                    case MetricDefinitionType.NDependMetric:
                        metricsValuesOfAllSameCodeElementsInAssembly = codeElementsManager.GetMetricFromAllCodeElementsInAssembly(nDependMetricDefinition, assemblyName);
                        break;
                    case MetricDefinitionType.UserDefinedMetric:
                        metricsValuesOfAllSameCodeElementsInAssembly = userDefinedMetrics.GetUserDefinedMetricFromAllCodeElementsInAssembly(userDefinedMetricDefinition, assemblyName);
                        break;
                }
                //metricsValuesOfAllSameCodeElementsInAssembly = codeElementsManager.GetMetricFromAllCodeElementsInAssembly(nDependMetricDefinition, assemblyName);
                this.gbxAssemplyBasicStats.Text = "Basic Stats - Count: " + metricsValuesOfAllSameCodeElementsInAssembly.Count;
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

        private void FillMetricDescriptionRTFBox(string metricName, string metricDescription)
        {
            rtboxMetricDescription.Clear();
            rtboxMetricDescription.AppendText(metricName);
            rtboxMetricDescription.Find(metricName);
            rtboxMetricDescription.SelectionFont = new Font(rtboxMetricDescription.Font, rtboxMetricDescription.Font.Style ^ FontStyle.Bold);
            rtboxMetricDescription.SelectionStart = this.rtboxMetricDescription.Text.Length;
            rtboxMetricDescription.SelectionLength = 0;
            rtboxMetricDescription.SelectionFont = rtboxMetricDescription.Font;
            rtboxMetricDescription.AppendText(Environment.NewLine + metricDescription);
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
            IList metricValues = new List<double>();
            switch (metricDefinitionType)
            {
                case MetricDefinitionType.NDependMetric:
                    metricValues = new AnalysisHistoryManager(nDependProject).GetMetricHistory(codeElementName, nDependMetricDefinition);
                    break;
                case MetricDefinitionType.UserDefinedMetric:
                    metricValues = new List<double>();
                    break;
            }
            //IList metricValues = new AnalysisHistoryManager(nDependProject).GetMetricHistory(codeElementName, nDependMetricDefinition);
            string chartTitle = parentCodeElementName.ToUpper() + ": " + codeElementName;
            //ShowMetricHistoricTrendChart(chartTitle, nDependMetricDefinition.MetricName, metricValues);
            ShowMetricHistoricTrendChart(chartTitle, this.metricFullName, metricValues);
        }

        private void btnParentCodeElementDrawChart_Click(object sender, EventArgs e)
        {
            //string columnName = this.metricResumedName; ;
            //string chartTitle = tabParentCondeElement.Text + ": " + nDependMetricDefinition.PropertyName;
            string chartTitle = tabParentCondeElement.Text + ": " + this.metricResumedName;

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
            //string columnName = nDependMetricDefinition.PropertyName;
            //string chartTitle = tabAsemblyName.Text + ": " + nDependMetricDefinition.PropertyName;
            string chartTitle = tabAsemblyName.Text + ": " + this.metricResumedName;
            //Type metricType = Type.GetType(nDependMetricDefinition.NDependMetricType);
            Type metricType = Type.GetType(this.metricType);

            Dictionary<double, int> frequencies = Statistics.FrequencesList<double>(metricsValuesOfAllSameCodeElementsInAssembly);
            IList xValues = frequencies.Keys.ToList();
            IList yValues = frequencies.Values.ToList();

            ShowMetricFrequencyBarChart(chartTitle, "Frequecies", xValues, yValues);
        }
    }
}
