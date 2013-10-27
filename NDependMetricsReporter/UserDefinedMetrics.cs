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

        private int CountMethodsCalledFromAssembly(string methodName, string assemblyName)
        {
            IAssembly assembly = codeElementsManager.GetAssemblyByName(assemblyName);
            IMethod method = codeElementsManager.GetMethodByName(methodName);
            return method.MethodsCalled.Select(m => m).Where(m => m.ParentAssembly == assembly).Count();
        }




    }
}
