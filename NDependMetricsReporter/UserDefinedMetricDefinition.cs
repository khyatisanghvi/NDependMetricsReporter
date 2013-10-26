using System.Xml.Serialization;

namespace NDependMetricsReporter
{
    [System.Serializable()]
    class UserDefinedMetricDefinition
    {
        private string metricTypeField;
        private string resumedMetricNameField;
        private string methodNameToInvokeField;
        private string metricNameField;
        private string descriptionField;

        [XmlElement("MetricType")]
        public string MetricType
        {
            get { return this.metricTypeField; }
            set { this.metricTypeField = value; }
        }

        [XmlElement("ResumedMetricName")]
        public string ResumedMetricName
        {
            get { return this.resumedMetricNameField; }
            set { this.resumedMetricNameField = value; }
        }

        [XmlElement("MethodNameToInvoke")]
        public string MethodNameToInvoke
        {
            get { return this.methodNameToInvokeField; }
            set { this.methodNameToInvokeField = value; }
        }

        [XmlElement("MetricName")]
        public string MetricName
        {
            get { return this.metricNameField; }
            set { this.metricNameField = value; }
        }

        [XmlElement("Description")]
        public string Description
        {
            get { return this.descriptionField; }
            set { this.descriptionField = value; }
        }
    }
}
