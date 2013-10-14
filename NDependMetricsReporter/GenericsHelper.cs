using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace NDependMetricsReporter
{
    public class GenericsHelper
    {
        public static IList CreateListOfType(string typeName)
        {
            Type userDefinedType = Type.GetType(typeName);
            IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(userDefinedType));
            return list;
        }

        public static IDictionary CreateDictionaryOfTypes(string keyType, string valueType)
        {
            Type userDefinedKeyType = Type.GetType(keyType);
            Type userDefinedValueType = Type.GetType(valueType);
            Type[] types = new Type[] {userDefinedKeyType, userDefinedValueType};
            IDictionary dictionary = (IDictionary)Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(types));
            return dictionary;
        }

        //Another way that works
        public static IList CreateGenericList2(string typeName)
        {
            Type list = typeof(List<>);
            Type listOfTypename = list.MakeGenericType(Type.GetType("People"));
            return (IList)listOfTypename.GetConstructor(Type.EmptyTypes).Invoke(null);
        }

        public static IList CreateNulableListOfType(string typeName)
        {
            Type userDefinedType = Type.GetType(typeName);
            Type nullableMetricType = typeof(Nullable<>).MakeGenericType(userDefinedType);
            IList metricValues = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(nullableMetricType));
            return metricValues;
        }

        public static List<KeyType> GetDictionaryKeys<KeyType, ValueType>(Dictionary<KeyType, ValueType> myDictionary)
        {
            return myDictionary.Keys.ToList<KeyType>();
        }

        public static List<ValueType> GetDictionaryValues<KeyType, ValueType>(Dictionary<KeyType, ValueType> myDictionary)
        {
            return myDictionary.Values.ToList<ValueType>();
        }

        public static object InvokeInstanceGenericMethod(string className, string methodName, Type[] genericType, object[] parameters)
        {
            Type classType = Type.GetType(className);
            MethodInfo methodInfo = classType.GetMethod(methodName);
            var tmpInstance = Activator.CreateInstance(classType);
            MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(genericType);
            return genericMethodInfo.Invoke(tmpInstance, parameters);
        }

        public static object InvokeStaticGenericMethod(string className, string methodName, Type[] genericTypes, object[] parameters)
        {
            Type classType = Type.GetType(className);
            MethodInfo methodInfo = classType.GetMethod(methodName);
            MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(genericTypes);
            return genericMethodInfo.Invoke(null, parameters);
        }
    }
}
