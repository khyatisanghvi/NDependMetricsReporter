using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDepend.CodeModel;
using NDepend.CodeQuery;
using NDepend.Analysis;

namespace NDependMetricsReporter
{
    class UserDefinedMetrics
    {
        ICodeBase codeBaseToStudy;
        CodeElementsManager codeElementsManager;

        public UserDefinedMetrics(ICodeBase codeBaseToStudy)
        {
            this.codeBaseToStudy = codeBaseToStudy;
            codeElementsManager = new CodeElementsManager(codeBaseToStudy);
        }

        public void CheckStringCodeQuery(ICodeBase codeBase)
        {
            string s = "from m in Assemblies.WithNameLike(\"UnitTest\") select new { m, m.NbLinesOfCode }";
            IQueryCompiled compiledString = s.Compile(codeBase);
            IQueryExecutionResult result = compiledString.QueryCompiledSuccess.Execute();
            IQueryExecutionSuccessResult successResult = result.SuccessResult;
        }

        public List<IMethod> GetUnitTestFromType(ICodeBase codeBase)
        {
            var complexMethods = (from m in codeBase.Application.Methods
                                  where m.ILCyclomaticComplexity > 10
                                  orderby m.ILCyclomaticComplexity descending
                                  select m).ToList();

            /*var implementing = from t in codeBase.Application.Types
                               where t.Implement("System.IDisposable".AllowNoMatch())
                               select t;*/

            var r = from m in codeBase.Application.Methods
                    where m.NbLinesOfCode > 10
                    let CC = m.CyclomaticComplexity
                    let uncov = (100 - m.PercentageCoverage) / 100f
                    let CRAP = (CC * CC * uncov * uncov * uncov) + CC
                    where CRAP != null && CRAP > 30
                    orderby CRAP descending, m.NbLinesOfCode descending
                    select new { m, CRAP, CC, uncoveredPercentage = uncov * 100, m.NbLinesOfCode };
            
            return complexMethods;
        }

        public int GetDistribition(ICodeBase codeBase, string codeElementName)
        {
            INamespace mynamespace = codeBase.Namespaces.WithName("RCNGCMembersManagementAppLogic.Billing").First();
            var r = from m in codeBase.Types.WithNameIn("BillUnitTests").ChildMethods()
                    let appMethodsCalledCount = m.MethodsCalled.Intersect(codeBase.Assemblies.WithNameIn("RCNGCMembersManagementAppLogic").ChildMethods()).Count()
                    where m.IsUsingNamespace(mynamespace)
                    //select new { m.Name, m.MethodsCalled};
                    select new { m.Name, appMethodsCalledCount };
            return 1;
        }

        public int CountAppLogicMethodsCalled(string methodName)
        {
            return CountMethodsCalledFromAssembly(methodName, "RCNGCMembersManagementAppLogic");
        }

        private int CountMethodsCalledFromAssembly(string methodName, string assemblyName)
        {
            IAssembly assembly = codeElementsManager.GetAssemblyByName(assemblyName);
            IMethod method = codeElementsManager.GetMethodByName(methodName);
            int i = method.MethodsCalled.Select(m => m).Where(m => m.ParentAssembly == assembly).Count();
            /*List<IMethod> methodsInAssembly = codeBaseToStudy.Assemblies.WithNameIn(assemblyName).ChildMethods().ToList();
            List<IMethod> methodsCalled = method.MethodsCalled.ToList();
            int j = methodsCalled.Intersect(methodsInAssembly).Count();*/
            return i;
        }


    }
}
