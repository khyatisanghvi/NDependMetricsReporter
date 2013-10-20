using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ExtensionMethods;

namespace NDependMetricsReporter
{
    class DataTableHelper
    {
        CodeElementsManager codeElementsManager;

        public DataTableHelper(CodeElementsManager codeElementsManager)
        {
            this.codeElementsManager = codeElementsManager;
        }

        public DataTable CreateCodeElemetMetricsDataTable<CodeElementType>(IEnumerable<CodeElementType> codeElementLists, List<NDependMetricDefinition> metricsDefinitionList)
        {
            DataTable metricsTable = new DataTable();
            AddCodeElementsColumnToTable(metricsTable);
            AddMetricsColumnsToTable(metricsTable, metricsDefinitionList);
            AddMetricRowsToTable<CodeElementType>(metricsTable, codeElementLists, metricsDefinitionList);
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

        private void AddMetricsColumnsToTable(DataTable metricsTable, List<NDependMetricDefinition> metricsDefinitionList)
        {
            foreach (NDependMetricDefinition metricDefinition in metricsDefinitionList)
            {
                DataColumn metricColumn = new DataColumn(metricDefinition.PropertyName);
                metricColumn.DataType = Type.GetType(metricDefinition.NDependMetricType);
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
    }
}
