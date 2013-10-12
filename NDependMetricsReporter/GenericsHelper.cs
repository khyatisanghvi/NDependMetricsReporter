using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace NDependMetricsReporter
{
    public static class GenericsHelper
    {
        public static IList CreateListOfType(string typeName)
        {
            Type userDefinedType = Type.GetType(typeName);
            IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(userDefinedType));
            return list;
        }

        //Another way that works
        /*public static IList CreateGenericList(string typeName)
        {
            Type list = typeof(List<>);
            Type listOfTypename = list.MakeGenericType(Type.GetType("People"));
            return (IList)listOfTypename.GetConstructor(Type.EmptyTypes).Invoke(null);
        }*/

        public static IList CreateNulableListOfType(string typeName)
        {
            Type userDefinedType = Type.GetType(typeName);
            Type nullableMetricType = typeof(Nullable<>).MakeGenericType(userDefinedType);
            IList metricValues = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(nullableMetricType));
            return metricValues;
        }

        public static object InvokeInstanceGenericMethod(string className, string methodName, Type genericType, object[] parameters)
        {
            Type classType = Type.GetType(className);
            MethodInfo methodInfo = classType.GetMethod(methodName);
            MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(genericType);
            var tmpInstance = Activator.CreateInstance(classType);
            return genericMethodInfo.Invoke(tmpInstance, parameters);
        }

        public static object InvokeStaticGenericMethod(string className, string methodName, Type genericType, object[] parameters)
        {
            Type classType = Type.GetType(className);
            MethodInfo methodInfo = classType.GetMethod(methodName);
            MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(genericType);
            return genericMethodInfo.Invoke(null, parameters);
        }
    }
}
