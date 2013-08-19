using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDepend.CodeModel;

namespace NDependMetricsReporter
{
    class CodeElementsManager
    {
        ICodeBase codeBase;
        public CodeElementsManager(ICodeBase codeBase)
        {
            this.codeBase = codeBase;
        }

        public IEnumerable<IAssembly> GetNonThirdPartyAssembliesInApplication()
        {
            return codeBase.Application.Assemblies.Where(a => !a.IsThirdParty);
        }

        public IAssembly GetAssemblyByName(string assemblyName)
        {
            IEnumerable<IAssembly> selectedAssemblies = codeBase.Application.Assemblies.Where(a => a.Name == assemblyName);
            if (selectedAssemblies.Any()) return selectedAssemblies.First();
            return null;
        }

        public INamespace GetNamespaceByName(string namespaceName)
        {
            IEnumerable<INamespace> selectedNamespaces = codeBase.Application.Namespaces.Where(a => a.Name == namespaceName);
            if (selectedNamespaces.Any()) return selectedNamespaces.First();
            return null;
        }

        public IType GetTypeByName(string typeName)
        {
            string typeWithoutNamespace = typeName.Substring(typeName.LastIndexOf(".") + 1);
            IEnumerable<IType> selectedTypes = codeBase.Application.Types.Where(a => a.Name == typeWithoutNamespace);
            if (selectedTypes.Any()) return selectedTypes.First();
            return null;
        }

        public IMethod GetMethodByName(string methodName)
        {
            IEnumerable<IMethod> selectedMethods = codeBase.Application.Methods.Where(a => a.Name == methodName);
            if (selectedMethods.Any()) return selectedMethods.First();
            return null;
        }

        public Dictionary<NDependMetricDefinition, double> GetCodeElementMetrics<CodeElementType>(CodeElementType codeElement, string xmlMetricDefinitionFile)
        {
            List<NDependMetricDefinition> codeElementMetricsDefinitionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions(xmlMetricDefinitionFile);
            return GetCodeElementMetricsValues<CodeElementType>(codeElement, codeElementMetricsDefinitionsList);
        }

        private Dictionary<NDependMetricDefinition, double> GetCodeElementMetricsValues<T>(T codeElement, List<NDependMetricDefinition> nDependMetricsDefinitions)
        {
            Dictionary<NDependMetricDefinition, double> codeElementMetrics = new Dictionary<NDependMetricDefinition, double>();
            PropertyInfo property;
            foreach (NDependMetricDefinition codeElementMetricDefinition in nDependMetricsDefinitions)
            {
                Double metricValue = 0;
                property = codeElement.GetType().GetProperty(codeElementMetricDefinition.InternalPropertyName);
                if (property != null) metricValue = Convert.ToDouble(property.GetValue(codeElement));
                codeElementMetrics.Add(codeElementMetricDefinition, metricValue);
            }
            return codeElementMetrics;
        }
    }
}
