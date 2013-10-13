using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDependMetricsReporter
{
    public static class Statistics
    {
        public static Dictionary<T, int> FrequencesList<T>(List<T> valueList)
        {
            return valueList.GroupBy(n => n).ToDictionary(n => n.Key, n => n.Count());
        }

        public static IDictionary FrequencesList2<T>(IList valueList)
        {
            return ((List<T>)valueList).GroupBy(n => n).ToDictionary(n => n.Key, n => n.Count());
        }
    }
}
