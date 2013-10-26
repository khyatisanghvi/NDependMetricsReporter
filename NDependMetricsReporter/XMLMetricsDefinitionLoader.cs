using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace NDependMetricsReporter
{
    class XMLMetricsDefinitionLoader
    {
        string pathToXMLMetrics;

        public XMLMetricsDefinitionLoader()
        {
            this.pathToXMLMetrics = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\XMLMetricDefinitions\";
        }

        public List<NDependMetricDefinition> LoadNDependMetricsDefinitions(string xmlMetricsDefinitionFile)
        {
            return DeseralizeMetricsList<NDependMetricDefinition>(this.pathToXMLMetrics + xmlMetricsDefinitionFile);
        }

        public List<UserDefinedMetricDefinition> LoadUserDefinedMetricsDefinitions(string xmlMetricsDefinitionFile)
        {
            return DeseralizeMetricsList <UserDefinedMetricDefinition>(this.pathToXMLMetrics + xmlMetricsDefinitionFile);
        }

        private List<TypeOfMetric> DeseralizeMetricsList<TypeOfMetric>(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<TypeOfMetric>));
            TextReader textReader = new StreamReader(filePath);
            List<TypeOfMetric> metricsList;
            metricsList = (List<TypeOfMetric>)deserializer.Deserialize(textReader);
            textReader.Close();
            return metricsList;
        }
    }
}
