using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace NDependMetricsReporter
{
    class CodeElementsManagerReflectionHelper
    {
        public CodeElementsManagerReflectionHelper()
        {
        }

        public MetricType GetCodeElementMetricUsingReflection<CodeElementType, MetricType>(CodeElementType codeElement, string metricInternalPropertyName)
        {
            return (MetricType)codeElement.GetType().GetProperty(metricInternalPropertyName).GetValue(codeElement);
        }

        public object GetCodeElementMetric(object codeElement, Type codeElementType, string metricName, string metricTypeName)
        {
            Type metricType = Type.GetType(metricTypeName);
            MethodInfo method = this.GetType().GetMethod("GetCodeElementMetricUsingReflection");
            MethodInfo genericMethod = method.MakeGenericMethod(new Type[] { codeElementType, metricType });
            return genericMethod.Invoke(this, new[] { codeElement, metricName });
        }
    }
}
