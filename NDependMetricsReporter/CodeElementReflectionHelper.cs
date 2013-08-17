using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDepend.CodeModel;

namespace NDependMetricsReporter
{
    class CodeElementReflectionHelper
    {
        ICodeBase codeBase;

        public CodeElementReflectionHelper(ICodeBase codeBase)
        {
            this.codeBase = codeBase;
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
            IEnumerable<IType> selectedTypes = codeBase.Application.Types.Where(a => a.Name == typeName);
            if (selectedTypes.Any()) return selectedTypes.First();
            return null;
        }

        public IMethod GetMethodByName(string methodName)
        {
            IEnumerable<IMethod> selectedTypes = codeBase.Application.Methods.Where(a => a.Name == methodName);
            if (selectedTypes.Any()) return selectedTypes.First();
            return null;
        }

        public MetricType GetCodeElementMetric<CodeElementType, MetricType>(CodeElementType codeElement, string metricname)
        {
            return (MetricType)codeElement.GetType().GetProperty(metricname).GetValue(codeElement);
        }
    }
}
