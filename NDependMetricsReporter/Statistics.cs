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
            //Dictionary<T, int> frequencesDictionary = valueList.GroupBy(n => n).Select(n => new { Value = n.Key, Count = n.Count() });

            //var frequencesEnumerable = valueList.GroupBy(n => n).Select(n => new { Value = n.Key, Count = n.Count() });
            //Dictionary<T, int> frequencesDictionary = new Dictionary<T, int>();
            return valueList.GroupBy(n => n).ToDictionary(n => n.Key, n => n.Count());
            //return strings.SelectMany(s => s.Split(' ')).GroupBy(w => w).ToDictionary(g => g.Key, g => g.Count());
            //return frequencesDictionary;
        }
    }
}
