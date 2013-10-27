using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ExtensionMethods;
using System.Reflection;

namespace NDependMetricsReporter
{
    class DataTableHelper
    {
        CodeElementsManager codeElementsManager;
        UserDefinedMetrics userDefinedMetrics;

        public DataTableHelper(CodeElementsManager codeElementsManager, UserDefinedMetrics userDefinedMetrics)
        {
            this.codeElementsManager = codeElementsManager;
            this.userDefinedMetrics = userDefinedMetrics;
        }

        public DataTable CreateCodeElementMetricsDataTable<CodeElementType>(IEnumerable<CodeElementType> codeElementLists, List<NDependMetricDefinition> nDependMetricsDefinitionList, List<UserDefinedMetricDefinition> userDefinedMetricsDefinitionList)
        {
            DataTable metricsTable = new DataTable();
            AddCodeElementsColumnToTable(metricsTable);
            AddMetricsColumnsToTable(metricsTable, nDependMetricsDefinitionList, userDefinedMetricsDefinitionList);
            AddMetricRowsToTable<CodeElementType>(metricsTable, codeElementLists, nDependMetricsDefinitionList, userDefinedMetricsDefinitionList);
            return metricsTable;
        }

        public static List<T> GetDataTableColumn<T>(DataTable metricsDataTable, string columnName)
        {
            return metricsDataTable.AsEnumerable().Select(s => s.Field<T>(columnName)).ToList<T>();
        }

        public static List<object> GetDataTableColumn(DataTable metricsDataTable, string columnName)
        {
            return metricsDataTable.AsEnumerable().Select(s => s.Field<object>(columnName)).ToList<object>();
        }

        public static Dictionary<ColumnType, int> GetDataTableColumnFrequencies<ColumnType>(DataTable metricsDataTable, string columnName)
        {
            List<ColumnType> metricsValues = DataTableHelper.GetDataTableColumn<ColumnType>(metricsDataTable, columnName);
            Dictionary<ColumnType, int> metricsFrequencies = Statistics.FrequencesList<ColumnType>(metricsValues);
            return metricsFrequencies;
        }

        private void AddCodeElementsColumnToTable(DataTable metricsTable)
        {
            DataColumn codeElementNameColumn = new DataColumn("Code Element");
            codeElementNameColumn.DataType = typeof(string);
            metricsTable.Columns.Add(codeElementNameColumn);
        }

        private void AddMetricsColumnsToTable(DataTable metricsTable, List<NDependMetricDefinition> nDependMetricsDefinitionList, List<UserDefinedMetricDefinition> userDefinedMetricDefinetionList)
        {
            foreach (NDependMetricDefinition nDependMetricDefinition in nDependMetricsDefinitionList)
            {
                DataColumn metricColumn = new DataColumn(nDependMetricDefinition.PropertyName);
                metricColumn.DataType = Type.GetType(nDependMetricDefinition.NDependMetricType);
                metricsTable.Columns.Add(metricColumn);
            }

            foreach (UserDefinedMetricDefinition userdefinedMetricDefinition in userDefinedMetricDefinetionList)
            {
                DataColumn metricColumn = new DataColumn(userdefinedMetricDefinition.ResumedMetricName);
                metricColumn.DataType = Type.GetType(userdefinedMetricDefinition.MetricType);
                metricsTable.Columns.Add(metricColumn);
            }
        }

        private void AddMetricRowsToTable<CodeElementType>(DataTable metricsTable, IEnumerable<CodeElementType> codeElementLists, List<NDependMetricDefinition> nDependMetricsDefinitionList, List<UserDefinedMetricDefinition> userDefinedMetricDefinetionList)
        {
            foreach (CodeElementType codeElement in codeElementLists)
            {
                DataRow row = metricsTable.NewRow();
                string codeElementName = (string)typeof(CodeElementType).GetPublicProperty("Name").GetValue(codeElement);
                row[0] = codeElementName;
                foreach (NDependMetricDefinition nDependMetricDefinition in nDependMetricsDefinitionList)
                {
                    row[nDependMetricDefinition.PropertyName] = codeElementsManager.GetCodeElementMetricValue<CodeElementType>((CodeElementType)codeElement, nDependMetricDefinition);
                }
                foreach (UserDefinedMetricDefinition userDefinedMetricDefinition in userDefinedMetricDefinetionList)
                {
                    row[userDefinedMetricDefinition.ResumedMetricName] = userDefinedMetrics.InvokeUserDefinedMetric(codeElementName, userDefinedMetricDefinition.MethodNameToInvoke);
                }
                metricsTable.Rows.Add(row);
            }
        }
    }
}
