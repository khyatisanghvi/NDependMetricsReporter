using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDepend;
using NDepend.Path;
using NDepend.Analysis;
using NDepend.Project;
using NDepend.CodeModel;


namespace NDependMetricsReporter
{
    class AnalysisHistoryManager
    {
        List<IAnalysisResultRef> analysisResultRefsList;

        public AnalysisHistoryManager(string nDpendProjectpath)
        {
            NDependServicesProvider nDependServicesProvider = new NDependServicesProvider();
            var projectManager = nDependServicesProvider.ProjectManager;
            IAbsoluteFilePath pathToNDependProject = PathHelpers.ToAbsoluteFilePath(nDpendProjectpath);
            IProject nDependProject = projectManager.LoadProject(pathToNDependProject);
            ICollection<IAnalysisResultRef> analysisResultRefs = nDependProject.GetAvailableAnalysisResultsRefs();
            analysisResultRefsList = analysisResultRefs.OrderBy(analysisResultRef => analysisResultRef.Date).ToList();
        }

        public AnalysisHistoryManager(IProject nDependProject)
        {
            ICollection<IAnalysisResultRef> analysisResultRefs = nDependProject.GetAvailableAnalysisResultsRefs();
            analysisResultRefsList = analysisResultRefs.OrderBy(analysisResultRef => analysisResultRef.Date).ToList();
        }

        public List<IAnalysisResultRef> AnalysisResultRefsList
        {
            get { return analysisResultRefsList; }
        }

        public IList GetMetricHistory(string codeElementName, object metricDefinition)
        {
            CodeBaseManager codeBaseManager = new CodeBaseManager(analysisResultRefsList[0].Project);
            
            Type metricType;
            string codeElementType;
            Type metricDefinitionType = metricDefinition.GetType();
            if (metricDefinitionType == typeof(NDependMetricDefinition))
            {
                metricType = Type.GetType(((NDependMetricDefinition)metricDefinition).NDependMetricType);
                codeElementType = ((NDependMetricDefinition)metricDefinition).NDependCodeElementType;
            }
            else
            {
                metricType = Type.GetType(((UserDefinedMetricDefinition)metricDefinition).MetricType);
                codeElementType = ((UserDefinedMetricDefinition)metricDefinition).NDependCodeElementType;
            }

            Type nullableMetricType = typeof(Nullable<>).MakeGenericType(metricType);
            var metricValue = Activator.CreateInstance(nullableMetricType);
            IList metricValues = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(nullableMetricType));

            foreach (var analysisResultRef in analysisResultRefsList)
            {
                ICodeBase currentAnalysisResultCodeBase = codeBaseManager.LoadCodeBase(analysisResultRef);
                CodeElementsManager currentAnalysisResultCodeBaseManager = new CodeElementsManager(currentAnalysisResultCodeBase);
                UserDefinedMetrics userDefinedMetrics = new UserDefinedMetrics(currentAnalysisResultCodeBase);
                
                metricValue = null;
                switch (codeElementType)
                {
                    case "NDepend.CodeModel.IAssembly":
                        IAssembly selectedAssemblyFromCurrentAnalysisResultCodebase = currentAnalysisResultCodeBaseManager.GetAssemblyByName(codeElementName);
                        if (selectedAssemblyFromCurrentAnalysisResultCodebase != null)
                            metricValue = metricDefinitionType == typeof(NDependMetricDefinition) ?
                                currentAnalysisResultCodeBaseManager.GetCodeElementMetricValue(selectedAssemblyFromCurrentAnalysisResultCodebase, typeof(IAssembly), (NDependMetricDefinition)metricDefinition) :
                                userDefinedMetrics.InvokeUserDefinedMetric(codeElementName, ((UserDefinedMetricDefinition)metricDefinition).MethodNameToInvoke);
                        break;
                    case "NDepend.CodeModel.INamespace":
                        INamespace selectedNamespaceFromCurrentAnalysisResultCodebase = currentAnalysisResultCodeBaseManager.GetNamespaceByName(codeElementName);
                        if (selectedNamespaceFromCurrentAnalysisResultCodebase != null)
                            metricValue = metricDefinitionType == typeof(NDependMetricDefinition) ?
                                currentAnalysisResultCodeBaseManager.GetCodeElementMetricValue(selectedNamespaceFromCurrentAnalysisResultCodebase, typeof(INamespace), (NDependMetricDefinition)metricDefinition) :
                                userDefinedMetrics.InvokeUserDefinedMetric(codeElementName, ((UserDefinedMetricDefinition)metricDefinition).MethodNameToInvoke);
                        break;
                    case "NDepend.CodeModel.IType":
                        IType selectedTypeFromCurrentAnalysisResultCodebase = currentAnalysisResultCodeBaseManager.GetTypeByName(codeElementName);
                        if (selectedTypeFromCurrentAnalysisResultCodebase != null)
                            metricValue = metricDefinitionType == typeof(NDependMetricDefinition) ?
                                currentAnalysisResultCodeBaseManager.GetCodeElementMetricValue(selectedTypeFromCurrentAnalysisResultCodebase, typeof(IType), (NDependMetricDefinition)metricDefinition) :
                                userDefinedMetrics.InvokeUserDefinedMetric(codeElementName, ((UserDefinedMetricDefinition)metricDefinition).MethodNameToInvoke);
                        break;
                    case "NDepend.CodeModel.IMethod":
                        IMethod selectedMethodFromCurrentAnalysisResultCodebase = currentAnalysisResultCodeBaseManager.GetMethodByName(codeElementName);
                        if (selectedMethodFromCurrentAnalysisResultCodebase != null)
                            metricValue = metricDefinitionType == typeof(NDependMetricDefinition) ?
                                currentAnalysisResultCodeBaseManager.GetCodeElementMetricValue(selectedMethodFromCurrentAnalysisResultCodebase, typeof(IMethod), (NDependMetricDefinition)metricDefinition) :
                                userDefinedMetrics.InvokeUserDefinedMetric(codeElementName, ((UserDefinedMetricDefinition)metricDefinition).MethodNameToInvoke);
                        break;
                }
                metricValues.Add(metricValue);
            }
            return metricValues;
        }
    }
}
