using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDepend.CodeModel;
using NDepend.CodeQuery;
using NDepend.Analysis;
using System.Reflection;

namespace NDependMetricsReporter
{
    public class UserDefinedMetrics
    {
        ICodeBase codeBaseToStudy;
        CodeElementsManager codeElementsManager;

        public UserDefinedMetrics(ICodeBase codeBaseToStudy)
        {
            this.codeBaseToStudy = codeBaseToStudy;
            codeElementsManager = new CodeElementsManager(codeBaseToStudy);
        }

/*        public void CheckStringCodeQuery(ICodeBase codeBase)
        {
            string s = "from m in Assemblies.WithNameLike(\"UnitTest\") select new { m, m.NbLinesOfCode }";
            IQueryCompiled compiledString = s.Compile(codeBase);
            IQueryExecutionResult result = compiledString.QueryCompiledSuccess.Execute();
            IQueryExecutionSuccessResult successResult = result.SuccessResult;
        }*/

        public double InvokeUserDefinedMetric(string codeElementName, string methodNameToInvoke)
        {
            string[] parameters = new string[] { codeElementName };
            Type userDefinedClassType = typeof(UserDefinedMetrics);
            MethodInfo methodInfo = userDefinedClassType.GetMethod(methodNameToInvoke);
            var returnedValue = methodInfo.Invoke(this, parameters);
            return Convert.ToDouble(returnedValue);
        }

        public List<double> GetUserDefinedMetricFromAllCodeElementsInAssembly(UserDefinedMetricDefinition userDefinedMetricDefinition, string assemblyName)
        {
            string codeElementType = userDefinedMetricDefinition.NDependCodeElementType;
            switch (codeElementType)
            {
                case "NDepend.CodeModel.IAssembly":
                    return null;
                case "NDepend.CodeModel.INamespace":
                    return (from m in codeBaseToStudy.Assemblies.WithName(assemblyName).ChildNamespaces()
                            select InvokeUserDefinedMetric(m.Name, userDefinedMetricDefinition.MethodNameToInvoke)).ToList();
                case "NDepend.CodeModel.IType":
                    return (from m in codeBaseToStudy.Assemblies.WithName(assemblyName).ChildTypes()
                            select InvokeUserDefinedMetric(m.Name, userDefinedMetricDefinition.MethodNameToInvoke)).ToList();
                case "NDepend.CodeModel.IMethod":
                    return (from m in codeBaseToStudy.Assemblies.WithName(assemblyName).ChildMethods()
                            select InvokeUserDefinedMetric(m.Name, userDefinedMetricDefinition.MethodNameToInvoke)).ToList();
            }
            return null;
        }

        public int CountAppLogicMethodsCalled(string methodName)
        {
            return CountMethodsCalledFromAssembly(methodName, "RCNGCMembersManagementAppLogic");
        }

        public double AverageLCOM(string assemblyName)
        {
            IAssembly assembly = codeElementsManager.GetAssemblyByName(assemblyName);
            float? average = assembly.ChildTypes.Select(type => type.LCOM).Average();
            return (average.HasValue ? average.Value : 0);
        }

        public double AverageLinesOfCodeInMethods(string assemblyName)
        {
            IAssembly assembly = codeElementsManager.GetAssemblyByName(assemblyName);
            List<uint?> linesOfCodeOfAllMethods = assembly.ChildMethods.Select(method => method.NbLinesOfCode).ToList();
            decimal? averageLines = linesOfCodeOfAllMethods.Average(value=> (decimal?)value);
            return (averageLines.HasValue ? (double)averageLines.Value : 0);
        }

        public double AverageCyclomaticComplexity(string assemblyName)
        {
            IAssembly assembly = codeElementsManager.GetAssemblyByName(assemblyName);
            List<ushort?> ciclomaticComplexityOfAllMethods = assembly.ChildMethods.Select(method => method.CyclomaticComplexity).ToList();
            decimal? average = ciclomaticComplexityOfAllMethods.Average(value => (decimal?)value);
            return (average.HasValue ? (double)average.Value : 0);
        }

        public double AverageNestingDepth(string assemblyName)
        {
            IAssembly assembly = codeElementsManager.GetAssemblyByName(assemblyName);
            List<ushort?> nestingDepthOfAllMethods = assembly.ChildMethods.Select(method => method.ILNestingDepth).ToList();
            decimal? average = nestingDepthOfAllMethods.Average(value => (decimal?)value);
            return (average.HasValue ? (double)average.Value : 0);
        }

        public double AverageMethodCe(string assemblyName)
        {
            IAssembly assembly = codeElementsManager.GetAssemblyByName(assemblyName);
            List<uint?> metodCeOfAllMethods = assembly.ChildMethods.Select(method => method.NbMethodsCalled).ToList();
            decimal? average = metodCeOfAllMethods.Average(value => (decimal?)value);
            return (average.HasValue ? (double)average.Value : 0);
        }

        public double AverageTypeCe(string assemblyName)
        {
            IAssembly assembly = codeElementsManager.GetAssemblyByName(assemblyName);
            List<uint?> typeCeOfAllTypes = assembly.ChildTypes.Select(method => method.NbTypesUsed).ToList();
            decimal? average = typeCeOfAllTypes.Average(value => (decimal?)value);
            return (average.HasValue ? (double)average.Value : 0);
        }

        public double AverageABC(string assemblyName)
        {
            IAssembly assembly = codeElementsManager.GetAssemblyByName(assemblyName);
            List<ushort?> abcOfAllTypes = assembly.ChildTypes.Select(method => method.ABT).ToList();
            decimal? average = abcOfAllTypes.Average(value => (decimal?)value);
            return (average.HasValue ? (double)average.Value : 0);
        }

        public double AverageLCOMHS(string assemblyName)
        {
            IAssembly assembly = codeElementsManager.GetAssemblyByName(assemblyName);
            List<float?> lCOMHSOfAllTypes = assembly.ChildTypes.Select(type => type.LCOMHS).ToList();
            double? average = lCOMHSOfAllTypes.Average(value => (double?)value);
            return (average.HasValue ? (double)average.Value : 0);
        }

        public int MethodsCallingMeNotFromTests(string methodName)
        {
            IAssembly testAssembly = codeElementsManager.GetAssemblyByName("RCNGCMembersManagementUnitTests");
            IAssembly bDDAssembly = codeElementsManager.GetAssemblyByName("RCNGCMembersManagementSpecFlowBDD");
            IMethod method = codeElementsManager.GetMethodByName(methodName);
            return method.MethodsCalled
                .Select(m => m)
                .Where(m => (m.ParentAssembly != testAssembly && m.ParentAssembly != bDDAssembly)).Count();
        }

        private int CountMethodsCalledFromAssembly(string methodName, string assemblyName)
        {
            IAssembly assembly = codeElementsManager.GetAssemblyByName(assemblyName);
            IMethod method = codeElementsManager.GetMethodByName(methodName);
            return method.MethodsCalled.Select(m => m).Where(m => m.ParentAssembly == assembly).Count();
        }

        private int CountMethodsCallingMeNotFromAssembly(string methodName, string assemblyName)
        {
            IAssembly assembly = codeElementsManager.GetAssemblyByName(assemblyName);
            IMethod method = codeElementsManager.GetMethodByName(methodName);
            return method.MethodsCalled.Select(m => m).Where(m => m.ParentAssembly != assembly).Count();
        }



    }
}
