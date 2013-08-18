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

        public Dictionary<NDependMetricDefinition, double> GetAssemblyMetrics(IAssembly assembly)
        {
            List<NDependMetricDefinition> assemblyMetricsDefinitionsList = new NDependXMLMetricsDefinitionLoader().LoadAssemblyMetricsDefinitions();
            return GetCodeElementMetrics<IAssembly>(assembly, assemblyMetricsDefinitionsList);
        }

        public Dictionary<NDependMetricDefinition, double> GetNamespaceMetrics(INamespace nNamespace)
        {
            List<NDependMetricDefinition> namespaceMetricsDefinitionsList = new NDependXMLMetricsDefinitionLoader().LoadNamespaceMetricsDefinitions();
            return GetCodeElementMetrics<INamespace>(nNamespace, namespaceMetricsDefinitionsList);
        }

        public Dictionary<NDependMetricDefinition, double> GetTypeMetrics(IType nType)
        {
            List<NDependMetricDefinition> typeMetricsDefinitionsList = new NDependXMLMetricsDefinitionLoader().LoadTypeMetricsDefinitions();
            return GetCodeElementMetrics<IType>(nType, typeMetricsDefinitionsList);
        }

        public Dictionary<NDependMetricDefinition, double> GetMethodMetrics(IMethod nMethod)
        {
            List<NDependMetricDefinition> methodMetricsDefinitionsList = new NDependXMLMetricsDefinitionLoader().LoadMethodMetricsDefinitions();
            return GetCodeElementMetrics<IMethod>(nMethod, methodMetricsDefinitionsList);
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
            Dictionary<string, string> namespaceMetrics = new Dictionary<string, string>();

            namespaceMetrics.Add("NbTypes", nNamespace.NbTypes.ToString());
            namespaceMetrics.Add("NbMethods", nNamespace.NbMethods.ToString());
            namespaceMetrics.Add("NbFields", nNamespace.NbFields.ToString());
            namespaceMetrics.Add("NbNamespacesUsed", nNamespace.NbNamespacesUsed.ToString());
            namespaceMetrics.Add("NbNamespacesUsingMe", nNamespace.NbNamespacesUsingMe.ToString());
            namespaceMetrics.Add("Level", nNamespace.Level.ToString());
            namespaceMetrics.Add("NbILInstructions", nNamespace.NbILInstructions.ToString());
            namespaceMetrics.Add("NbLinesOfCode", nNamespace.NbLinesOfCode.ToString());
            namespaceMetrics.Add("NbLinesOfCodeCovered", nNamespace.NbLinesOfCodeCovered.ToString());
            namespaceMetrics.Add("NbLinesOfCodeNotCovered", nNamespace.NbLinesOfCodeNotCovered.ToString());
            namespaceMetrics.Add("PercentageCoverage", nNamespace.PercentageCoverage.ToString());
            namespaceMetrics.Add("NbLinesOfComment", nNamespace.NbLinesOfComment.ToString());
            namespaceMetrics.Add("PercentageComment", nNamespace.PercentageComment.ToString());

            return namespaceMetrics;
        }

        public Dictionary<string, string> GetTypeMetrics_NoReflection(IType nType)
        {
            Dictionary<string, string> typeMetrics = new Dictionary<string, string>();
            typeMetrics.Add("NbMethods", nType.NbMethods.ToString());
            typeMetrics.Add("NbFields", nType.NbFields.ToString());
            typeMetrics.Add("LCOM", nType.LCOM.ToString());
            typeMetrics.Add("LCOMHS", nType.LCOMHS.ToString());
            typeMetrics.Add("NbTypesUsed", nType.NbTypesUsed.ToString());
            typeMetrics.Add("NbTypesUsingMe", nType.NbTypesUsingMe.ToString());
            typeMetrics.Add("Level", nType.Level.ToString());
            typeMetrics.Add("Rank", nType.Rank.ToString());
            typeMetrics.Add("ABT", nType.ABT.ToString());
            typeMetrics.Add("NbILInstructions", nType.NbILInstructions.ToString());
            typeMetrics.Add("NbLinesOfCode", nType.NbLinesOfCode.ToString());
            typeMetrics.Add("NbLinesOfCodeCovered", nType.NbLinesOfCodeCovered.ToString());
            typeMetrics.Add("NbLinesOfCodeNotCovered", nType.NbLinesOfCodeNotCovered.ToString());
            typeMetrics.Add("PercentageCoverage", nType.PercentageCoverage.ToString());
            typeMetrics.Add("NbLinesOfComment", nType.NbLinesOfComment.ToString());
            typeMetrics.Add("PercentageComment", nType.PercentageComment.ToString());
            typeMetrics.Add("CyclomaticComplexity", nType.CyclomaticComplexity.ToString());
            typeMetrics.Add("ILCyclomaticComplexity", nType.ILCyclomaticComplexity.ToString());
            typeMetrics.Add("SizeOfInst", nType.SizeOfInst.ToString());
            typeMetrics.Add("NbChildren", nType.NbChildren.ToString());
            typeMetrics.Add("DepthOfInheritance", nType.DepthOfInheritance.ToString());

            return typeMetrics;
        }

        public Dictionary<string, string> GetMethodMetrics_NoReflection(IMethod nMethod)
        {
            Dictionary<string, string> methodMetrics = new Dictionary<string, string>();
            methodMetrics.Add("NbVariables", nMethod.NbVariables.ToString());
            methodMetrics.Add("NbParameters", nMethod.NbParameters.ToString());
            methodMetrics.Add("NbOverloads", nMethod.NbOverloads.ToString());
            methodMetrics.Add("NbMethodsCalled", nMethod.NbMethodsCalled.ToString());
            methodMetrics.Add("NbMethodsCallingMe", nMethod.NbMethodsCallingMe.ToString());
            methodMetrics.Add("Level", nMethod.Level.ToString());
            methodMetrics.Add("Rank", nMethod.Rank.ToString());
            methodMetrics.Add("NbILInstructions", nMethod.NbILInstructions.ToString());
            methodMetrics.Add("NbLinesOfCode", nMethod.NbLinesOfCode.ToString());
            methodMetrics.Add("NbLinesOfCodeCovered", nMethod.NbLinesOfCodeCovered.ToString());
            methodMetrics.Add("NbLinesOfCodeNotCovered", nMethod.NbLinesOfCodeNotCovered.ToString());
            methodMetrics.Add("PercentageCoverage", nMethod.PercentageCoverage.ToString());
            methodMetrics.Add("NbLinesOfComment", nMethod.NbLinesOfComment.ToString());
            methodMetrics.Add("PercentageComment", nMethod.PercentageComment.ToString());
            methodMetrics.Add("CyclomaticComplexity", nMethod.CyclomaticComplexity.ToString());
            methodMetrics.Add("ILCyclomaticComplexity", nMethod.ILCyclomaticComplexity.ToString());
            methodMetrics.Add("ILNestingDepth", nMethod.ILNestingDepth.ToString());

            return methodMetrics;
        }

        private Dictionary<NDependMetricDefinition, double> GetCodeElementMetrics<T>(T codeElement, List<NDependMetricDefinition> nDependMetricsDefinitions)
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
