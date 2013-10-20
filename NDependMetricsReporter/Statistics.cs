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

        public static double StandardDeviation<T>(List<T> valueList)
        {
            List<double> valueListOfTypeDouble = valueList.Select(val => Convert.ToDouble(val)).ToList();
            double average = valueListOfTypeDouble.Average();
            double sumOfSquaresOfDifferences = valueListOfTypeDouble.Select(val => (val - average) * (val - average)).Sum();
            double sd = Math.Sqrt(sumOfSquaresOfDifferences / valueListOfTypeDouble.Count);
            return sd;
        }
    }
}
