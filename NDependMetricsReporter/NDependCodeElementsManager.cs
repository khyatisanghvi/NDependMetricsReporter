using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDepend.CodeModel;

namespace NDependMetricsReporter
{
    class NDependCodeElementsManager
    {
        ICodeBase codeBase;
        public NDependCodeElementsManager(ICodeBase codeBase)
        {
            this.codeBase = codeBase;
        }

        public IEnumerable<IAssembly> GetAssembliesInApplication()
        {
            return codeBase.Application.Assemblies;
        }

        public IEnumerable<IAssembly> GetNonThirPartyAssembliesInApplication()
        {
            List<IAssembly> nonThirPartyAssemblies = new List<IAssembly>();
            foreach (IAssembly assembly in codeBase.Application.Assemblies)
            {
                if (assembly.IsThirdParty) continue;
                nonThirPartyAssemblies.Add(assembly);
            }
            return nonThirPartyAssemblies;
        }

        public IEnumerable<INamespace> GetNamespacesInAssembly(IAssembly assembly)
        {
            return assembly.ChildNamespaces;
        }

        public IEnumerable<INamespace> GetNamespacesInAssembly(string assemblyName)
        {
            var myAssembly = from assembly in codeBase.Application.Assemblies
                             where assembly.Name == assemblyName
                             select assembly;
            return myAssembly.ElementAt<IAssembly>(0).ChildNamespaces;
        }
    }
}
