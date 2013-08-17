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

        public IEnumerable<IAssembly> GetNonThirPartyAssembliesInApplication()
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
            IEnumerable<IType> selectedTypes = codeBase.Application.Types.Where(a => a.Name == typeName);
            if (selectedTypes.Any()) return selectedTypes.First();
            return null;
        }

        public IMethod GetMethodByName(string methodName)
        {
            IEnumerable<IMethod> selectedMethods = codeBase.Application.Methods.Where(a => a.Name == methodName);
            if (selectedMethods.Any()) return selectedMethods.First();
            return null;
        }

/*        public MetricType GetCodeElementMetric<CodeElementType, MetricType>(CodeElementType codeElement, string metricname)
        {
            return (MetricType)codeElement.GetType().GetProperty(metricname).GetValue(codeElement);
        }*/

        public Dictionary<NDependMetricDefinition, double> GetCodeElementMetrics(IAssembly assembly)
        {
            return null;
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

        public Dictionary<NDependMetricDefinition, double> GetNamespaceMetrics(INamespace nNamespace)
        {
            Dictionary<NDependMetricDefinition, double> namespaceMetrics = new Dictionary<NDependMetricDefinition, double>();
            List<NDependMetricDefinition> namespaceMetricsDefinitionsList = new NDependXMLMetricsDefinitionLoader().LoadNamespaceMetricsDefinitions();
            PropertyInfo property;
            PropertyInfo[] pi = nNamespace.GetType().GetProperties();
            foreach (NDependMetricDefinition namespaceMetricDefinition in namespaceMetricsDefinitionsList)
            {
                Double metricValue = 0;
                property = nNamespace.GetType().GetProperty(namespaceMetricDefinition.InternalPropertyName);
                if (property != null) metricValue = Convert.ToDouble(property.GetValue(nNamespace));
                namespaceMetrics.Add(namespaceMetricDefinition, metricValue);
            }

            return namespaceMetrics;
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

        public Dictionary<string, string> GetNamespaceMetrics_NoReflection(INamespace nNamespace)
        {
            Dictionary<string, string> assemblyMetrics = new Dictionary<string, string>();

            assemblyMetrics.Add("NbTypes", nNamespace.NbTypes.ToString());
            assemblyMetrics.Add("NbMethods", nNamespace.NbMethods.ToString());
            assemblyMetrics.Add("NbFields", nNamespace.NbFields.ToString());
            assemblyMetrics.Add("NbNamespacesUsed", nNamespace.NbNamespacesUsed.ToString());
            assemblyMetrics.Add("NbNamespacesUsingMe", nNamespace.NbNamespacesUsingMe.ToString());
            assemblyMetrics.Add("Level", nNamespace.Level.ToString());
            assemblyMetrics.Add("NbILInstructions", nNamespace.NbILInstructions.ToString());
            assemblyMetrics.Add("NbLinesOfCode", nNamespace.NbLinesOfCode.ToString());
            assemblyMetrics.Add("NbLinesOfCodeCovered", nNamespace.NbLinesOfCodeCovered.ToString());
            assemblyMetrics.Add("NbLinesOfCodeNotCovered", nNamespace.NbLinesOfCodeNotCovered.ToString());
            assemblyMetrics.Add("PercentageCoverage", nNamespace.PercentageCoverage.ToString());
            assemblyMetrics.Add("NbLinesOfComment", nNamespace.NbLinesOfComment.ToString());
            assemblyMetrics.Add("PercentageComment", nNamespace.PercentageComment.ToString());

            return assemblyMetrics;
        }
    }
}
