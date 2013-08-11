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

        enum AssemblyMetrics {
            NbNamespaces, NbTypes, NbMethods, NbFields,
            AssembliesUsed, AssembliesUsingMe, Level,
            NbILInstructions, NbLinesOfCode,
            NbLinesOfCodeCovered, NbLinesOfCodeNotCovered, PercentageCoverage,
            NbLinesOfComment, PercentageComment,
            NbTypesUsed, NbTypesUsingMe, RelationalCohesion,
            Abstractness, Instability, DistFromMainSeq, NormDistFromMainSeq
        };

        enum NamespaceMetrics {
            NbTypes, NbMethods, NbFields,
            NbNamespacesUsed, NbNamespacesUsingMe, Level,
            NbILInstructions, NbLinesOfCode,
            NbLinesOfCodeCovered, NbLinesOfCodeNotCovered, PercentageCoverage,
            NbLinesOfComment, PercentageComment
        };

        enum TypeMetrics {
            NbMethods, NbFields, LCOM, LCOMHS,
            NbTypesUsed, NbTypesUsingMe, Level, Rank,
            NbILInstructions, NbLinesOfCode,
            NbLinesOfCodeCovered, NbLinesOfCodeNotCovered, PercentageCoverage,
            NbLinesOfComment, PercentageComment,
            CyclomaticComplexity, ILCyclomaticComplexity,
            SizeOfInst,
            ABT,
            NbChildren, DepthOfInheritance
        };

        enum MethodMetrics
        {
            NbVariables, NbParameters,
            NbOverloads,
            NbMethodsCalled, NbMethodsCallingMe, level, Rank,
            NbILInstructions, NbLinesOfCode,
            NbLinesOfCodeCovered, NbLinesOfCodeNotCovered, PercentageCoverage, PercentageBranchCoverage,
            NbLinesOfComment, PercentageComment,
            CyclomaticComplexity, ILCyclomaticComplexity, ILNestingDepth
        };

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

        public IEnumerable<INamespace> GetNamespacesInAssembly(string assemblyName)
        {
            return GetAssemblyByName(assemblyName).ChildNamespaces;
        }

        public IEnumerable<IType> GetTypesInAssembly(string assemblyName)
        {
            return GetAssemblyByName(assemblyName).ChildTypes;
        }

        public IEnumerable<IType> GetTypesInNamespace(string namespaceName)
        {
            return GetNamespaceByName(namespaceName).ChildTypes;
        }

        public IAssembly GetAssemblyByName(string assemblyName)
        {
            return codeBase.Application.Assemblies.Where(a => a.Name == assemblyName).First();
        }

        public INamespace GetNamespaceByName(string namespaceName)
        {
            return codeBase.Application.Namespaces.Where(n => n.Name == namespaceName).First();
        }

        public IType GetTypeByName(string typeName)
        {
            return codeBase.Application.Types.Where(t => t.Name == typeName).First();
        }

        public IEnumerable<INamespace> GetNamespaces(string basePropertyName, string propertyToGetName)
        {
            
            return null;
        }
    }
}
