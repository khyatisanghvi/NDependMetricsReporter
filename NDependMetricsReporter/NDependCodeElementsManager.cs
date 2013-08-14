using System;
using System.Reflection;
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

/*        public enum AssemblyMetrics {
            NbNamespaces, NbTypes, NbMethods, NbFields,
            AsmCe, AsmCa, Level,
            NbILInst, NbLinesOfCode,
            NbLinesOfCodeCovered, NbLinesOfCodeNotCovered, PercentageCoverage,
            NbLinesOfComment, PercentageComment,
            NbTypesUsed, NbTypesUsingMe, TypeCe, TypeCa, RelationalCohesion,
            Abstractness, Instability, DistFromMainSeq, NormDistFromMainSeq
        };*/

        public enum NamespaceMetrics {
            NbTypes, NbMethods, NbFields,
            NbNamespacesUsed, NbNamespacesUsingMe, Level,
            NbILInstructions, NbLinesOfCode,
            NbLinesOfCodeCovered, NbLinesOfCodeNotCovered, PercentageCoverage,
            NbLinesOfComment, PercentageComment
        };

        public enum TypeMetrics {
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

        public enum MethodMetrics
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

        public Dictionary<NDependMetricDefinition, double> GetAssemblyMetrics(IAssembly assembly)
        {
            Dictionary<NDependMetricDefinition, double> assemblyMetrics = new Dictionary<NDependMetricDefinition, double>();
            List<NDependMetricDefinition> assemblyMetricsDefinitionsList = new NDependXMLMetricsDefinitionLoader().LoadAssemblyMetricsDefinitions();
            PropertyInfo property;
            foreach (NDependMetricDefinition assemblyMetricDefinition in assemblyMetricsDefinitionsList)
            {
                Double metricValue = 0;
                property = assembly.GetType().GetProperty(assemblyMetricDefinition.InternalPropertyName);
                if (property != null) metricValue = Convert.ToDouble(property.GetValue(assembly));
                assemblyMetrics.Add(assemblyMetricDefinition, metricValue);
            }

            return assemblyMetrics;
        }

        public Dictionary<string, string> GetAssemblyMetrics_NoReflection(IAssembly assembly)
        {
            Dictionary<string, string> assemblyMetrics = new Dictionary<string, string>();

            assemblyMetrics.Add("NbNamespaces", assembly.NbNamespaces.ToString());
            assemblyMetrics.Add("NbTypes", assembly.NbTypes.ToString());
            assemblyMetrics.Add("NbMethods", assembly.NbMethods.ToString());
            assemblyMetrics.Add("NbFields", assembly.NbFields.ToString());
            assemblyMetrics.Add("AssembliesUsingMe", assembly.AssembliesUsingMe.Count(p=>true).ToString());
            assemblyMetrics.Add("AssembliesUsed", assembly.AssembliesUsed.Count(p => true).ToString());
            assemblyMetrics.Add("Level", assembly.Level.ToString());
            assemblyMetrics.Add("NbILInstructions", assembly.NbILInstructions.ToString());
            assemblyMetrics.Add("NbLinesOfCode", assembly.NbLinesOfCode.ToString());
            assemblyMetrics.Add("NbLinesOfCodeCovered", assembly.NbLinesOfCodeCovered.ToString());
            assemblyMetrics.Add("NbLinesOfCodeNotCovered", assembly.NbLinesOfCodeNotCovered.ToString());
            assemblyMetrics.Add("PercentageCoverage", assembly.PercentageCoverage.ToString());
            assemblyMetrics.Add("NbLinesOfComment", assembly.NbLinesOfComment.ToString());
            assemblyMetrics.Add("PercentageComment", assembly.PercentageComment.ToString());
            assemblyMetrics.Add("NbTypesUsed", assembly.NbTypesUsed.ToString());
            assemblyMetrics.Add("NbTypesUsingMe", assembly.NbTypesUsingMe.ToString());
            assemblyMetrics.Add("RelationalCohesion", assembly.RelationalCohesion.ToString());
            assemblyMetrics.Add("Abstractness", assembly.Abstractness.ToString());
            assemblyMetrics.Add("Instability", assembly.Instability.ToString());
            assemblyMetrics.Add("DistFromMainSeq", assembly.DistFromMainSeq.ToString());
            assemblyMetrics.Add("NormDistFromMainSeq", assembly.NormDistFromMainSeq.ToString());

            return assemblyMetrics;
        }
    }
}
