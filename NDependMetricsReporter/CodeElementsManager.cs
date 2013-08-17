﻿using System;
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

            typeMetrics.Add("NbTypes", nType.NbTypes.ToString());
            typeMetrics.Add("NbMethods", nType.NbMethods.ToString());
            typeMetrics.Add("NbFields", nType.NbFields.ToString());
            typeMetrics.Add("NbNamespacesUsed", nType.NbNamespacesUsed.ToString());
            typeMetrics.Add("NbNamespacesUsingMe", nType.NbNamespacesUsingMe.ToString());
            typeMetrics.Add("Level", nType.Level.ToString());
            typeMetrics.Add("NbILInstructions", nType.NbILInstructions.ToString());
            typeMetrics.Add("NbLinesOfCode", nType.NbLinesOfCode.ToString());
            typeMetrics.Add("NbLinesOfCodeCovered", nType.NbLinesOfCodeCovered.ToString());
            typeMetrics.Add("NbLinesOfCodeNotCovered", nType.NbLinesOfCodeNotCovered.ToString());
            typeMetrics.Add("PercentageCoverage", nType.PercentageCoverage.ToString());
            typeMetrics.Add("NbLinesOfComment", nType.NbLinesOfComment.ToString());
            typeMetrics.Add("PercentageComment", nType.PercentageComment.ToString());

            return typeMetrics;
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
